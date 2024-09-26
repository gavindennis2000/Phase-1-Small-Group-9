# Team9logEntryLater Test Design Document
This document will outline the Testing design for the Team9logEntryLater application, an application that allows
users to log entries for later viewing.

---

### GroupNumber: 9
### People working on the project: Sam Irvin
### Date of First Design: 9/22/2024
### Dates of Revision:
> 9/25/2024 - Updated name of test file to match actual file name

---

## Overview
This application will have built in testing supported by Moq. Unit tests will be written for each method in the application.
The tests will be written to test the functionality of the methods and to ensure that the methods are working as expected.

> Team9logEntryLaterTests.cs
```csharp
/*
Given file exists in the directory with the name "TestFileLog.csv"
Given file exists in the directory with the name "DuplicateFileLog.csv"
Given file exists in the directory with the name "DuplicateFile1Log.csv"
Given file exists in the directory with the name "1DuplicateFileLog.csv"

- Expect function given last name "Test" and first name "File" will return true (File exists)
- Expect function given last name "Missing" and first name "Test" will return false (File does not exist)
- Expect function given last name "Duplicate" and first name "File" will return false (Multiple files with the same name exist))

- Expect validate firstName function given first name 1234 will return false (Input should only contain letters)
- Expect validate firstName function given first name "Iman" will return true (Input should only contain letters)
- Expect validate firstName function given first name "" will return false (Input should not be empty)
- Expect validate firstName function given first name null will return false (Input should not be null)

- Expect validate lastName function given last name 1234 will return false (Input should only contain letters)
- Expect validate lastName function given last name "Example" will return true (Input should only contain letters)
- Expect validate lastName function given last name "" will return false (Input should not be empty)
- Expect validate lastName function given last name null will return false (Input should not be null)

- Expect validate date function given date 9/22/2024 will return true (Input should be a valid date in MM/DD/YYYY format)
- Expect validate date function given date 22/9/2024 will return false (Input should be a valid date in MM/DD/YYYY format)

- Expect validate startTime function given startTime 12:00 will return true (Input should be a valid time in HH:MM format)
- Expect validate startTime function given startTime 1:00 PM will return false (Input should be a valid time in 24 hour format)
- Expect validate startTime given "12:00" will return false (Input should be an integer)

- Expect validate endTime function given endTime 12:00 will return true (Input should be a valid time in HH:MM format)
- Expect validate endTime function given endTime 1:00 PM will return false (Input should be a valid time in 24 hour format)
- Expect validate endTime given "12:00" will return false (Input should be an integer)

- Expect validate workDuration with a startDate of 1/1/2024, startTime of 12:00, endDate 1/1/2024, endTime of 16:00 should return true (Work duration is valid)
- Expect validate workDuration with a startDate of 1/1/2024, startTime of 12:00, endDate 1/2/2024 endTime of 12:00 should return false (Work duration is invalid)
- Expect validate workDuration with a startDate of 1/1/2024, startTime of 12:00, endDate 1/1/2024 endTime of 13:00 should return false (Work duration is invalid)

- Expect logEntry given startDate of 1/1/2024, startTime of 12:00, endDate 1/1/2024, endTime of 13:00 to add one log entry to the logEntries list
- Expect logEntry given startDate of 1/1/2024, startTime of 23:00, endDate 1/2/2024, endTime of 01:00 to add two log entries to the logEntries list
  - Expect first added log entry to have a start date of 1/1/2024, start time of 23:00, end date of 1/1/2024, end time of 23:59 and all other fields to be user entered values
  - Expect second added log entry to have a start date of 1/2/2024, start time of 00:00, end date of 1/2/2024, end time of 01:00 and all other fields match log the first log entry

- Expect validate activityCode given string 1 will return int 1 (Input is converted from string to int)
- Expect validate activityCode given string "A" will return int 10 (Input converted from string to int)
- Expect validate activityCode given string "Z" will return error (Input out of range)
- Expect validate activityCode given string "" will return error (Input is empty)
- Expect validate activityCode given string "a" will return error (Only capital letters allowed)

- Expect validate howManyPeople given 1 will return true (Input is between 1 and 50 inclusive)
- Expect validate howManyPeople given 0 will return false (Input is less than 1)
- Expect validate howManyPeople given 51 will return false (Input is greater than 50)
- Expect validate howManyPeople given "1" will return false (Input must be integer)

- Expect validate note given "This is a test note" will return true (Input is a valid note)
- Expect validate note given "This is a test note, with a comma" will return false (Input cannot contain commas)
- Expect validate note given "This is a test note that is too long and contains more than 80 characters and this note continues to run on and on and on" 
	will return false (Input cannot be greater than 80 characters)
- Expect validate note given "" will return false (Input should not be empty)
- Expect validate note given null will return false (Input should not be null)
- Expect validate note to be Mandatory given activityCode is "D"
- Expect validate note to be Optional given activityCode is "A"
- Expect validate note to be Optional, then given note input, note should be updated to be Mandatory
*/
```
###### page 1