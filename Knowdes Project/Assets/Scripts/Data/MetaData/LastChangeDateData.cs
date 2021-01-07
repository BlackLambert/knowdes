using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
	public class LastChangeDateData : MetaData
	{
		private DateTime _date;
		public DateTime Date
		{
			get => _date;
			set
			{
				_date = value;
				OnDateChanged?.Invoke();
			}
		}

		public override MetaDataType Type => MetaDataType.LastChangedDate;

		public override int Priority => int.MinValue + 1;

		public event Action OnDateChanged;

		public LastChangeDateData(Guid iD) : base(iD, false)
		{
			_date = DateTime.Now;
		}
	}
}