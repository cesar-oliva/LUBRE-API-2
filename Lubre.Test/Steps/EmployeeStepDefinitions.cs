using TechTalk.SpecFlow;
using System;

namespace Lubre.Test.Steps
{
    [Binding]
    public sealed class EmployeeStepDefinitions
    {
       
       // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

       private readonly ScenarioContext _scenarioContext;

       public EmployeeStepDefinitions(ScenarioContext scenarioContext)
       {
           _scenarioContext = scenarioContext;
       }

       [Given("I have connected to the API")]
       public void GivenIHaveAnEmployeeWithId()
       {
           //TODO: implement arrange (precondition) logic
           // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
           // To use the multiline text or the table argument of the scenario,
           // additional string/Table parameters can be defined on the step definition
           // method. 

           _scenarioContext.Pending();
       }
        
       [When("I search for the employee by ID (.*)")]
       public void WhenISearchForTheEmployeeByID(Guid id)
       {
           //TODO: implement act (action) logic

           _scenarioContext.Pending();
       }

       [Then("I should see the employee details (.*)")]
       public void ThenIShouldSeeTheEmployeeDetails()
       {
           //TODO: implement assert (verification) logic

           _scenarioContext.Pending();
       }
    }
}
