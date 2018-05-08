using System;
using GraphQL.Client;
using GraphQL.Common.Request;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace graphql
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GetLeetcodeAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }
        }
        static private async Task GetLeetcodeAsync()
        {
            var heroAndFriendsRequest = new GraphQLRequest
            {
                Query = @"
                    query getQuestionDetail($titleSlug: String!) {
                    question(titleSlug: $titleSlug) {
                    questionId
                    questionTitle
                    questionTitleSlug
                    content
                    difficulty
                    categoryTitle
                    codeDefinition
                  }
                
                }",
                OperationName = "getQuestionDetail",
                Variables = new
                {
                    titleSlug = "house-robber"
                }
            };
            var graphQLClient = new GraphQLClient("https://leetcode.com/graphql");
            var graphQLResponse = await graphQLClient.GetAsync(heroAndFriendsRequest);
            var questionDetail = graphQLResponse.GetDataFieldAs<QuestionDetail>("question");
            Console.WriteLine(questionDetail.ToJson());
            var codeDefinitions = JsonConvert.DeserializeObject<List<CodeDefinition>>(questionDetail.CodeDefinition);
            Console.WriteLine(codeDefinitions[0].ToJson());
        }
    }
}
