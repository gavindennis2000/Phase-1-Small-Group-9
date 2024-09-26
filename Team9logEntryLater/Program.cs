using System;
using System.IO;

namespace Team9logEntryLater
{
	internal class Program
	{
		static void Main(string[] args)
		{
			bool pause = true;
			string lastName = string.Empty;
			string firstName = string.Empty;
			string fileName;
			string filePath;
			string currentDirectory = Directory.GetCurrentDirectory();
			string projectDirectory = Directory.GetParent(currentDirectory).Parent.FullName;

			Console.WriteLine("Welcome! \n" +
				"This program allows you to edit logs that track activities.");

			while (pause) {
				Console.WriteLine("\nPress any key to continue...");

				ConsoleKeyInfo keyValue = Console.ReadKey(true);

				if (keyValue != null)
				{
					pause = false;
				}
			}

			do
			{
				Console.WriteLine("\nPlease enter your first name: ");
				firstName = Console.ReadLine();
			} while (string.IsNullOrEmpty(firstName));

			do
			{
				Console.WriteLine("\nPlease enter your last name: ");
				lastName = Console.ReadLine();
			} while (string.IsNullOrEmpty(lastName));

			fileName = lastName + firstName + "Log.csv";
			filePath = Path.Combine(projectDirectory, fileName);

			Console.WriteLine("\nSearching for File: " + fileName);

			if (File.Exists(filePath))
			{
				// Check if there are multiple files that contain the same name, if so, inform the user and exit the program
				if (Directory.GetFiles(projectDirectory, "*" + lastName + firstName + "*").Length > 1) {
					Console.WriteLine("There were multiple files found for that user. \n \n " +
						"Please update the file names so only one file with the name " + fileName + " exists then try again. \n \n");

					Console.WriteLine("Press any key to exit...");
					Console.ReadKey(true);
				} else
				{
					Console.WriteLine("\nFile found. Opening...");
					try
					{
						using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
						using (StreamReader streamReader = new StreamReader(fileStream))
						{
							string line = string.Empty;
							while ((line = streamReader.ReadLine()) != null)
							{
								Console.WriteLine(line);
							}
							Console.WriteLine("\nSuccessfully Opened " + fileName);
							Console.WriteLine("\nPress any key to continue...");
							Console.ReadKey(true);
						}
					} catch (Exception ex)
					{
						Console.WriteLine("An error occurred while reading the file: " + ex.Message);
					}

				}
			}
			else
			{
				Console.WriteLine("\nSorry, no file found for that user.");
				Console.WriteLine("\nPress any key to exit...");
				Console.ReadKey(true);
			}

			Console.WriteLine("Success");
			Console.ReadKey(true);
		}
	}
}
