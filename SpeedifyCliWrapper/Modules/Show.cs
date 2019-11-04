using SpeedifyCliWrapper.ReturnTypes;
using System.Collections.Generic;

namespace SpeedifyCliWrapper.Modules
{
    public class Show
    {
        private Speedify _wrapper;

        public Show(Speedify speedify)
        {
            this._wrapper = speedify;
        }

        public SpeedifyServers Servers(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyServers>(timeout, args: new[] { "show", "servers" });
        }

        public SpeedifySettings Settings(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifySettings>(timeout, args: new[] { "show", "settings" });
        }

        public SpeedifyPrivacy Privacy(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyPrivacy>(timeout, args: new[] { "show", "privacy" });
        }
        
        public List<SpeedifyAdapter> Adapters(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<List<SpeedifyAdapter>>(timeout, args: new[] { "show", "adapters" });
        }

        public SpeedifyServer CurrentServer(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyServer>(timeout, args: new[] { "show", "currentserver" });
        }

        public SpeedifyUser User(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyUser>(timeout, args: new[] { "show", "user" });
        }
    }
}