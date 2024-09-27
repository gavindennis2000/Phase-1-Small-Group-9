/* 
 * C# programing language used 
 * Auother : Alewiya Duressa 
 * CS-4500 Project Phase-1 
 * Group - 9
 * Team members : Sam Irvin, Gavin Dennis
 * Program B
 * StartWorkingon : 9/20/2024
 * Integrated development environment (IDE) ) is Visual studio 2022
 * Requirement 
     1. As a user working on  class , I want a program to track my time and log my activity time automatically update my time log file so that I can accurately track the time spent on class activities.
 *This program allows users to track time spent on different activities for the CS 4500 class. 
 *The user can start and stop a timer, which records the time spent on each activity.The date data, along with other information the user provides such as activityCode, number of paticipant and note, is automatically appended to a CSV file in real time. 
 *This program used an external file LastnameFirstName.cvs and when it's runs it pcik that file to update and read information. The file located in the same directory as the executable(.exe)
 * The structure of the program include 4  different class 
 *  - Team9logEnterImmediate class that have the main method and it's where all class instance created and methods called
 *  - TimeEntryLogger Class have methods that deal with searching the log.cvs file, validating the formate & opening the file, and appnding the new line entry to the file.
 *  - Activity Code Enum that hold activityCode opitions with there value 
 *  - UserActivity Class have methods that deal with user interaction like accepting input from user like activity code, number of participant,and any information requiredfor the entry. 
 *  - TimeLogFileUpdater Class have mehtods that deal with setting startTime and EndTime behid the scene using system virtual colock
 *  To compile,build, and run this program follow the below steps 
 *    - To compile it requires .NET SDK is installed on your machine and the version need to be Net 6.0 or  above  
 *    - open command line and navigate to folder where the project reside 
 *    - Put dotnet restore command to restore external file 
 *    - dotnet build to build the project 
 *    - dotnet run to run the program
 *  To run get to the exe file, open the  folder project located and navigate to bin\Release\net6.0 then click on  Group 9 Phase 1 Program B.exe/app; It will start the program  
 * Resources used 
 * https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-8.0: how to write line in file
 * https://stackoverflow.com/questions/13762338/read-files-from-a-folder-present-in-project: how to access file from derictory
 * https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum : how to use mixed value for enum value
 * https://jonathancrozier.com/blog/how-to-publish-a-dot-net-application-as-a-standalone-executable-file#google_vignette : how to get the exe
 * https://stackoverflow.com/questions/11107465/getting-only-hour-minute-of-datetime used to set start&end time to hour:minute 
 * https://stackoverflow.com/questions/78286718/local-git-commit-in-visual-studio-2022-results-in-the-pipe-has-been-ended
 */
using System;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;
namespace Group_9_Phase_1_Program_B
{
    //a class that holds the main method where all the methods from the 3 class will be called here accordinglly 
    public class Team9logEntryImmediate
    {
        /* Show program descrtiption 
         * allow user to start the timmer
         * Instantiate object for the 3 class
         * call methods from those object 
         * confirm the user want to finish logactivity 
         * Terminat the program 
         */
        public static void Main(string[] args)
        {
            //Desciription what the pogram does 
            Console.WriteLine("Welcome to Time Entry Log Program!");
            Console.WriteLine("This program is time logging tool that allows users to track time spent on different activities for CS 4500 class.");
            Console.WriteLine("The program enables user to start and stop a virtual clock, and it automatically logs the activity details into a CSV file,including the date,the start and end times,the number of people involved,the activity Code,and optional notes.");
            Console.Write("Press Enter to Proceed:");
            //accept input/ enter 
            Console.ReadKey();
            //Initialize the logger and user info
            TimeEntryLogger logger = new TimeEntryLogger();
            //locate and open LastnameFirstNameLog.cvs file
            logger.locateLogFile();
            //create obj from userActivity class to accept all user input 
            UserActivity activity = new UserActivity();
            activity.PickActivityCode();
            //enter number of pepole involved 
            activity.EnterNumberOfparticipant();
            //add note
            activity.AddNote();
            //create obj from TimeLogFileUpdaterStart the time log 
            TimeLogFileUpdater timeLogFileUpdater = new TimeLogFileUpdater();
            //star the timmer and set startTime
            timeLogFileUpdater.StartLogging();
            Console.WriteLine("Press ENTER when the activity is finished.");
            // Wait for the user to finish
            Console.ReadLine();
            // Prompt user a confirmation that the activity is finsihed 
            bool isFinshied = false;
            while (!isFinshied)
            {
                Console.Write("Are you sure the activity has finished? (Y/N):");
                string confirmationInput = Console.ReadLine().Trim().ToUpper();

                if (confirmationInput == "Y")
                {
                    // Set the confirmation to true and exit the loop
                    isFinshied = true;
                    Console.WriteLine("Activity confirmed as finished.");
                }
                else if (confirmationInput == "N")
                {
                    Console.WriteLine("Please continue with the activity. Press ENTER when finished.");
                    Console.ReadLine(); // Wait for the user to finish again
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y for yes or N for no.");
                }
            }
            //stop logging and set end time
            timeLogFileUpdater.StopLogging();

            // Handle midnight crossing and log the entry
            //Append changes 
            logger.LogTimeEntry(timeLogFileUpdater, activity);

            // End of program
            Console.WriteLine("Your log entry has been added successfully!");
            Console.ReadKey();

        }
        /* A Class that handle log file Opreation
         - search and Locate log file in the current derictory
         - Open log file and validate the format
         - Read some properties of the file 
         - Display the properties from the file to the screen
         - Update the Log file by adding the new line entry
          
         */

