using RestSharp;
using TestPlanTools.Services;

namespace TestPlanTools.Services
{

    public class WorkItemClient : GenericRestClient
    {
        private string _workItem = "wit/workitems/";
        private string _apiVersion = "?api-version=5.1";
        private protected const string _fields = "fields";

        public WorkItemClient(DevOpsConfiguration config) : base(config)
        {
        }

        public IRestResponse GetWorkItem(string workItemId) =>
            Get(_workItem + workItemId + _apiVersion);

        public IRestResponse PatchWorkItem(string workItemId, string body) =>
            Patch(_workItem + workItemId + _apiVersion, body);
    }
}