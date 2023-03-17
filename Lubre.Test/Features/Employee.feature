Feature: Employee

@mytag
Scenario: Find Employee by ID
	Given I have connected to the API
	When I search for the employee by ID 1234
	Then I should see the employee details
	| EmployeeId | Name        | Dni      |
	| 1234       | Cesar Oliva | 28479584 |