        class TimeEntryLogger
        {
            //filename
            private string logFileName;
            /*method for searching the log file in the current directory
             *Validate the file loated in current directory
             *validate the file has the correct format 
             *Open the file 
             *get lastname firstname, and classId from file and print to screen
             *
             */
            public void locateLogFile()
            {
                //LastnameFirstNameLog.Cvs format pattern
                string pattern = @"^[A-Za-z]+[A-Za-z]+Log\.csv$";
                //get the file in current directory that's cvs type 
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*Log.csv");
                // Filter files based on pattern
                var matchingFiles = files.Where(file => Regex.IsMatch(Path.GetFileName(file), pattern)).ToArray();

                // Handle file count and validate the 
                if (matchingFiles.Length > 1)
                {
                    Console.WriteLine("Error: More than one file matches the pattern. Please ensure there's only one.");
                    // return;
                    Environment.Exit(0);
                }
                else if (matchingFiles.Length == 0)
                {
                    Console.WriteLine("Error: No log file found matching the required pattern.");
                    //return;
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("File Located successfully!");
                }
                logFileName = matchingFiles[0];
                // Open and validate the log file
                try
                {
                    string[] logFileLines = File.ReadAllLines(logFileName);
                    if (logFileLines.Length < 2 || !logFileLines[0].Contains(","))
                    {
                        Console.WriteLine("Error: Log file is not in the correct format.");
                        Environment.Exit(0);
                    }
                    // Check format of the first two lines
                    var firstLineData = logFileLines[0].Split(',');
                    var secondLineData = logFileLines[1].Split(',');
                    if (firstLineData.Length < 2 || secondLineData.Length < 2)
                    {
                        Console.WriteLine("Error: Each line must contain at least two data items.");
                        Environment.Exit(0);
                    }

                    // Extract Lastname, Firstname from firstline and Class ID from the second line
                    string lastname = firstLineData[0];
                    string firstname = firstLineData[1];
                    string classID = secondLineData[0];
                    // Show user information to screen
                    Console.WriteLine($"Lastname: {lastname}");
                    Console.WriteLine($"Firstname: {firstname}");
                    Console.WriteLine($"Class ID: {classID}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Could not open or read log file. {ex.Message}");
                    Environment.Exit(0);
                }
            }
            /*method for adding/appending activityLog to the file
             - method takes to areguments TimeLogFileUpdater obj and UserActivity obj
            - check wether time spans mid night and create second entr for it
             - Append the new entry/entries to log file 
             */
            public void LogTimeEntry(TimeLogFileUpdater TimeLogFileUpdater, UserActivity activity)
            {
                // Handle midnight crossing
                if (TimeLogFileUpdater.StartTime.Date != TimeLogFileUpdater.EndTime.Date)
                {
                    // The etry  spanned midnight
                    //https://stackoverflow.com/questions/13467230/how-to-set-time-to-midnight-for-current-day - used as reference for setting date
                    var endOfDay = new DateTime(TimeLogFileUpdater.StartTime.Year, TimeLogFileUpdater.StartTime.Month, TimeLogFileUpdater.StartTime.Day, 23, 59, 59);
                    var startOfNextDay = new DateTime(TimeLogFileUpdater.EndTime.Year, TimeLogFileUpdater.EndTime.Month, TimeLogFileUpdater.EndTime.Day, 0, 0, 0);
                    /*set StartTime and EndTime
                     Create a new TimeLogFileUpdater for the first entry*/
                    TimeLogFileUpdater firstTimeEntry = new TimeLogFileUpdater { LogDate = TimeLogFileUpdater.LogDate, StartTime = TimeLogFileUpdater.StartTime, EndTime = endOfDay };
                    AppendLog(firstTimeEntry, activity);
                    //create a new TimeLogFileUpdater object for the second time entry / for the next day
                    TimeLogFileUpdater secondTimeEntry = new TimeLogFileUpdater { LogDate = startOfNextDay.Date.ToString("dd/MM/yyyy"), StartTime = startOfNextDay, EndTime = TimeLogFileUpdater.EndTime };
                    AppendLog(secondTimeEntry, activity);
                }
                else
                {
                    // The entry did not span midnight
                    AppendLog(TimeLogFileUpdater, activity);
                }
            }
            /*append the new entry information to log file 
              method accept two arguments , TimeLogFileUpdater object  and UserActivity obj
              get activiyCodeValue form ActivityCode enum
              Appended the new entry with gathered information 
             */
            private void AppendLog(TimeLogFileUpdater TimeLogFileUpdater, UserActivity activity)
            {
                //enum shows the string to string to hold activityCode.valuee 
                string activityCodeValue;

                // If the ActivityCode is in the numeric range (1-9), convert it to an integer
                if ((int)activity.ActivityCode >= 1 && (int)activity.ActivityCode <= 9)
                {
                    activityCodeValue = ((int)activity.ActivityCode).ToString();
                }
                // If the ActivityCode is in the alphabetic range (A-D), convert it to the corresponding character
                else
                {
                    activityCodeValue = ((char)activity.ActivityCode).ToString();
                }
                //append new entry to lof file
                using (StreamWriter sw = File.AppendText(logFileName))
                {
                    string logEntry = $"{TimeLogFileUpdater.LogDate},{TimeLogFileUpdater.StartTime.ToString("HH:mm")},{TimeLogFileUpdater.EndTime.ToString("HH:mm")},{activity.ParticipantNumber},{activityCodeValue},{activity.Note}";
                    sw.WriteLine(logEntry);
                }
                Console.WriteLine($"Log entry added: Date:{TimeLogFileUpdater.LogDate}, Start: {TimeLogFileUpdater.StartTime.ToString("HH:mm")}, End: {TimeLogFileUpdater.EndTime.ToString("HH:mm")}, Participants: {activity.ParticipantNumber}, ActivityCode: {activityCodeValue}, Notes: {activity.Note}");
            }
        }
        /*Enum mixing char and number value 
         * https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
         */
        //activityCode type with value  
        public enum ActivityCode
        {
            Readingtextbook = 0,
            PracticeQuiz = 1,
            ScoringQuiz = 2,
            DisscussonBoard = 3,
            TeamMeeting = 4,
            Documentation = 5,
            Designs = 6,
            Programming = 7,
            Testingtestplan = 8,
            FinalExam = 9,
            Instructormeeting = 'A', // 65,
            Minilecture = 'B',// ASCII66,
            ViewingSupplementVideo = 'C', //67,
            Other = 'D' //68
        }
        /*class that handle user Interaction 
         * accept user input and validate
         * set activityCode
         * validate and set Note 
         * accept, validate, and set  people Involved in activity 
          */

