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

        public string[] ContentLines => HtmlToText(Content).Split('\n');
        public MethodData MethodData => JsonConvert.DeserializeObject<MethodData>(MetaData);

        public string CSharpCode => CodeDefinitions.Where(x=>x.Value=="csharp").First().DefaultCode;
    }

    public partial class QuestionDetail
    {
        public static QuestionDetail FromJson(string json) => JsonConvert.DeserializeObject<QuestionDetail>(json, Converter.Settings);

        private static string HtmlToText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text); 
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }


}
