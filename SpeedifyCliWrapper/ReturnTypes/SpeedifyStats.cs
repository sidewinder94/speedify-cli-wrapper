using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyStats : ICustomJson, INotifyPropertyChanged
    {
        public SpeedifyState State
        {
            get { return this._state; }
            private set
            {
                if (Equals(value, this._state)) return;
                this._state = value;
                this.OnPropertyChanged();
            }
        }

        public List<SpeedifyAdapter> Adapters
        {
            get { return this._adapters; }
            private set
            {
                if (Equals(value, this._adapters)) return;
                this._adapters = value;
                this.OnPropertyChanged();
            }
        }

        public SpeedifyConnectionStats ConnectionStats
        {
            get { return this._connectionStats; }
            private set
            {
                if (Equals(value, this._connectionStats)) return;
                this._connectionStats = value;
                this.OnPropertyChanged();
            }
        }

        public SpeedifySessionStats SessionStats
        {
            get { return this._sessionStats; }
            private set
            {
                if (Equals(value, this._sessionStats)) return;
                this._sessionStats = value;
                this.OnPropertyChanged();
            }
        }

        private readonly IReadOnlyDictionary<string, MethodInfo> _accessorDictionary;
        private SpeedifyState _state;
        private List<SpeedifyAdapter> _adapters = new List<SpeedifyAdapter>();
        private SpeedifyConnectionStats _connectionStats;
        private SpeedifySessionStats _sessionStats;

        public SpeedifyStats()
        {
            this._accessorDictionary = this.GetType().GetProperties().ToDictionary(kp => kp.Name.ToLower(), vp => vp.SetMethod);
        }


        public MethodInfo this[string part] => this._accessorDictionary[part.Replace("_", "")];

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
