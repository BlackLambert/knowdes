using System;

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

		public override bool Destroyable => false;

		public event Action OnDateChanged;

		public LastChangeDateData(Guid iD) : base(iD)
		{
			_date = DateTime.Now;
			init();
		}

		public LastChangeDateData(Guid iD, DateTime dateTime) : base(iD)
		{
			_date = dateTime;
			init();
		}

		private void init()
		{
			ShowInPreview = false;
		}
	}
}