using System;
using System.Collections.Generic;
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

			while (pause)
			{
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
				if (Directory.GetFiles(projectDirectory, "*" + lastName + firstName + "*").Length > 1)
				{
					Console.WriteLine("There were multiple files found for that user. \n \n " +
						"Please update the file names so only one file with the name " + fileName + " exists then try again. \n \n");

					Console.WriteLine("Press any key to exit...");
					Console.ReadKey(true);
				}
				else
				{
					Console.WriteLine("\nFile found. Opening...");
					try
					{
						using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
						using (StreamReader streamReader = new StreamReader(fileStream))
						{
							// Create a list to store the fields by row
							List<string[]> fields = new List<string[]>();
							string line = string.Empty;

							while ((line = streamReader.ReadLine()) != null)
							{
								// Split the line by commas and add it to the fields list
								fields.Add(line.Split(','));
							}
							if (fields != null)
							{
								Console.WriteLine("Author's Name: " + fields[0][1] + " " + fields[0][0]);
								Console.WriteLine("Class ID: " + fields[1][0]);

								Console.WriteLine("\nIs this the correct Author and Class Id?");
								Console.WriteLine("\nYes / No");
								string userResponse = Console.ReadLine();

								if (VerifyUserResponse(userResponse)) {
									Console.WriteLine("Success");
									Console.WriteLine("\nPress any key to exit...");
									Console.ReadKey(true);
								}
                                else
                                {
									Console.WriteLine("\nPlease Verify file name and try again.");
									Console.WriteLine("\nPress any key to exit...");
									Console.ReadKey(true);
								}
                            }
						}
					}
					catch (Exception ex)
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
		}

		static private bool VerifyUserResponse(string userResponse)
		{
			switch (userResponse.ToLower())
			{
				case "yes":
					return true;
				case "y":
					return true;
				default:
					return false;
			}
		}
		//static bool VerifyCorrectFile(string filePath, string fileName)
		//{
		//	if (File.Exists(filePath))
		//	{
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}
	}
}
