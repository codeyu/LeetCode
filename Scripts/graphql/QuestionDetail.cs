using System;
using System.Collections.Generic;

using System.Globalization;
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

        [JsonProperty("questionDetailUrl")]
        public string QuestionDetailUrl{get;set;}
        public List<CodeDefinition> CodeDefinitions => JsonConvert.DeserializeObject<List<CodeDefinition>>(CodeDefinition);
    }

    public partial class QuestionDetail
    {
        public static QuestionDetail FromJson(string json) => JsonConvert.DeserializeObject<QuestionDetail>(json, Converter.Settings);
    }


}
