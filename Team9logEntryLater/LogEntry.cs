using System;
using System.Globalization;
using System.Security.Cryptography;

namespace Team9logEntryLater
{
	internal class LogEntry
	{
		private DateTime? _startDate;
		private DateTime? _startTime;
		private DateTime? _endDate;
		private DateTime? _endTime;
		private int _howManyPeople = 0;
		private int? _activityCode = null;

		public LogEntry()
		{
			Note = new Note();
		}

		public DateTime? StartDate
		{
			get { return _startDate; }
			set { _startDate = value.HasValue ? value.Value.Date : (DateTime?)null; }
		}
		public DateTime? StartTime
		{
			get { return _startTime; }
			set { _startTime = value; }
		}
		public DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value.HasValue ? value.Value.Date : (DateTime?)null; }
		}
		public DateTime? EndTime
		{
			get { return _endTime; }
			set { _endTime = value; }
		}
		public int HowManyPeople
		{
			get { return _howManyPeople; }
			set
			{
				if (value > 50 || value < 1) { 
					throw new ArgumentOutOfRangeException("Must have at least one participant and no more than 50"); 
				} else
				{
					_howManyPeople = value;
				}
			}
		}
		public int? ActivityCode
		{
			// code inspiration taken from https://stackoverflow.com/questions/8860879/in-c-sharp-is-there-any-datatype-to-store-the-hexadecimal-value
			get { return _activityCode; }
			set
			{
				if (value >= 0x0 && value <= 0xD)
				{
					_activityCode = value;
				}
				else
				{
					throw new ArgumentOutOfRangeException("Activity Code must be a hexidecimal value between 0 and D");
				}
			}
		}
		public Note Note { get; set; }
	}
}
