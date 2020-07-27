# TestPlanTools

Adds functionality to Azure DevOps Test Plan and helps with test automation.

## Functions

### Test case association

Lets you associate automated test cases with test cases in your test plan.
Add an annotation to your test case - the id is the test case id in the test plan

```c#
[TestCaseId("123")]
public static void TestMethod()
{
    // ...
}
```

Then initialize the TestPlanTool, set your DevOps-account and call method 'AssociateTestCases'. This must be done from inside the assembly that contains the tests.

```c#
var testPlanTools = new TestPlanTool(new DevOpsConfiguration
{
    Url = "",
    Project = "",
    User = "",
    Token = "".ToSecureString()
});

testPlanTools.AssociateTestCases();
```
