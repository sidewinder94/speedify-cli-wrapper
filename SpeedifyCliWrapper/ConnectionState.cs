namespace SpeedifyCliWrapper
{
    /// <summary>
    /// Enum specifying the state Speedify's connection is in
    /// </summary>
    public enum ConnectionState
    {
        /// <summary>
        /// Logged out 
        /// </summary>
        LoggedOut = 0,

        /// <summary>
        /// Logging in
        /// </summary>
        LoggingIn = 1,

        /// <summary>
        /// Logged out
        /// </summary>
        LoggedIn = 2,

        /// <summary>
        /// Is automatically connecting
        /// </summary>
        AutoConnecting = 3,

        /// <summary>
        /// Is connecting
        /// </summary>
        Connecting = 4,

        /// <summary>
        /// Is disconnecting
        /// </summary>
        Disconnecting = 5,

        /// <summary>
        /// Connected
        /// </summary>
        Connected = 6,

        /// <summary>
        /// User is over his data limit
        /// </summary>
        Overlimit = 7,

        /// <summary>
        /// Unkown state
        /// </summary>
        Unknown = 8
    }
}