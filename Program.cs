using TPTools.Attributes;
using TPTools.Extensions;
using TPTools.Services;

namespace TPTools
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
        public static void ExampleTestMethod()
        {
            // ...
        }
    }
}
