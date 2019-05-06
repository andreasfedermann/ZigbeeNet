using Microsoft.Extensions.Configuration;
using System;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Settings
{
    public class XBeeConfiguration : IXBeeConfiguration
    {
        #region fields

        private readonly IConfigurationRoot _configuration;

        #endregion fields

        #region constructor

        public XBeeConfiguration(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        #endregion constructor

        #region properties

        public INetworkSettings NetworkSettings
        {
            get;
            private set;
        }

        #endregion properties

        #region methods

        public void Initialize()
        {
            IConfigurationSection networkConfigurationSection = _configuration.GetSection("DigiXBeeConfiguration").GetSection("NetworkSettings");
            INetworkSettings networkSettings = new NetworkSettings
            {
                PanId = Convert.ToUInt64(networkConfigurationSection.GetSection("PanId").Value),
                ScanChannels = Convert.ToUInt16(networkConfigurationSection.GetSection("ScanChannels").Value, 16),
                ScanDuration = Convert.ToByte(networkConfigurationSection.GetSection("ScanDuration").Value),
                ZigBeeStackProfile = Convert.ToByte(networkConfigurationSection.GetSection("ZigBeeStackProfile").Value),
                NodeJoinTime = Convert.ToByte(networkConfigurationSection.GetSection("NodeJoinTime").Value, 16),
                NetworkWatchdogTimeout = Convert.ToUInt16(networkConfigurationSection.GetSection("NetworkWatchdogTimeout").Value),
                ChannelVerification = (EnabledDisabledState)Enum.Parse(typeof(EnabledDisabledState), networkConfigurationSection.GetSection("ChannelVerification").Value),
                JoinNotification = (EnabledDisabledState)Enum.Parse(typeof(EnabledDisabledState), networkConfigurationSection.GetSection("JoinNotification").Value),
                CoordinatorEnable = (EnabledDisabledState)Enum.Parse(typeof(EnabledDisabledState), networkConfigurationSection.GetSection("CoordinatorEnable").Value),
                DeviceOptions = Convert.ToByte(networkConfigurationSection.GetSection("DeviceOptions").Value),
                DeviceControls = Convert.ToByte(networkConfigurationSection.GetSection("DeviceControls").Value)
            };


            throw new NotImplementedException();
        }

        #endregion methods
    }
}
