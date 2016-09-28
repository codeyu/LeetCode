using  System;
using System.Collections.Generic;
namespace Algorithms.Utils
{
    /// <summary>
    /// Visit the node
    /// </summary>
    /// <param name="sourceLabel">Node label</param>
    public delegate void VisitNode(int sourceLabel);

    /// <summary>
    /// Visit the edge
    /// </summary>
    /// <param name="source">Source node label</param>
    /// <param name="target">Target node label</param>
    /// <param name="start">Start of the suffix</param>
    /// <param name="end">End of the suffix</param>
    public delegate void VisitEdge(int source, int target, int start, int end);
    public class SuffixTree
    {
        Node _activeNode;

        // keep track of last branch nodes to add suffix pointers
        Node _lastBranchNode;


        int _activeLength = 0;
        int _lastBranchIndex = 0;
        uint _currentNodeNumber = 0;
        int _minDistance = 0;

        /// <summary>
        /// Root node for walking the tree
        /// </summary>
        internal Node RootNode { get; private set; }

        /// <summary>
        /// Original text
        /// </summary>
        public static string Text { get; private set; } 

        /// <summary>
        /// Try finding the suffix in the tree
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public bool TryFind(string suffix, ref int start, ref int end)
        {
            if (string.IsNullOrWhiteSpace(suffix))
            {
                return false;
            }

            Node current = this.RootNode;
            Edge edge = null;
            bool setEnd = true;

            for (int i = 0; i < suffix.Length; )
            {
                edge = current.FindEdgeByChar(suffix[i]);

                if (edge == null)
                {
                    return false;
                }

                // we skip one character in the edge as well as the
                // suffix since it was already picked up by the call above
                int j = edge.Walk(suffix, i, 1);
                i += j;

                // if we have walked the edge...
                if (i >= suffix.Length)
                {
                    // ... and terminated before reaching a node
                    if (j < edge.Route.Length)
                    {
                        end = NormalizeEndValue(edge.End) - (edge.Route.Length - j);
                        setEnd = false;
                    }
                    break;
                }

                if (j < edge.Route.Length)
                {
                    return false;
                }

                current = edge.EndNode;
            }

            if (setEnd)
            {
                end = NormalizeEndValue(edge.End);
            }

            start = end - suffix.Length + 1;
            return true;
        }

        /// <summary>
        /// Construct the tree for a given string of text
        /// </summary>
        /// <param name="text">text from which the tree is constructed</param>
        public SuffixTree(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(text);
            }
            
            Text = text;
            _activeNode = new Node(_currentNodeNumber);
            RootNode = _activeNode;
        }

