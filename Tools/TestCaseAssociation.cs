using System;
using System.Collections.Generic;
using TestPlanTools.Attributes;
using TestPlanTools.Models;
using TestPlanTools.Refelection;

namespace TestPlanTools.Tools
{
    public class TestCaseAssociation
    {
        public List<TestCaseAssociationData> AssociateTestCases()
        {
            var resultList = new List<TestCaseAssociationData>();
            List<AttributedMethod> attributedMethods = MethodIdentifier.GetAllAttributedMethodsInAssemblies<TestCaseIdAttribute>();

            foreach (var method in attributedMethods)
            {
                TestCaseIdAttribute attribute = (TestCaseIdAttribute)Attribute.GetCustomAttribute(method.MethodInfo, typeof(TestCaseIdAttribute));
                resultList.Add(new TestCaseAssociationData
                {
                    AutomatedTestName = method.Type.FullName + "." + method.MethodInfo.Name,
                    AutomatedTestStorage = method.AssemblyName,
                    TestCaseId = attribute.TestId
                });
            }
            return resultList;
        }
    }
}