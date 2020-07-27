using System;

namespace TPTools.Models
{
    public class TestCaseAssociationData
    {
        public string AutomatedTestName { get; set; }
        public string AutomatedTestStorage { get; set; }
        public string AutomatedTestId { get => Guid.NewGuid().ToString(); }
        public string TestCaseId { get; set; }
    }
}