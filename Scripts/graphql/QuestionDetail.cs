using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace graphql
{
    public partial class QuestionDetail
    {
        [JsonProperty("questionId")]
        public string QuestionId { get; set; }

        [JsonProperty("questionTitle")]
        public string QuestionTitle { get; set; }

        [JsonProperty("questionTitleSlug")]
        public string QuestionTitleSlug { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("categoryTitle")]
        public string CategoryTitle { get; set; }

        [JsonProperty("codeDefinition")]
        public string CodeDefinition { get; set; }

        [JsonProperty("sampleTestCase")]
        public string SampleTestCase {get; set;}

        [JsonProperty("metaData")]
        public string MetaData {get; set;}

        [JsonProperty("questionDetailUrl")]
        public string QuestionDetailUrl{get;set;}

        public string QuestionName => QuestionTitle.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", "").Replace("'", "").Replace("-", "");
        public string QuestionUrl => $"{Leetcode.BaseUrl}{QuestionDetailUrl}";
        public List<CodeDefinition> CodeDefinitions => JsonConvert.DeserializeObject<List<CodeDefinition>>(CodeDefinition);

        public string[] ContentLines => HtmlToText(Content).Replace("\r","").Split('\n', StringSplitOptions.RemoveEmptyEntries);
        public MethodData MethodData => JsonConvert.DeserializeObject<MethodData>(MetaData);

        public string CSharpCodeTxt => CodeDefinitions.Where(x=>x.Value=="csharp").First().DefaultCode;

    }

    public partial class QuestionDetail
    {
        public static QuestionDetail FromJson(string json) => JsonConvert.DeserializeObject<QuestionDetail>(json, Converter.Settings);

        private static string HtmlToText(string html)
        {
            var regex = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return System.Web.HttpUtility.HtmlDecode((regex.Replace(html, "")));
        }
    }


}
