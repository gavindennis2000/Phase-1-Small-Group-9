# Team9logEntryLater Design Document
This document will outline the design of the Team9logEntryLater application, an application that allows
users to log entries for later viewing.

---

### GroupNumber: 9
### People working on the project: Sam Irvin
### Date of First Design: 9/21/2024
### Dates of Revision:
> 9/22/2024 - added endDate variable to logEntry object and simplified pseudocode to be one-page

---

## Overview

> Team9logEntryLater.cs
```csharp
/* 
define variables to hold user inputs

Outpute a welcome message explaining the purpose of the application

prompt user for an interaction to continue

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
			start date
			start time
			end date
			end time
			activity code
			number of people involved
			Notes
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
###### page 1
