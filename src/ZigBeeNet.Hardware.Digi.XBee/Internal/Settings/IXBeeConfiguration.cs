namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Settings
{
    public interface IXBeeConfiguration
    {       
        #region properties

        INetworkSettings NetworkSettings { get; }

        // TODO: Implement the settings
        //IAddressSettings AddressSettings { get; }
        //IRfInterfaceOptions RfInterfaceOptions { get; }
        //ISecurityParameters SecurityParameters { get; }
        //IModemInterfaceOptions ModemInterfaceOptions { get; }
        //IAtCommandOptions AtCommandOptions { get; }
        //ISleepModeConfiguration SleepModeConfiguration { get; }
        //IIoSettings IoSettings { get; }
        //IDiagnosticCommands DiagnosticCommands { get; }

        #endregion properties

        #region methods

        void Initialize();

        #endregion methods
    }
}
