using System;
using System.Security;
using TestPlanTools.Attributes;
using TestPlanTools.Extensions;
using TestPlanTools.Refelection;
using TestPlanTools.Services;

namespace TestPlanTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var tpt = new TestPlanTools(new DevOpsConfiguration
            {
                Url = "",
                Project = "",
                User = "",
                Token = "".ToSecureString()
            });

            tpt.AssociateTestCases();

        }

        [TestCaseId("123")]
        public static void PrintAllAttributedMethodsInExecutingAssembly()
        {

        }
    }
}
