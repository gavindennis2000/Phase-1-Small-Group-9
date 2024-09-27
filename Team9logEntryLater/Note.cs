using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team9logEntryLater
{
	internal class Note
	{
		private bool _isManadatory = false;
		private string _entry = null;

		public bool IsMandatory
		{
			get { return _isManadatory; }
			set { _isManadatory = value; }
		}

		public string Entry
		{
			get { return _entry; }
			set
			{
				if (value.Length > 80) throw new ArgumentOutOfRangeException("Note must not be longer than 80 characters");
				if (value.Contains(",")) throw new ArgumentOutOfRangeException("Note must not contain commas");
				if (IsMandatory && string.IsNullOrWhiteSpace(value)) throw new ArgumentOutOfRangeException("Note cannot be empty when mandatory");
				if (IsMandatory && value.Equals("null")) throw new ArgumentOutOfRangeException("Note cannot be empty when mandatory");

				_entry = value;
			}
		}
	}
}
