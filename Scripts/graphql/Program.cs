using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
namespace graphql
{
    class Program
    {
        private static readonly Leetcode lc = new Leetcode();
        static void Main(string[] args)
        {
            var questionId = 0L;
            if(args.Length > 0)
            {
                long.TryParse(args[0], out questionId);
            }
            if(questionId <= 0)
            {
                var s = "";
                do  
                {
                    Console.WriteLine("Please enter a QuestionId:");
                    s = Console.ReadLine();
                    long.TryParse(s, out questionId);
                
                } while (questionId <= 0);
            }
            try
            {
                var questionStat = lc.GetAllAsync().Result;
                if(questionStat != null && questionStat.StatStatusPairs.Any())
                {
                    var statStatusPair = questionStat.StatStatusPairs.Where(x=>x.Stat.QuestionId==questionId).FirstOrDefault();
                    if(statStatusPair != null)
                    {
                        var questionDetail = lc.GetLeetcodeAsync(statStatusPair.Stat.QuestionTitleSlug).Result;
                        TemplateOpt temp = new TemplateOpt(questionDetail);
                        temp.Save();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }
        }
        
    }
}
