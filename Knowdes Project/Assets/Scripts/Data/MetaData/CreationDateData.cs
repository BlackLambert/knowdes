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
		public override bool Destroyable => false;

		public CreationDateData(Guid iD) : base(iD)
		{
			_date = DateTime.Now;
			init();
		}

		public CreationDateData(Guid iD, DateTime date) : base(iD)
		{
			_date = date;
			init();
		}



		private void init()
		{
			ShowInPreview = false;
		}
	}
}