        class UserActivity
        {
            public int ParticipantNumber { get; private set; }
            public ActivityCode ActivityCode { get; private set; }
            public string Note { get; private set; }

            /*Method for handling Picking activity code for the entry 
             * accept user input for actiityCode
             * validate the input is in the activityCode List
             * set user selected value to activity code
             */
            public void PickActivityCode()
            {
                bool validActivity = false;
                //force user to enter activity code till the correct one selected 
                while (!validActivity)
                {
                    //Dislay the option to select from 
                    Console.WriteLine("Select activity code for the entry (0-D): ");
                    Console.WriteLine(" 0:Reading Canvas site or textbook");
                    Console.WriteLine(" 1:Studying using a practice quiz");
                    Console.WriteLine(" 2:Taking a scoring quiz");
                    Console.WriteLine(" 3:Participating in a Canvas discussion, DX");
                    Console.WriteLine(" 4:Meeting with your team");
                    Console.WriteLine(" 5:Working on documentation");
                    Console.WriteLine(" 6:Working on designs");
                    Console.WriteLine(" 7:Programming");
                    Console.WriteLine(" 8:Working on a test plan or doing testing");
                    Console.WriteLine(" A:Conferring with the instructor(outside team meeting)");
                    Console.WriteLine(" B:Working on a mini - lecture video or reading");
                    Console.WriteLine(" C: Viewing a video or website that is not a mini - lecture, but relevant to the course.");
                    Console.WriteLine(" D: Other");
                    //input is nullable 
                    string input = Console.ReadLine().ToUpper();
                    //https://www.geeksforgeeks.org/out-parameter-with-examples-in-c-sharp/
                    //out to reference the ascii value of the character
                    if (int.TryParse(input, out int number) && Enum.IsDefined(typeof(ActivityCode), number))
                    {
                        //set the activiy code 
                        ActivityCode = (ActivityCode)number;
                        validActivity = true;
                    }
                    //Checks whether the input string has exactly one character for activity Code with Char (A,B,C,D) and
                    // the selecting chara value is in Enum
                    else if (input.Length == 1 && Enum.IsDefined(typeof(ActivityCode), (int)input[0]))
                    {
                        ActivityCode = (ActivityCode)(int)input[0];
                        validActivity = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid activity code. Please select the correct activity code again.");
                    }
                }
            }
            /*Method for handing number of people involved in this acticity entry 
            accept number from user and validate it is b/n 1 to 50 
             If input is > 50 repromote user to enter the correct value 
            set the value to ParticipantNumber*/
            public void EnterNumberOfparticipant()
            {
                //foce user to enter value b/  to 50
                bool validParticiantCount = false;
                while (!validParticiantCount)
                {
                    Console.WriteLine("Enter the number of people involved in this activity (1-50) : ");
                    //change userinput string to int and check if input is >0 and <50
                    if (int.TryParse(Console.ReadLine(), out int noPeople) && noPeople > 0 && noPeople <= 50)
                    {
                        //set input value to field 
                        ParticipantNumber = noPeople;
                        //Stop loop
                        validParticiantCount = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of people. Please enter a number between 1 and 50.");
                    }
                }
            }
            /*method to handling note 
             Required only if the activityCode value is D (other) 
             note should be no more than 80 character 
            set note value 

             */
            public void AddNote()
            {
                // check weather the activityCode is D/"Other"
                if (ActivityCode == ActivityCode.Other)
                {
                    bool validNotes = false;
                    while (!validNotes)
                    {
                        Console.WriteLine("Enter notes (max 80 characters, no commas): ");
                        //set not to what user entered 
                        Note = Console.ReadLine();
                        if (Note == "")
                        {
                            Console.WriteLine("You pressed Enter Key without entering value.Please enter Note value!");
                        }
                        //validate the legth and comma character
                        else if (Note.Length <= 80 && !Note.Contains(","))
                        {
                            validNotes = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid note. Ensure it's under 80 characters and contains no commas.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Enter notes (max 80 characters, no commas): ");
                    // Set Note to what user entered
                    Note = Console.ReadLine();
                    //check if usr enter some note validate it. 
                    if (!string.IsNullOrWhiteSpace(Note))
                    {
                        bool optionalNoteValid = false;
                        while (!optionalNoteValid)
                        {
                            if (Note.Length <= 80 && !Note.Contains(","))
                            {
                                optionalNoteValid = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid note. Ensure it's under 80 characters and contains no commas.");
                                Note = Console.ReadLine(); // Allow user to re-enter
                            }
                        }
                    }
                }
            }

        }
        // class to handle logging the time automatically
        // set strat time , log date and end time using datetime
        class TimeLogFileUpdater
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string? LogDate { get; set; }

            //start  the timmer and set the date and startTime
            public void StartLogging()
            {
                StartTime = DateTime.Now;
                //set the date 
                LogDate = DateTime.Today.ToShortDateString();
                Console.WriteLine($"Working on this activity started at: {StartTime}");
            }
            //stop the virtual clock and set the value to endTime 
            public void StopLogging()
            {
                // Stop logging the time
                EndTime = DateTime.Now;
                Console.WriteLine($"Working on this activity ended at: {EndTime}");
            }
        }

    }
}
