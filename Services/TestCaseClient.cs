using System.Net;
using Serilog;
using TestPlanTools.Extensions;
using TestPlanTools.Models;

namespace TestPlanTools.Services
{
    public class TestCaseClient : WorkItemClient
    {
        public TestCaseClient(DevOpsConfiguration config) : base(config)
        {
        }

        private const string _automationStatus = "Microsoft.VSTS.TCM.AutomationStatus";
        private const string _automatedTestName = "Microsoft.VSTS.TCM.AutomatedTestName";
        private const string _automatedTestStorage = "Microsoft.VSTS.TCM.AutomatedTestStorage";
        private const string _automatedTestId = "Microsoft.VSTS.TCM.AutomatedTestId";


        public TestCase GetTestCase(string id)
        {
            var response = GetWorkItem(id);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                return ToTestCase(GetWorkItem(id).Content);
            }
            return null;
        }

        public TestCase UpdateTestCaseAssociation(TestCaseAssociationData data, bool force = false)
        {
            var tc = GetTestCase(data.TestCaseId);
            if(tc == null)
            {
                Log.Logger.Error("Testcase with ID == {0} not found", data.TestCaseId);
                return null;
            }

            var op = (tc.Automationstatus == "Not Automated") ? "add" : "replace";
            if (tc.Automationstatus == "Automated" && !force)
            {
                Log.Logger.Information("Did not update test case {0}. It is already associated.", tc.AutomatedTestName);
                return null;
            }

            var sendData = CreateSendData(op, data);
            var response = PatchWorkItem(data.TestCaseId, sendData);

            var newTestCase = ToTestCase(response.Content);
            Log.Logger.Information("Updated test id = {0}", data.TestCaseId);
            Log.Logger.Information("Automated test name {0}", newTestCase.AutomatedTestName);
            Log.Logger.Information("Automated test storage = {0}", newTestCase.AutomatedTestStorage);
            Log.Logger.Information("Automated test guid = {0}", newTestCase.Id);
            return newTestCase;
        }

        private string CreateSendData(string operation, TestCaseAssociationData data)
        {
            var patchDataList = new PatchDataList();
            patchDataList.Add(operation, $"/{_fields}/{_automatedTestId}", data.AutomatedTestId);
            patchDataList.Add(operation, $"/{_fields}/{_automatedTestName}", data.AutomatedTestName);
            patchDataList.Add(operation, $"/{_fields}/{_automatedTestStorage}", data.AutomatedTestStorage);
            return patchDataList.ToJson();
        }

        private TestCase ToTestCase(string body)
        {
            Log.Logger.Information("BODY " + body);
            var json = body.ToJson();

            return new TestCase
            {
                Id = json["id"].ToString(),
                Automationstatus = json[_fields][_automationStatus]?.ToString(),
                AutomatedTestName = json[_fields][_automatedTestName]?.ToString(),
                AutomatedTestStorage = json[_fields][_automatedTestStorage]?.ToString()
            };
        }
    }
}