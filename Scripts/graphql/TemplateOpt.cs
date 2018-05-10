using System;
using CmdRunner;
using System.IO;
using Scriban;
using Scriban.Runtime;
using Scriban.Parsing;
namespace graphql
{
    public class TemplateOpt
    {
        private QuestionDetail _detail;
        private string _algorithmsPath;
        private string _algorithmsTestPath;
        private string _readmePath = @"../../README.md";
        private string _author;
        public string AlgorithmsTmp =>@"
// Source : {{detail.question_url}}
// Author : {{author}}
// Date   : {{date}}

/********************************************************************************** 
* 
{{for contentLine in detail.content_lines}} 
* {{contentLine}}
{{end}}
* 
*               
**********************************************************************************/
using System;
using System.Collections.Generic;
using Algorithms.Utils;
namespace Algorithms
{
    public class Solution{{detail.question_id}} 
    {
        public static {{m.return.type}} {{m.name}}({{ for param in m.params }} {{ param.type }} {{ param.name }},{{ end }}) 
        {
            throw new NotImplementedException();
        }
    }
}";
        public string AlgorithmsTestTmp =>@"
using System;
using System.Collections.Generic;
using Algorithms;
using Algorithms.Utils;
using Xunit;
namespace AlgorithmsTest
{
    public class {{detail.question_name}}Test
    {
        [Theory]
        [InlineData({{detail.SampleTestCase}})]
        public void TestMethod({{ for param in m.params }} {{ param.type }} {{ param.name }},{{ end }} {{m.return.type}} output)
        {
            Assert.Equal(output, Solution{{detail.question_id}}.{{m.name}}({{ for param in m.params }} {{ param.name }},{{ end }}));
        }
    }
}";
        public string ReadmeTmp =>@"|{{detail.question_id}}|[{{detail.question_title}}]({{detail.question_url}}) | [C#](./Algorithms/{{detail.question_name}}.cs)|{{detail.difficulty}}|";
        public TemplateOpt(QuestionDetail detail)
        {
            _detail = detail;
            dynamic cmd = new Cmd();
            _author = cmd.git.config(get: "user.name");
            _algorithmsPath = $"../../Algorithms/{_detail.QuestionName}.cs";
            _algorithmsTestPath = $"../../AlgorithmsTest/{_detail.QuestionName}Test.cs";
        }

        public bool Save()
        {
            var template = Template.Parse(AlgorithmsTmp);
            var algorithmsText = template.Render(new { Author = _author, Date = DateTime.Now, Detail = _detail, M = _detail.MethodData});
            File.WriteAllText(_algorithmsPath, algorithmsText);
            template = Template.Parse(AlgorithmsTestTmp);
            var algorithmsTestText = template.Render(new { Detail = _detail, M = _detail.MethodData});
            File.WriteAllText(_algorithmsTestPath, algorithmsTestText);
            template = Template.Parse(ReadmeTmp);
            var readmeText = template.Render(new { Detail = _detail});
            File.AppendAllLines(_readmePath, new[]{readmeText});
            return true;
        }
    }
}