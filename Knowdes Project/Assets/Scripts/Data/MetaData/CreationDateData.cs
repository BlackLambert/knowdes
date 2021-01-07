using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
	public class CreationDateData : MetaData
	{
		private DateTime _date;
		public DateTime Date => _date;

		public override MetaDataType Type => MetaDataType.CreationDate;

		public override int Priority => int.MinValue;

		public CreationDateData(Guid iD) : base(iD, false)
		{
			_date = DateTime.Now;
		}
	}
}