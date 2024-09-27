using System;
using System.Collections.Generic;
using System.IO;

namespace Team9logEntryLater
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string lastName = string.Empty;
			string firstName = string.Empty;
			string fileName;
			string filePath;
			string userResponse;
			// code inspiration taken from https://stackoverflow.com/questions/816566/how-do-you-get-the-current-project-directory-from-c-sharp-code-when-creating-a-c
			string currentDirectory = Directory.GetCurrentDirectory();
			string projectDirectory = Directory.GetParent(currentDirectory).Parent.FullName;
			List<LogEntry> logEntries = new List<LogEntry>();


			Console.WriteLine("Welcome! \n" +
				"This program allows you to edit logs that track activities.");

			Console.WriteLine("\nPress any key to continue...");
			Console.ReadKey(true);

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

			// Creates file name and path based on user input
			fileName = lastName + firstName + "Log.csv";
			filePath = Path.Combine(projectDirectory, fileName);

			Console.WriteLine("\nSearching for File: " + fileName);

			// file access code taken from https://learn.microsoft.com/en-us/dotnet/api/system.io.file.open?view=net-8.0
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
								userResponse = Console.ReadLine();

								if (VerifyUserResponse(userResponse))
								{
									do
									{
										LogEntry logEntry = new LogEntry();

										// Gather Start Date, Start Time, End Date, and End Time
										do
										{
											Console.WriteLine("\nPlease enter the start date (MM/DD/YYYY): ");
											string input = Console.ReadLine();
											if (DateTime.TryParse(input, out DateTime parsedDate))
											{
												logEntry.StartDate = parsedDate;
											}
											else
											{
												Console.WriteLine("Invalid date. Please try again.");
												logEntry.StartDate = null;
											}
										} while (logEntry.StartDate == null);
										do
										{
											Console.WriteLine("\nPlease enter the start time in 24 hr format (HH:MM): ");
											string input = Console.ReadLine();
											if (DateTime.TryParse(input, out DateTime parsedTime))
											{
												logEntry.StartTime = parsedTime;
											}
											else
											{
												Console.WriteLine("Invalid time. Please try again.");
												logEntry.StartTime = null;
											}
										} while (logEntry.StartTime == null) ;
										do
										{
											Console.WriteLine("\nPlease enter the end date (MM/DD/YYYY): ");
											string input = Console.ReadLine();
											if (DateTime.TryParse(input, out DateTime parsedDate))
											{
												// check if the end date is before the start date and reject the end date
												if (parsedDate < logEntry.StartDate)
												{
													Console.WriteLine("End date cannot be before the start date. Please enter a valid end date.");
													logEntry.EndDate = null;
												}

												logEntry.EndDate = parsedDate;
											}
											else
											{
												Console.WriteLine("Invalid date. Please try again.");
												logEntry.EndDate = null;
											}
										} while (logEntry.EndDate == null);
										do
										{
											Console.WriteLine("\nPlease enter the end time in 24 hr format (HH:MM): ");
											string input = Console.ReadLine();
											if (DateTime.TryParse(input, out DateTime parsedTime))
											{
												logEntry.EndTime = parsedTime;

												// Combine date and time components for accurate duration calculation
												// code inspiration taken from https://stackoverflow.com/questions/12521366/getting-time-span-between-two-times-in-c
												// and https://stackoverflow.com/questions/3142547/join-date-and-time-to-datetime-in-c-sharp
												DateTime startDateTime = logEntry.StartDate.Value.Date + logEntry.StartTime.Value.TimeOfDay;
												DateTime endDateTime = logEntry.EndDate.Value.Date + logEntry.EndTime.Value.TimeOfDay;

												// Check if the end time is before the start time
												if (endDateTime < startDateTime)
												{
													Console.WriteLine("End Time must be after Start Time.");
													logEntry.EndTime = null;
												}

												// Check if the total time logges is greater than 24 hours and reject the end time
												if ((endDateTime - startDateTime).TotalHours > 24)
												{
													Console.WriteLine("Total time logged is greater than 24 hours. Please enter a valid end time.");
													logEntry.EndTime = null;
												}

												// Check if the total time logged is greater than 4 hours and request user confirmation
												if ((endDateTime - startDateTime).TotalHours < 24 && (endDateTime - startDateTime).TotalHours > 4)
												{
													Console.WriteLine("Total time logged is greater than 4 hours. Is this correct?");
													Console.WriteLine("Yes / No");
													userResponse = Console.ReadLine();

													if (VerifyUserResponse(userResponse))
													{
														Console.WriteLine("End Time confirmed");
													}
													else
													{
														Console.WriteLine("Please enter the end time again.");
														logEntry.EndTime = null;
													}
												}
											}
											else
											{
												Console.WriteLine("Invalid time. Please try again.");
												logEntry.EndTime = null;
											}

										} while (logEntry.EndTime == null) ;

										// Gather Activity Code
										do
										{
											Console.WriteLine("\nPlease enter the Activity Code from the following choices: ");
											Console.WriteLine(" 0 : Reading Canvas site or textbook");
											Console.WriteLine(" 1 : Studying using a practice quize");
											Console.WriteLine(" 2 : Taking a scoring test");
											Console.WriteLine(" 3 : Participating in a Canvas discussion, DX");
											Console.WriteLine(" 4 : Meeting with your team");
											Console.WriteLine(" 5 : Working on documentation");
											Console.WriteLine(" 6 : Working on designs");
											Console.WriteLine(" 7 : Programming");
											Console.WriteLine(" 8 : Working on a test plan or doing testing");
											Console.WriteLine(" 9 : Studying for the final exam");
											Console.WriteLine(" A : Conferring with the instructor (outside team meeting)");
											Console.WriteLine(" B : Working on a mini-lecture video or reading");
											Console.WriteLine(" C : Viewing a video or website that is not a mini-lecture, but is relevant");
											Console.WriteLine(" D : Other\n");

											string input = Console.ReadLine();
											try
											{
												logEntry.ActivityCode = Convert.ToInt32(input, 16);

												//If activity code is D, note is mandatory
												if (logEntry.ActivityCode.Equals(0xD)) { logEntry.Note.IsMandatory = true; }
											}
											catch (Exception ex)
											{
												Console.WriteLine("Activity Code must be a hexidecimal value between 0 and D");
											}
										} while (logEntry.ActivityCode == null);

										// Gather How Many People
										do
										{
											Console.WriteLine("\nPlease enter the number of participants: ");
											string input = Console.ReadLine();
											try
											{
												logEntry.HowManyPeople = Int32.Parse(input);
											}
											catch (Exception ex)
											{
												Console.WriteLine(ex.Message);
											}
										} while (logEntry.HowManyPeople == 0);

										// Gather Note
										do
										{
											if (logEntry.Note.IsMandatory)
											{
												try
												{
													Console.WriteLine("\nPlease enter a note: ");
													string userEntry = Console.ReadLine();
													logEntry.Note.Entry = userEntry;
												}
												catch (Exception ex)
												{
													Console.WriteLine(ex.Message);
												}
											}
											else
											{
												Console.WriteLine("\nWould you like to add a note?: ");
												Console.WriteLine("\nYes / No");
												userResponse = Console.ReadLine();

												if (VerifyUserResponse(userResponse))
												{
													try
													{
														logEntry.Note.IsMandatory = true;
														Console.WriteLine("\nPlease enter a note: ");
														string userEntry = Console.ReadLine();
														logEntry.Note.Entry = userEntry;
													}
													catch (Exception ex)
													{
														Console.WriteLine(ex.Message);
													}
												}
												else
												{
													break;
												}
											}
										} while (logEntry.Note.Entry == null);

										// if the end date is on a different date than the start date, duplicate the log entries and adjust the times
										if (!logEntry.StartDate.Equals(logEntry.EndDate))
										{
											// create a second log entry
											LogEntry logEntry2 = new LogEntry();

											// set the start time of the new log entry to the second day with a start time of 00:00
											logEntry2.StartDate = logEntry.EndDate;
											logEntry2.StartTime = logEntry.EndDate.Value.Date + new TimeSpan(0, 0, 0);

											// set the end time of the new log entry to the second day with an end time of the original end time
											logEntry2.EndDate = logEntry.EndDate;
											logEntry2.EndTime = logEntry.EndTime;

											// set the end time of the original log entry to the first day with an end time of 23:59
											logEntry.EndDate = logEntry.StartDate;
											logEntry.EndTime = logEntry.StartTime.Value.Date + new TimeSpan(23, 59, 59);

											// copy the rest of the log entry data to the new log entry
											logEntry2.HowManyPeople = logEntry.HowManyPeople;
											logEntry2.ActivityCode = logEntry.ActivityCode;
											logEntry2.Note = logEntry.Note;

											// add the new log entries to the log entries list
											logEntries.Add(logEntry);
											logEntries.Add(logEntry2);
										}
										else
										{
											logEntries.Add(logEntry);
										}

										Console.WriteLine("\nWould you like to add another log entry?");
										Console.WriteLine("\nYes / No");
										userResponse = Console.ReadLine();

										if (!VerifyUserResponse(userResponse)) { break; }
									} while (true);
								}
								else
								{
									Console.WriteLine("\nPlease verify the file name and try again.");
									Console.WriteLine("\nPress any key to exit...");
									Console.ReadKey(true);
								}
							}
						}
						Console.WriteLine("\n\n Here are all your new log entries:\n");

						using (StreamWriter streamWriter = new StreamWriter(filePath, append: true))
						{
							foreach (LogEntry entry in logEntries)
							{
								// format the log entry data into a string
								string entryLine = $"{entry.StartDate?.ToString("MM/dd/yyyy")}, {entry.StartTime?.ToString("HH:mm")}, {entry.EndTime?.ToString("HH:mm")}, {entry.HowManyPeople}, {entry.ActivityCode?.ToString("X")}, {entry.Note?.Entry}";

								// display the log entry data to the console then append it to the file
								Console.WriteLine(entryLine);
								streamWriter.WriteLine(entryLine);
							}
						}

						Console.WriteLine("\nYour log entries have been saved to the file: " + fileName);
						Console.WriteLine("\nGoodbye!");
						Console.WriteLine("\nPress any key to exit...");
						Console.ReadKey(true);
					}
					catch (Exception ex)
					{
						Console.WriteLine("An error occurred while reading the file: " + ex.Message);
						Console.WriteLine("\nPress any key to exit...");
						Console.ReadKey(true);
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
	}
}
