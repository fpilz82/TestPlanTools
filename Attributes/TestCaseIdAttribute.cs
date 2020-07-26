using System;

namespace TestPlanTools.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TestCaseIdAttribute : Attribute
    {
        private string _testCaseId;

        public string TestId { get => _testCaseId; }

        public TestCaseIdAttribute(string testCaseId)
        {
            _testCaseId = testCaseId;
        }
    }
}