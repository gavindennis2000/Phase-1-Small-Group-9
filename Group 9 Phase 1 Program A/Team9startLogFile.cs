using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CSVLog;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
}
public class Team9startLogFile
{
    static void WriteToCSV(string filename)
    {
        try
        {
            if (!File.Exists(filename)) 
            {
                using (File.Create(filename)) { }
            }

            List<Person> people = new List<Person>
            {
                new Person { Name = "John", Age = 25, Country = "USA" },
                new Person { Name = "Alice", Age = 30, Country = "Canada" }
            };

            var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            using (StreamWriter streamWriter = new StreamWriter(filename, true))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, configPersons))
            {
                csvWriter.WriteRecords(people);
            }
            Console.WriteLine("Data written to CSV file.");
        }
        catch(Exception ex)
        {
            throw;
        }
    }

    static void Main()
    {
        string fileDirectory = @"C:\Users\Anthony\Documents\GitHub\Phase-1-Small-Group-9\Group 9 Phase 1 Program A\";
        string fileName = "data.csv";
        string filePath = Path.Combine(fileDirectory, fileName);

        WriteToCSV(filePath);
        Console.ReadKey();
  
    }
}
