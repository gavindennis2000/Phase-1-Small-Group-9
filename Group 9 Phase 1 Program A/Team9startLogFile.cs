using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Program;

//public class Inputs
//{
//    /* 
//    CSV file contains:
//        last name, first name, and class ID - strings
//        date - string in the form MM/DD/YYYY
//        start and end times - times with 24 hour clocks
//        how many people - an integer between 1 and 50
//        activity code - hexidecimal digit (0 - F)
//        notes - string with 80 or fewer characters
//    */
//    public string LastName { get; set; }
//    public string FirstName { get; set; }
//    public string ClassID { get; set; }
//    public string Date { get; set; }
//    public TimeOnly StartTime { get; set; }
//    public TimeOnly EndTime { get; set; }
//    public int HowManyPeople { get; set; }
//    public char ActivityCode { get; set; }
//    public string Notes { get; set; }
//}

public class Team9startLogFile
{
    //static void WriteToCSV(string filename)
    //{
    //    try
    //    {
    //        if (!File.Exists(filename)) 
    //        {
    //            using (File.Create(filename)) { }
    //        }

    //        List<Person> people = new List<Person>
    //        {
    //            new Person { Name = "John", Age = 25, Country = "USA" },
    //            new Person { Name = "Alice", Age = 30, Country = "Canada" }
    //        };

    //        var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
    //        {
    //            HasHeaderRecord = true
    //        };

    //        using (StreamWriter streamWriter = new StreamWriter(filename, true))
    //        using (CsvWriter csvWriter = new CsvWriter(streamWriter, configPersons))
    //        {
    //            csvWriter.WriteRecords(people);
    //        }
    //        Console.WriteLine("Data written to CSV file.");
    //    }
    //    catch(Exception ex)
    //    {
    //        throw;
    //    }
    //}

    static void BeginProgram()
    {
        // prints to the screen a description of what the program does and
        // waits for an input to continue

        Console.WriteLine(
            "Start Log File Program.\n" +
            "Programmed by Gavin Dennis\n\n" +
            "This program creates a CSV file time log. It will prompt you for your last name, first name, class ID, d" + 
            "Press any key to continue. "
        );

        Console.ReadKey(); 

    }

    static void Main()


    {

        //begin the program
        BeginProgram();
        Console.WriteLine("Works");
        Console.ReadKey();

        //string fileDirectory = @"C:\Users\Anthony\Documents\GitHub\Phase-1-Small-Group-9\Group 9 Phase 1 Program A\";
        //string fileName = "data.csv";
        //string filePath = Path.Combine(fileDirectory, fileName);

        //WriteToCSV(filePath);
        //Console.ReadKey();

    }
}
