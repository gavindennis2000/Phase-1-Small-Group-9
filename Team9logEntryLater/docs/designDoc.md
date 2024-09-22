# Team9logEntryLater Design Document
This document will outline the design of the Team9logEntryLater application, an application that allows
users to log entries for later viewing.

---

### GroupNumber: 9
### People working on the project: Sam Irvin
### Date of First Design: 9/21/2024

---

## Overview

> Team9logEntryLater.cs
```csharp
/* 
Input:		
			string lastName
			string firstName
			string classId
			datetime date
			datetime startTime
			datetime endTime
			int howManyPeople
			int activityCode
			string note
Output:		
			string to console
			csv file
Constraints:	none
Edge Cases:	
			last name and first name should only contain letters
			last name and first name should not be empty
			last name and first name should not be null

define variables to hold user inputs for: 
	string lastName
	string firstName
	string fileName
	string classId
	datetime date
	datetime startTime
	datetime endTime
	int howManyPeople
	int activityCode
	Note note
	List<LogEntry> logEntries

Outpute a welcome message explaining the purpose of the application
prompt user for an interaction to continue
*/
```
###### page 1

```csharp
/*
Request user inputs 
	if first name and last name are not empty and contain only letters
		store first and last name in variables
		store combination of lastName + firstName + "Log.csv" into fileName variable
	else 
		prompt user to enter valid first and last name

Check local file system for existing file that matches fileName
	if file exists
		open file and read contents
	else if file does not exist
		display error to user that a file authored by that user does not exist and close the program
	else
		display error that there are duplicate files that are made by the same user and close the program

If the file is opened
	read last name and first name from the file
	read class id from the file
	output the last name, first name, and class id to the console
	prompt the user to confirm that the information is correct and the correct file was opened
	if the user confirms
		promt user for:
			date
				if date is not in the correct format
					prompt user to enter date in the correct format
				else if date is in the correct format
					store date in variable
			start time
				if start time is not in the correct format
					prompt user to enter start time in the correct format
				else if start time is in the correct format
					store start time in variable
			end time
				if end time is not in the correct format
					prompt user to enter end time in the correct format
				else if end time is in the correct format
					store end time in variable
					if end time on the next day
						set end time to 23:29 of current day
						create new log entry for the next day starting at 00:00 and ending at the end time
							duplicate all other varbiables except for date and start time to new log entry
					if end time is more that 24 hours away from start time
						display error to user and reject end time entry
					if end time is more than 4 hours away from start time
						request user to confirm that the end time is correct
*/
```
###### page 2

```csharp
/*
			activity code
				parse activity code as a hexidecimal number (1 to 13 inclusive)
					use solution here https://stackoverflow.com/questions/8860879/in-c-sharp-is-there-any-datatype-to-store-the-hexadecimal-value
					store activity code in variable
			number of people involved
				if number of people is a positive integer between 1 and 50 inclusive
					store number of people in variable
				else
					prompt user to enter a positive integer between 1 and 50 inclusive
			Notes
				if activity code equals "Other"
					set notes to "Mandatory"
				else
					notes are "Optional"

				if the user starts to enter a note
					set notes to "Mandatory"

				if notes are "Mandatory"
					check that notes are not empty
					check that notes are not null
					check that notes are not whitespace with no characters
					check that notes are not longer than 80 characters
					check that notes do not contain any commas
				else
					display relevant error to user and reprompt until valid note is entered
		once user has entered all the information correctly create a new log entry object with all the information
		store the log entry object in a list of log entries
		prompt the user to enter another log entry
			if user enters another log entry
				repeat the process
			else
				write all log entries to the console for the user to review
				write all log entries to the file
				save and close the file
				write a goodby message to the console
				close the program
else
	close the program
*/
```
> Note.cs
```csharp
/*
define a class for note that contains:
	a string variable to hold the note information
	a boolean varaiable to indicate whether the note is mandatory or optional
*/
```
> LogEntry.cs
```csharp
/*
define a class for logEntry that contains variables for:
	datetime date
	datetime startTime
	datetime endTime
	int howManyPeople
	int activityCode
	Note note
*/
```
###### page 3