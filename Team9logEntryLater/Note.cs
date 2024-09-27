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
			set { value = _isManadatory; }
		}

		public string Entry
		{
			get { return _entry; }
			set
			{
				if (value.Length > 80) throw new ArgumentOutOfRangeException(nameof(value), "Note must not be longer than 80 characters");
				if (value.Contains(",")) throw new ArgumentOutOfRangeException(nameof(value), "Note must not contain commas");
				if (IsMandatory && string.IsNullOrWhiteSpace(value)) throw new ArgumentOutOfRangeException(nameof(value), "Note cannot be empty when mandatory");

				_entry = value;
			}
		}
	}
}
