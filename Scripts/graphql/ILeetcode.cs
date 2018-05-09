using System;
using System.Threading.Tasks;
using Refit;
namespace graphql
{
    public interface ILeetcode
    {
         [Get("/api/problems/all")]
         Task<QuestionStat> GetAllQuestionStat();
    }
}