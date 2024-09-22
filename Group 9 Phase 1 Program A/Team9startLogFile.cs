/* Group 9 Phase 1 Program A
Lead Programmer: Gavin Dennis

CsvHelper documentation from:
    Josh Close CsvHelper GitHub
    https://joshclose.github.io/CsvHelper/getting-started/

    IAmTimCorey on YouTube
    https://www.youtube.com/watch?v=z3BwMlcGdhg&t=820s&ab_channel=IAmTimCorey
*/

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

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
    */
    public string LastName;
    public string FirstName;
    public string ClassID;
    public string Date;
    public TimeOnly StartTime;
    public TimeOnly EndTime;
    public int HowManyPeople;
    public char ActivityCode;
    public string Notes;
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

        // try and write the file
        try
        {
            using (var writer = new StreamWriter(directory))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))  // uses default for delimiters on csv files
            {
                csv.WriteRecords(input.FirstName);
            }
            Console.WriteLine("CSV file written.");
        }
        catch(Exception e)
        {
            Console.WriteLine("Failed to write to CSV file.");
            Console.WriteLine(e.Message);
        }
    }

    static void ReadFromCsv(string filename)
    {
        // attempt to read Csv file after it is written.

        using (var reader = new StreamReader(filename))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Foo>();
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
        Console.WriteLine("\n\nEnter your last name followed by the \'enter\' key (e.g. 'Smith'): ");  // last name
        input.LastName = Console.ReadLine();

        Console.WriteLine("\nEnter your first name followed by the \'enter\' key (e.g. \'John\'): ");  // first name
        input.FirstName = Console.ReadLine();

        Console.WriteLine("\nEnter your class followed by the \'enter\' key (e.g. \'CS 4050'): ");  // class ID
        input.ClassID = Console.ReadLine();

        // create the csv file
        WriteToCsv(input);

        // end the program
        Console.ReadKey();

    }
}
