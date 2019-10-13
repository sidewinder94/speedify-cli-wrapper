using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public class SpeedifyStats : ICustomJson, INotifyPropertyChanged
    {
        [JsonProperty("state")]
        public SpeedifyState State
        {
            get => this._state;
            private set
            {
                if (Equals(value, this._state)) return;
                this._state = value;
                this.OnPropertyChanged();
            }
        }

        [JsonProperty("adapters")]
        public List<SpeedifyAdapter> Adapters
        {
            get => this._adapters;
            private set
            {
                if (Equals(value, this._adapters)) return;
                this._adapters = value;
                this.OnPropertyChanged();
            }
        }

        [JsonProperty("connection_stats")]
        public SpeedifyConnectionStats ConnectionStats
        {
            get => this._connectionStats;
            private set
            {
                if (Equals(value, this._connectionStats)) return;
                this._connectionStats = value;
                this.OnPropertyChanged();
            }
        }

        [JsonProperty("session_stats")]
        public SpeedifySessionStats SessionStats
        {
            get => this._sessionStats;
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
            this._accessorDictionary = this.GetType()
                .GetProperties()
                .SelectMany(p =>
                    p.GetCustomAttributes(typeof(JsonPropertyAttribute))
                    .ToDictionary(
                        k => ((JsonPropertyAttribute)k).PropertyName, v => p.SetMethod))
                        .ToDictionary(kv => kv.Key, v => v.Value);
        }


        public MethodInfo this[string part] => this._accessorDictionary[part];

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
