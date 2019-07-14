using SpeedifyCliWrapper.ReturnTypes;

namespace SpeedifyCliWrapper.Modules
{
    public class Show
    {
        private Speedify _wrapper;

        public Show(Speedify speedify)
        {
            this._wrapper = speedify;
        }

        public SpeedifySettings Settings()
        {
            return this._wrapper.RunSpeedifyCommand<SpeedifySettings>(args: new[] { "show", "settings" });
        }
    }
}