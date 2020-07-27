using Serilog;
using Serilog.Core;
using TestPlanTools.Services;
using TestPlanTools.Tools;

namespace TestPlanTools
{
    public class TestPlanTool
    {
        public Logger Logger;
        private DevOpsConfiguration _config;

        public TestPlanTool(DevOpsConfiguration config)
        {
            _config = config;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Logger = (Logger)Log.Logger;
        }

        public void AssociateTestCases(bool forceUpdate = false)
        {
            var associatedTestCases = new TestCaseAssociation().AssociateTestCases();
            var client = new TestCaseClient(_config);

            foreach (var atc in associatedTestCases)
            {
                client.UpdateTestCaseAssociation(atc, forceUpdate);
            }
        }
    }
}