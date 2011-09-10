Feature: Add Metrics
	In order to track my progress
	As a user
	I want to be able to add measures to the system

Scenario: Add a new metric
	Given I am logged in user
	And I have no existing measures
	When I add a new measure called "Biceps" with unit in "cm" and save
	Then I can see "Biceps" measure in my measures page
	And I have exactly 1 measure(s) in my measures page

Scenario: Add a new metric and cancel
	Given I am logged in user
	And I have no existing measures
	When I add a new measure called "Biceps" with unit in "cm" and cancel
	Then I cannot see "Biceps" measure in my measures page
	And I have exactly 0 measure(s) in my measures page