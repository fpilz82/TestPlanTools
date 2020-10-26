using System;

namespace TestPlanTools.TestStepAttribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class TestStepAttribute : Attribute
    {
        private string _stepName;

        public string TestId { get => _stepName; }

        public TestStepAttribute(string stepName)
        {
            _stepName = stepName;
        }
    }
}