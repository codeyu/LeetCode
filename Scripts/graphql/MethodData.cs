using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace graphql
{
    public class MethodData
    {
        [JsonProperty("name")]
        public string Name{get;set;}
        [JsonProperty("params")]
        public List<TypeName> Params {get;set;}
        [JsonProperty("return")]
        public TypeName Return {get; set;}
    }
    public class TypeName
    {
        public string name {get;set;}
        public string type {get;set;}

        public bool dealloc {get;set;}
    }
}