        /// <summary>
        /// Creates the actual suffix tree
        /// </summary>
        public void Create()
        {
            // some of our loop iterations actually constitute one route
            // in that case we should not chose to accidentally slip and 
            // follow the suffix node
            bool followSuffixNode = false;

            for (int i=0; i < Text.Length;)
            {
                // make sure the lower bound remains within its boundaries
                ValidateAndUpdateMinDistance(i);

                var nodeEdge = _activeNode.FindNextRoute(i + _activeLength, followSuffixNode);

                //if we have terminated in a non-leaf node we are done
                if (i + _activeLength >= Text.Length && nodeEdge == null)
                {
                    break;
                }

                // we could not find anything, add to the tree
                if (nodeEdge == null)
                {
                    _activeNode.AddNode(++_currentNodeNumber, i + _activeLength);
                    _lastBranchIndex = i + _activeLength;
                    i++;
                    followSuffixNode = true;
                    continue;
                }

                var node = nodeEdge.Item1;
                var edge = nodeEdge.Item2;

                if (edge == null)
                {
                    //we found a suffix node
                    _activeNode = node;
                    _activeLength--;
                    followSuffixNode = false;
                    continue;
                }
                else if(node != null)
                {
                    //we found a new active
                    _activeNode = node;
                    _activeLength++;
                    followSuffixNode = false;
                    continue;
                }

                // now walk the chosen path and see where the current suffix diverges
                var edgePosTuple = edge.WalkTheEdge(i, ref _activeLength, ref _minDistance, ref _activeNode);

                edge = edgePosTuple.Item1;
                int j = edgePosTuple.Item2;

                if (j == edge.Route.Length)
                {
                    _activeNode = edge.EndNode;
                    _activeLength += edge.Route.Length;
                    followSuffixNode = false;
                    continue;
                }

                // we now need to insert a new branch node
                _minDistance = j;
                _lastBranchIndex = i + j + _activeLength;

                if (_lastBranchIndex >= Text.Length)
                {
                    i++;
                    followSuffixNode = true;
                    continue;
                }

                // we are inserting a new branch node
                var newBranchNode = edge.Split(edge.Start + j - 1, ++_currentNodeNumber);

                // if we have reached this branch node through a route of just
                // one character - the last branch node should be set as the 
                if (edge.Route.Length == 1)
                {
                    newBranchNode.SuffixPointer = _activeNode;
                }

                // the second check is because of the root-node suffix pointer special case
                // above
                if (null != _lastBranchNode && _lastBranchNode.SuffixPointer == null)
                {
                    _lastBranchNode.SuffixPointer = newBranchNode;
                }
                
                newBranchNode.AddNode(++_currentNodeNumber, _lastBranchIndex);
                _lastBranchNode = newBranchNode;
                i++;
                followSuffixNode = true;
            }
        }

        /// <summary>
        /// Breadth-first walk of the tree
        /// </summary>
        /// <param name="visit">Visit type delegate</param>
        public void WalkTree(VisitNode visitNode, VisitEdge visitEdge)
        {
            Queue<Node> walkingQueue = new Queue<Node>();

            for (walkingQueue.Enqueue(this.RootNode); walkingQueue.Count > 0; )
            {
                var currentNode = walkingQueue.Dequeue();
                visitNode((int)currentNode.Label);
                foreach (var edge in currentNode.Edges)
                {
                    walkingQueue.Enqueue(edge.Value.EndNode);
                    visitEdge((int)currentNode.Label, (int)edge.Value.EndNode.Label, edge.Value.Start, NormalizeEndValue(edge.Value.End));
                }
            }
        }

        private int NormalizeEndValue(int end)
        {
            if (end < 0)
            {
                return Text.Length - 1;
            }

            return end;
        }

