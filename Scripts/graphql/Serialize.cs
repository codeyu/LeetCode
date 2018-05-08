namespace graphql
{
    using Newtonsoft.Json;

    public static class Serialize
    {
        public static string ToJson(this QuestionStat self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this QuestionDetail self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this CodeDefinition self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}