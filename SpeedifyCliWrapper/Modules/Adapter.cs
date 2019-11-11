using SpeedifyCliWrapper.ReturnTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedifyCliWrapper.Modules
{
    public class Adapter
    {
        private Speedify _wrapper;
        private const string _moduleName = "adapter";

        public Adapter(Speedify wrapper)
        {
            this._wrapper = wrapper;
        }

        /// <summary>
        /// Defines a daily data limit for a specified adapter.
        /// </summary>
        /// <param name="adapter">The adapter to set the limit on</param>
        /// <param name="dataUsage">The limit in bytes, <code>null</code> is unlimited, default is null</param>
        /// <param name="timeout">Timeout for the command, default 60</param>
        /// <returns>An updated <see cref="SpeedifyAdapter"/> object</returns>
        public SpeedifyAdapter DailyDataLimit(SpeedifyAdapter adapter, long? dataUsage = null, int timeout = 60)
        {
            string value = "unlimited";

            if(dataUsage.HasValue)
            {
                value = dataUsage.ToString();
            }

            return this._wrapper.RunSpeedifyCommand<SpeedifyAdapter>(timeout, args: new[] { Adapter._moduleName, "datalimit", "daily", adapter.AdapterId.ToString(), value });
        }

        /// <summary>
        /// Increase the daily data cap for today only
        /// </summary>
        /// <param name="adapter">The adapter to increase the limit on</param>
        /// <param name="additionalData">Byte count to add to the limit</param>
        /// <param name="timeout">Timeout for the command, default 60</param>
        /// <returns>An updated <see cref="SpeedifyAdapter"/> object</returns>
        public SpeedifyAdapter DailyDataBoost(SpeedifyAdapter adapter, long additionalData, int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyAdapter>(timeout, args: new[] { Adapter._moduleName, "datalimit", "dailyboost", adapter.AdapterId.ToString(), additionalData.ToString() });
        }

        /// <summary>
        /// Defines a daily data limit for a specified adapter.
        /// </summary>
        /// <param name="adapter">The adapter to set the limit on</param>
        /// <param name="dataUsage">The limit in bytes, <code>null</code> is unlimited, default is null</param>
        /// <param name="dayOfReset">day of the month to reset on, 0 for last 30 days, default : 0</param>
        /// <param name="timeout">Timeout for the command, default 60</param>
        /// <returns>An updated <see cref="SpeedifyAdapter"/> object</returns>
        public SpeedifyAdapter MonthlyDataLimit(SpeedifyAdapter adapter, long? dataUsage = null, int dayOfReset = 0,int timeout = 60)
        {
            string value = "unlimited";

            if (dataUsage.HasValue)
            {
                value = dataUsage.ToString();
            }

            return this._wrapper.RunSpeedifyCommand<SpeedifyAdapter>(timeout, args: new[] { Adapter._moduleName, "datalimit", "monthly", adapter.AdapterId.ToString(), value, value == "unlimited" ? "" : dayOfReset.ToString() });
        }
    }
}
