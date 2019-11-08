using SpeedifyCliWrapper.ReturnTypes;
using System.Collections.Generic;

namespace SpeedifyCliWrapper.Modules
{
    public class Show
    {
        private Speedify _wrapper;
        private const string _moduleName = "show";

        public Show(Speedify speedify)
        {
            this._wrapper = speedify;
        }

        public SpeedifyServers Servers(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyServers>(timeout, args: new[] { _moduleName, "servers" });
        }

        public SpeedifySettings Settings(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifySettings>(timeout, args: new[] { _moduleName, "settings" });
        }

        public SpeedifyPrivacy Privacy(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyPrivacy>(timeout, args: new[] { _moduleName, "privacy" });
        }
        
        public List<SpeedifyAdapter> Adapters(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<List<SpeedifyAdapter>>(timeout, args: new[] { _moduleName, "adapters" });
        }

        public SpeedifyServer CurrentServer(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyServer>(timeout, args: new[] { _moduleName, "currentserver" });
        }

        public SpeedifyUser User(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyUser>(timeout, args: new[] { _moduleName, "user" });
        }

        public SpeedifyDirectory Directory(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyDirectory>(timeout, args: new[] { _moduleName, "directory" });
        }

        public SpeedifyConnectMethod ConnectMethod(int timeout = 60)
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifyConnectMethod>(timeout, args: new[] { _moduleName, "connectmethod" });
        }
    }
}