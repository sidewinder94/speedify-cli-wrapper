﻿/*
This file is generated by a tool, do not edit manually
*/

using System;
using System.Collections.Generic;
using SpeedifyCliWrapper.Common;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public partial class SpeedifyAdapter
    {
        public SpeedifyAdapter DailyDataLimit(Nullable<long> dataUsage, int timeout = 60)
        {
            return this._wrapper.Adapter.DailyDataLimit(this, dataUsage, timeout);
        }

        public SpeedifyAdapter DailyDataBoost(long additionalData, int timeout = 60)
        {
            return this._wrapper.Adapter.DailyDataBoost(this, additionalData, timeout);
        }

        public SpeedifyAdapter MonthlyDataLimit(Nullable<long> dataUsage, int dayOfReset = 0, int timeout = 60)
        {
            return this._wrapper.Adapter.MonthlyDataLimit(this, dataUsage, dayOfReset, timeout);
        }

        public SpeedifyAdapter SetEncryption(bool enable, int timeout = 60)
        {
            return this._wrapper.Adapter.SetEncryption(this, enable, timeout);
        }

    }
}

