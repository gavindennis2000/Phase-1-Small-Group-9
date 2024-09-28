/* C# will be the language used for each program
CS 4050
Group 9 Phase 1 - Gavin Dennis, Alewiya Duressa, and Sam Irvin
Program A
Lead Programmer: Gavin Dennis
Programmed in Microsoft Visual Studio 2022
27 September 2024

The program uses user input to create a time log in the form of a CSV file. It will prompt
the user for his or her last name, first name, and class ID. After which it will call a function
to create and write to a CSV file. An error message will be prompted if a user with the same
name already has CSV file.

I was unable to create a standalone executable for c# due to the nature of the language.
Here are the dependencies:
    Group 9 Phase 1 Program A.runtimeconfig.json
    Group 9 Phase 1 Program A.deps.json
    Group 9 Phase 1 Program A.dll

C# documentation provided by the Microsoft website at 
https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file
*/

using System;
using System.IO;

namespace Program;

public class Inputs
{
    /* 
    CSV file contains:
        last name, first name, and class ID - strings
        date - string in the form MM/DD/YYYY
        start and end times - times with 24 hour clocks
        how many people - an integer between 1 and 50
        activity code - hexidecimal digit (0 - F)
        notes - string with 80 or fewer characters

        Program A only needs lastname, firstname, and class ID.
        These strings should not contain commas due to the
        nature of CSV files
    */
    public string LastName = "Smith";
    public string FirstName = "John";
    public string ClassID = "CS 0000";
}

public class Team9startLogFile
{
    static void WriteToCsv(Inputs input)
    {
        // Use inputted data to write to new Csv file

        // directory and filename
        var dirName = String.Concat(input.LastName, input.FirstName, "Log.csv");
        var directory = @".\";
        directory = Path.Combine(directory, dirName);

        // check if file already exists
        if (File.Exists(directory))
        {
            Console.WriteLine("Error. File already exists. Closing program.");
            return;
        }

        // try and write the csv file
        try
        {           
            // Write to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(directory))
            {
                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < 6; j++)
                    {
                        var str = "";
                        if (i == 0 && j == 0) { str = input.LastName; }
                        else if (i == 0 && j == 1) { str = input.FirstName; }
                        else if (i == 1 && j == 0) { str = input.ClassID; }
                        outputFile.Write(str);
                        outputFile.Write(",");
                    }
                    outputFile.Write("\n");  // new line after everything from the row is written
                }
                outputFile.Close();  // close the file after writing is finished
            }

            // confirm csv creation
            Console.WriteLine("\n" + dirName + " has been created! \nClosing the program.");
            return;
        }
        catch(Exception e)
        {
            // throw exception if CSV output fails
            Console.WriteLine("\nFailed to write to CSV file.");
            Console.WriteLine(e.Message);
        }
    }

    static void BeginProgram()
    {
        // prints to the screen a description of what the program does and
        // waits for an input to continue

        Console.WriteLine(
            "Start Log File Program." +
            "\nProgrammed by Gavin Dennis" +
            "\n\nThis program creates a CSV file time log. It will prompt you for some information, then write it to a CSV file." + 
            "\nNo commas can be used in any of the data inputs due to the nature of CSV files. You will be reprompted if your input contains commas." +
            "\n\n" +
            "\nPress any key to continue. "
        );

        Console.ReadKey(); 

    }

    static void Main()


    {

        //begin the program
        BeginProgram();
        Inputs input = new Inputs();


        // prompt for user data
        string comma = ",";  // variable for comma detection in user inputs

        // last name
        do
        {
            Console.WriteLine("\n\nEnter your last name followed by the \'enter\' key (e.g. 'Smith'): ");  
            input.LastName = Console.ReadLine();

            if (input.LastName.Contains(comma))
            {
                Console.WriteLine("\nError: String cannot contain commas. Try again.");
            }
        } while (input.LastName.Contains(comma)) ;

        // first name
        do
        {
            Console.WriteLine("\nEnter your first name followed by the \'enter\' key (e.g. \'John\'): ");  
            input.FirstName = Console.ReadLine();
            if (input.FirstName.Contains(comma))
            {
                Console.WriteLine("\nError: String cannot contain commas. Try again.");
            }
        } while (input.FirstName.Contains(comma));

        // class ID
        do
        { 
            Console.WriteLine("\nEnter your class followed by the \'enter\' key (e.g. \'CS 4050'): ");  
            input.ClassID = Console.ReadLine();
            if (input.ClassID.Contains(comma))
            {
                Console.WriteLine("\nError: String cannot contain commas. Try again.");
            }
        } while (input.ClassID.Contains(comma));
        // create the csv file
        WriteToCsv(input);

        // end the program
        Console.ReadKey();
        return;

    }
}
