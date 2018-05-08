using Newtonsoft.Json;

namespace graphql
{
    public partial  class CodeDefinition
    {
        [JsonProperty("text")]
        public string Text{get;set;}
        [JsonProperty("value")]
        public string Value{get;set;}
        [JsonProperty("defaultCode")]
        public string DefaultCode{get;set;}
    }
    public partial class CodeDefinition
    {
        public static CodeDefinition FromJson(string json) => JsonConvert.DeserializeObject<CodeDefinition>(json, Converter.Settings);
    }
}