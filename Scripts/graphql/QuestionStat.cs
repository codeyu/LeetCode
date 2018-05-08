namespace graphql
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class QuestionStat
    {
        [JsonProperty("frequency_mid")]
        public long FrequencyMid { get; set; }

        [JsonProperty("frequency_high")]
        public long FrequencyHigh { get; set; }

        [JsonProperty("ac_medium")]
        public long AcMedium { get; set; }

        [JsonProperty("ac_easy")]
        public long AcEasy { get; set; }

        [JsonProperty("num_solved")]
        public long NumSolved { get; set; }

        [JsonProperty("category_slug")]
        public string CategorySlug { get; set; }

        [JsonProperty("stat_status_pairs")]
        public StatStatusPair[] StatStatusPairs { get; set; }

        [JsonProperty("ac_hard")]
        public long AcHard { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("num_total")]
        public long NumTotal { get; set; }
    }

    public partial class StatStatusPair
    {
        [JsonProperty("status")]
        public object Status { get; set; }

        [JsonProperty("stat")]
        public Stat Stat { get; set; }

        [JsonProperty("is_favor")]
        public bool IsFavor { get; set; }

        [JsonProperty("paid_only")]
        public bool PaidOnly { get; set; }

        [JsonProperty("difficulty")]
        public Difficulty Difficulty { get; set; }

        [JsonProperty("frequency")]
        public long Frequency { get; set; }

        [JsonProperty("progress")]
        public long Progress { get; set; }
    }

    public partial class Difficulty
    {
        [JsonProperty("level")]
        public long Level { get; set; }
    }

    public partial class Stat
    {
        [JsonProperty("total_acs")]
        public long TotalAcs { get; set; }

        [JsonProperty("question__title")]
        public string QuestionTitle { get; set; }

        [JsonProperty("is_new_question")]
        public bool IsNewQuestion { get; set; }

        [JsonProperty("question__article__slug")]
        public string QuestionArticleSlug { get; set; }

        [JsonProperty("total_submitted")]
        public long TotalSubmitted { get; set; }

        [JsonProperty("frontend_question_id")]
        public long FrontendQuestionId { get; set; }

        [JsonProperty("question__title_slug")]
        public string QuestionTitleSlug { get; set; }

        [JsonProperty("question__article__live")]
        public bool? QuestionArticleLive { get; set; }

        [JsonProperty("question__hide")]
        public bool QuestionHide { get; set; }

        [JsonProperty("question_id")]
        public long QuestionId { get; set; }
    }

    public partial class QuestionStat
    {
        public static QuestionStat FromJson(string json) => JsonConvert.DeserializeObject<QuestionStat>(json, Converter.Settings);
    }
}