        // makes sure that minDistance remains a lower bound
        // in the equation lastBranchIndex >= i + activeLength + minDistance
        private void ValidateAndUpdateMinDistance(int index)
        {
            if (_lastBranchIndex < _activeLength + _minDistance + index)
            {
                _minDistance = Math.Max(0, _lastBranchIndex - _activeLength - index);
            }
        }

    }

    class Edge
    {
        internal Node EndNode { get; private set; }
        internal int Start { get; private set; }
        internal int End { get; private set; }

        internal string Route
        {
            get
            {
                return GetSubstring();
            }
        }
        /// <summary>
        /// constructor that takes relative text position
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Edge(Node node, int start, int end = -1)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", "start cannot be negative");
            }

            // pretend that "end" can be infinite, and then compare with start
            if (start > (uint)end)
            {
                throw new ArgumentOutOfRangeException("start", "cannot start the string after its end");
            }

            // infinity is just -1
            if (end < 0)
            {
                end = -1;
            }

            this.Start = start;
            this.End = end;
            this.EndNode = node;
        }

        private int GetLength()
        {
            return this.End < 0 ? SuffixTree.Text.Length - this.Start : this.End - this.Start + 1;
        }

        private string GetSubstring()
        {
            return SuffixTree.Text.Substring(this.Start, GetLength());
        }

        /// <summary>
        /// Splits the edge into two new edges.
        /// </summary>
        /// <param name="end">Index of the end of the old edge</param>
        /// <returns></returns>
        internal Node Split(int end, uint currentNodeNumber)
        {
            int nextStart = end + 1;
            var oldNode = this.EndNode;
            
            var newEdge = new Edge(oldNode, nextStart, this.End);
            Node newNode = new Node(currentNodeNumber);

            this.End = end;
            this.EndNode = newNode;
            newNode.Edges.Add(newEdge.Route[0], newEdge);
            return newNode;
        }

        /// <summary>
        /// Keep comparing original text from position i
        /// with what is in the edge
        /// </summary>
        /// <param name="i">Index of comparison start in the original text</param>
        /// <param name="skipCharacters"> How many characters are guaranteed equal</param>
        /// <returns>(edge, index) - the edje the character in it where the walk ended</returns>
        internal Tuple<Edge,int> WalkTheEdge(int i, ref int activeLength, ref int minDistance, ref Node activeNode)
        {
            string text = SuffixTree.Text;
            int skipCharacters = minDistance;
            int index = i + activeLength;

            // we know we do not need any comparisons on this edge
            if (skipCharacters >= this.Route.Length)
            {
                var edge = this.EndNode.FindEdgeByChar(i + this.Route.Length);
                activeLength += this.Route.Length;
                minDistance -= this.Route.Length;

                activeNode = this.EndNode;
                return edge.WalkTheEdge(i, ref activeLength, ref minDistance, ref activeNode);
            }

            int j = Walk(text, index, skipCharacters);
            return new Tuple<Edge, int>(this, j);
        }

        /// <summary>
        /// Walk this single edge to see whether it matches the substring
        /// </summary>
        /// <param name="suffix">Search string</param>
        /// <param name="i">Starting index</param>
        /// <returns></returns>
        internal int Walk(string suffix, int i, int skip = 0)
        {
            int j;
            for (j = skip, i += j; j < Route.Length && i < suffix.Length; j++, i++)
            {
                if (Route[j] != suffix[i])
                {
                    break;
                }
            }

            return j;
        }
    }

    class Node
    {
        internal uint Label { get; set; }
        internal Dictionary<char, Edge> Edges { get; private set; }

        internal Node SuffixPointer { get; set; }

        public Node(uint label)
        {
            this.Label = label;
            this.Edges = new Dictionary<char, Edge>();
            this.SuffixPointer = null;
        }

        /// <summary>
        /// finds next route starting from the current node
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        internal Tuple<Node, Edge> FindNextRoute(int start, bool followSuffixNode)
        {
            if (followSuffixNode && null != SuffixPointer)
            {
                return new Tuple<Node,Edge>(SuffixPointer, null);
            }

            var edge = FindEdgeByChar(start);
            if (null == edge)
            {
                return null;
            }

            // search terminated in a node
            if (edge.Route.Length == 1)
            {
                return new Tuple<Node, Edge>(edge.EndNode, edge);
            }

            //search did not terminate in a node
            return new Tuple<Node, Edge>(null, edge);
        }

        /// <summary>
        /// Adds a new node to the tree
        /// </summary>
        /// <param name="label">Node label</param>
        /// <param name="start">Start position in the text</param>
        /// <param name="end">End position in the text</param>
        internal void AddNode(uint label, int start, int end = -1)
        {
            var newNode = new Node(label);
            var newEdge = new Edge(newNode, start, end);
            this.Edges.Add(newEdge.Route[0], newEdge);
        }

        internal Edge FindEdgeByChar(int start)
        {
            //we have reached the end of the string
            if (start >= SuffixTree.Text.Length)
            {
                return null;
            }

            return FindEdgeByChar(SuffixTree.Text[start]);
        }

        internal Edge FindEdgeByChar(char c)
        {
            if (!this.Edges.ContainsKey(c))
            {
                return null;
            }

            return this.Edges[c];
        }
    }
}
