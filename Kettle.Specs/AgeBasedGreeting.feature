Feature: AgeBasedGreeting
	In order to be greeted
	As a young person below 30
	I want to be greeted with howdy

Scenario: Greet persons below 30 with howdy, otherwise say hello	
	Given I have an input file and expected file for test "agebasedgreeting"
	When I run the transform
	Then the output file should match the expected file

