using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;
using Refit;
namespace graphql
{
    public class Leetcode
    {
        public static string BaseUrl => "https://leetcode.com";
        public async Task<QuestionStat> GetAllAsync()
        {
            var leetcodeApi = RestService.For<ILeetcode>(BaseUrl);

            var questionStat = await leetcodeApi.GetAllQuestionStat();
            return questionStat;
        }

        public async Task<QuestionDetail> GetLeetcodeAsync(string titleSlug)
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
                    sampleTestCase
                    metaData
                    questionDetailUrl
                  }
                
                }",
                OperationName = "getQuestionDetail",
                Variables = new
                {
                    titleSlug = titleSlug
                }
            };
            var graphQLClient = new GraphQLClient($"{BaseUrl}/graphql");
            var graphQLResponse = await graphQLClient.GetAsync(heroAndFriendsRequest);
            var questionDetail = graphQLResponse.GetDataFieldAs<QuestionDetail>("question");
            return questionDetail;   
        }
    }
}