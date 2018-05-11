using System;
using CmdRunner;
using System.IO;
using Scriban;
using Scriban.Runtime;
using Scriban.Parsing;
using System.Text.RegularExpressions;
using System.Linq;

namespace graphql
{
    public class TemplateOpt
    {
        private QuestionDetail _detail;
        private string _algorithmsPath;
        private string _algorithmsTestPath;
        private string _readmePath = @"../../README.md";
        private string _author;

        private CSharpCode _csharpCode;
        public string AlgorithmsTmp =>@"
// Source : {{detail.question_url}}
// Author : {{author}}
// Date   : {{date}}

/********************************************************************************** 
* 
*{{for contentLine in detail.content_lines}} 
* {{contentLine}}
*{{end}}
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
        public static {{csharp.return_type}} {{csharp.method_name}}({{csharp.params_txt}})
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
        public void TestMethod({{csharp.params_txt}},{{csharp.return_type}} output)
        {
            Assert.Equal(output, Solution{{detail.question_id}}.{{csharp.method_name}}({{ for param in csharp.params }}{{ param.name }}{{if !for.last }}, {{end}}{{ end }}));
        }
    }
}";
        public string ReadmeTmp =>@"|{{detail.question_id}}|[{{detail.question_title}}]({{detail.question_url}}) | [C#](./Algorithms/{{detail.question_name}}.cs)|{{detail.difficulty}}|";
        public TemplateOpt(QuestionDetail detail)
        {
            _detail = detail;
            _csharpCode = BuilderCSharpCodeModel(detail.CSharpCodeTxt);
            dynamic cmd = new Cmd();
            _author = cmd.git.config(get: "user.name");
            _algorithmsPath = $"../../Algorithms/{_detail.QuestionName}.cs";
            _algorithmsTestPath = $"../../AlgorithmsTest/{_detail.QuestionName}Test.cs";
        }

        public bool Save()
        {
            var template = Template.Parse(AlgorithmsTmp);
            var algorithmsText = template.Render(new { Author = _author, Date = DateTime.Now, Detail = _detail, Csharp = _csharpCode});
            File.WriteAllText(_algorithmsPath, algorithmsText);
            template = Template.Parse(AlgorithmsTestTmp);
            var algorithmsTestText = template.Render(new { Detail = _detail, Csharp = _csharpCode});
            File.WriteAllText(_algorithmsTestPath, algorithmsTestText);
            template = Template.Parse(ReadmeTmp);
            var readmeText = template.Render(new { Detail = _detail});
            File.AppendAllLines(_readmePath, new[]{readmeText});
            return true;
        }
        private CSharpCode BuilderCSharpCodeModel(string chsarpCodeTxt)
        {
            var pattern = @"(?s)\bSolution\b\s\{(.*?)\{";
            var match = Regex.Matches(chsarpCodeTxt, pattern).FirstOrDefault();
            var csharp = new CSharpCode();
            if(match != null)
            {
                var methodDeclare = match.Groups[1].Value.Trim();
                csharp.MethodName = methodDeclare.Split(' ')[2].Substring(0, methodDeclare.Split(' ')[2].IndexOf('('));
                csharp.ReturnType = methodDeclare.Split(' ')[1];
                csharp.ParamsTxt = methodDeclare.Substring(methodDeclare.IndexOf('(') + 1, methodDeclare.IndexOf(')') - methodDeclare.IndexOf('(') - 1);
                csharp.Params = csharp.ParamsTxt.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(x=>new TypeName{type=x.Split(' ')[0],name=x.Split(' ')[1]})
                                                .ToList();

            }
            return csharp;
        }
    }
}