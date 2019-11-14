namespace SpeedifyCliWrapper.ReturnTypes
{
    public partial class SpeedifyAdapter
    {
		public SpeedifyAdapter DailyDataLimit (SpeedifyCliWrapper.ReturnTypes.SpeedifyAdapter adapter, System.Nullable<long> dataUsage, int timeout)
		{
			return this._wrapper.Adapter.DailyDataLimit(adapter, dataUsage, timeout);
		}

		public SpeedifyAdapter DailyDataBoost (SpeedifyCliWrapper.ReturnTypes.SpeedifyAdapter adapter, long additionalData, int timeout)
		{
			return this._wrapper.Adapter.DailyDataBoost(adapter, additionalData, timeout);
		}

		public SpeedifyAdapter MonthlyDataLimit (SpeedifyCliWrapper.ReturnTypes.SpeedifyAdapter adapter, System.Nullable<long> dataUsage, int dayOfReset, int timeout)
		{
			return this._wrapper.Adapter.MonthlyDataLimit(adapter, dataUsage, dayOfReset, timeout);
		}

	}
}

