Feature: Add Metrics
	In order to track my progress
	As a user
	I want to be able to add measures to the system

Scenario: Add a new metric
	Given I am logged in user
	And I have no existing measures
	When I add a new measure called "Biceps"
	Then I can see "Biceps" measure in my home page
	And I only have 1 measure(s)