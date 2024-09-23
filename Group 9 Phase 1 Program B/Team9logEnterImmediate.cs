using System;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;
/*auother : Alewiya Duressa 
 * CS-4500 Project Phase-1 
 * Program B
 * Requirement 
     1. As a user working on  class , I want a program to track my time and log my activity time automatically update my time log file so that I can accurately track the time spent on class activities.
 Integrated development environment (IDE) ) is Visual studio 2022 
 * Resources used 
 * https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-8.0
 * https://stackoverflow.com/questions/13762338/read-files-from-a-folder-present-in-project
 * https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
 * https://jonathancrozier.com/blog/how-to-publish-a-dot-net-application-as-a-standalone-executable-file#google_vignette
 * https://stackoverflow.com/questions/78286718/local-git-commit-in-visual-studio-2022-results-in-the-pipe-has-been-ended
 */
namespace Group_9_Phase_1_Program_B
{
    public class Team9logEnterImmediate
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Time Entry Log Program!");
            Console.WriteLine("This program is time logging tool that allows users to track time spent on different activities for CS 4500 class.");
            Console.WriteLine("The program enables user to start and stop a virtual clock, and it automatically logs the activity details into a CSV file,including the start and end times,the number of people involved,the activity Code,and optional notes.");
            Console.WriteLine("Press Enter to Proccede:");
            Console.ReadKey();
            //Initialize the logger and user info
            TimeEntryLogger logger = new TimeEntryLogger();
            //locate and open LastnameFirstNameLog.cvs file
            logger.locateLogFile();
            //create obj from userActivity class to accept all user input 
            UserActivity activity = new UserActivity();
            activity.PickActivityCode();
            activity.EnterNumberOfparticipant();
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
            timeLogFileUpdater.StopLogging();

        // Handle midnight crossing and log the entry
        //Append changes 
        logger.LogTimeEntry(timeLogFileUpdater, activity);

        // End of program
        Console.WriteLine("Your log entry has been added. Thank you!");

        }
        /* A Class that handle log file Opreation
         - search and Locate log file in the current derictory
         - Open log file and validate the format
         - Read some properties of the file 
         - write new activity/log entry to the existig log file
         - Append new information
         */

        class TimeEntryLogger
        {
            private string logFileName;
            //method for searching the log file in the current directory
            public void locateLogFile()
            {
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

                    //string[] userInfo = logFileLines[0].Split(',');
                    // Extract Lastname, Firstname from firstline and Class ID from the second line
                    string lastname = firstLineData[0];
                    string firstname = firstLineData[1];
                    string classID = secondLineData[0];
                    //Console.WriteLine($"Lastname: {userInfo[0]} Firstname: {userInfo[1]}, Class ID: {logFileLines[1]}");
                    // Promote user information to screen
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
            //method for adding/appending activityLog to the file
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
                    TimeLogFileUpdater firstTimeEntry = new TimeLogFileUpdater { StartTime = TimeLogFileUpdater.StartTime, EndTime = endOfDay };
                    AppendLog(firstTimeEntry, activity);
                    //create a new TimeLogFileUpdater object for the second time entry / for the next day
                    TimeLogFileUpdater secondTimeEntry = new TimeLogFileUpdater { StartTime = startOfNextDay, EndTime = TimeLogFileUpdater.EndTime };
                }
                else
                {
                    // The entry did not span midnight
                    AppendLog(TimeLogFileUpdater, activity);
                }
            }
            //private void AppendLog(DateTime startTime,DateTime endTime, UserActivity activity)
            private void AppendLog(TimeLogFileUpdater TimeLogFileUpdater, UserActivity activity)
            {
                using (StreamWriter sw = File.AppendText(logFileName))
                {
                    string logEntry = $"{TimeLogFileUpdater.StartTime},{TimeLogFileUpdater.EndTime},{activity.ParticipantNumber},{activity.Note}";
                    sw.WriteLine(logEntry);
                }
                Console.WriteLine($"Log entry added: Start: {TimeLogFileUpdater.StartTime}, End: {TimeLogFileUpdater.EndTime}, Participants: {activity.ParticipantNumber}, Notes: {activity.Note}");
            }
        }
        /*Enum mixing char and number value 
         * https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
         */
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
            Instructormeeting = 65,
            Minilecture = 66,
            ViewingSupplementVideo = 67,
            Other = 68
        }
        /*class that handle user Interaction 
         * accept user input and validate
          */

        class UserActivity
        {
            public int ParticipantNumber { get; private set; }
            public ActivityCode ActivityCode { get; private set; }
            public string Note { get; private set; }
            // public UserActivity(ActivityCode activityCode, int numberParticipant) { }
            //Method for handng Picking activity code for the entry 
            public void PickActivityCode()
            {
                bool validActivity = false;
                //force user to enter activity code till the correct one selected 
                while (!validActivity)
                {
                    //Dislay the option to select from 
                    Console.WriteLine("Select activity code for the entry: ");
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
                        Console.WriteLine("Invalid activity code. Please select correct activity code again.");
                    }
                }
            }
            //Method for handing number of people involved in this acticity entry 
            //accept number from user and validate
            public void EnterNumberOfparticipant()
            {
                bool validParticiantCount = false;
                while (!validParticiantCount)
                {
                    Console.WriteLine("Enter the number of people involved in this activity (1-50) : ");
                    if (int.TryParse(Console.ReadLine(), out int noPeople) && noPeople > 0 && noPeople <= 50)
                    {
                        //set input value to field 
                        ParticipantNumber = noPeople;
                        validParticiantCount = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of people. Please enter a number between 1 and 50.");
                    }
                }
            }
            /*method to handle note 
             Required only if the activityCode value is D/Other
             */

            public void AddNote() {
                //set note to empty string by defualt 
                Note = "";
                // check weather the activityCode is D/"Other"
                if (ActivityCode == ActivityCode.Other) 
                {
                    bool validNotes = false;
                    while (!validNotes)
                    {
                        Console.WriteLine("Enter notes (max 80 characters, no commas): ");
                        //set not to what user entered 
                        Note = Console.ReadLine();
                        //validate the legth and comma character
                        if (Note.Length <= 80 && !Note.Contains(","))
                        {
                            validNotes = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid notes. Ensure it's under 80 characters and contains no commas.");
                        }
                    }
                }
                // null
                Console.WriteLine();
            }

        }
       // class to handle logging the time 
       // set strat time and end time using system.clock
        class TimeLogFileUpdater
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }

            //start counting the time
            public void StartLogging()
            {
                StartTime = DateTime.Now;
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
