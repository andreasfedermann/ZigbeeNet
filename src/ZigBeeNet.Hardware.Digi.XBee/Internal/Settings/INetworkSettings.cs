namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Settings
{
    public interface INetworkSettings
    {
        /// <summary>
        /// Gets or sets the pan identifier.
        /// Set/Read the ZigBee extended PAN ID. Valid range is 0 - 0xFFFFFFFFFFFFFFFF. For a router or end device, 
        /// ID determines the network to join, but 0 allows it to join a network with any extended PAN ID. For a coordinator, 
        /// ID selects extended PAN ID, but a value of 0 causes coordinator to randomly select the extended PAN ID.
        /// </summary>
        /// <value>
        /// The pan identifier.
        /// </value>
        ulong PanId { get; set; }

        /// <summary>
        /// Gets or sets the scan channels.
        /// Set/read list of channels to scan as bitfield: Bit 15 = Chan 0x1A . . . Bit 0 = Chan 0x0B. These channels apply 
        /// when joining for routers and end devices. The coordinator uses these channels for active and energy scans when 
        /// forming a network on startup.
        /// </summary>
        /// <value>
        /// The scan channels.
        /// </value>
        ushort ScanChannels { get; set; }

        /// <summary>
        /// Gets or sets the duration of the scan.
        /// Set/read the Scan Duration exponent. The exponent configures the duration of the active scan (PAN scan) on each channel 
        /// in the SC channel mask when attempting to join a PAN. Scan Time = (SC * (2 ^ SD) * 15.36ms) + (38ms * SC) + 20ms. (SC=# channels)
        /// </summary>
        /// <value>
        /// The duration of the scan.
        /// </value>
        byte ScanDuration { get; set; }

        /// <summary>
        /// Gets or sets the zig bee stack profile.
        /// Set/read the ZigBee stack profile setting. ZS must be written to flash; changing ZS and applying the change will 
        /// automatically execute a WR command and reset the device. 0=Network Specific, 1=ZigBee-2006, 2=ZigBee-PRO
        /// </summary>
        /// <value>
        /// The zig bee stack profile.
        /// </value>
        byte ZigBeeStackProfile { get; set; }

        /// <summary>
        /// Gets or sets the node join time.
        /// Set/read the Node Join time. The value of NJ determines the time (in seconds) that the device will allow other devices 
        /// to join to it. If set to 0xFF, the device will always allow joining.
        /// </summary>
        /// <value>
        /// The node join time.
        /// </value>
        byte NodeJoinTime { get; set; }

        /// <summary>
        /// Gets or sets the network watchdog timeout.
        /// Set/read the network watchdog timeout. If set to a non-zero value, the network watchdog timer is enabled on a router. 
        /// The router will leave the network if is does not receive valid communication within (3 * NW) minutes. The timer is 
        /// reset each time data is received from or sent to a coordinator, or if a many-to-one broadcast is received.
        /// </summary>
        /// <value>
        /// The network watchdog timeout.
        /// </value>
        ushort NetworkWatchdogTimeout { get; set; }

        /// <summary>
        /// Gets or sets the channel verification.
        /// verify a coordinator exists on the same channel after joining or power cycling to ensure it is operating on a valid 
        /// channel, and will leave if a coordinator cannot be found (if NJ=0xFF). If disabled, the router will remain on the 
        /// same channel through power cycles.
        /// </summary>
        /// <value>
        /// The channel verification.
        /// </value>
        EnabledDisabledState ChannelVerification { get; set; }

        /// <summary>
        /// Gets or sets the join notification.
        /// Set/read the join notification setting. If enabled, the module will transmit a broadcast node identification frame 
        /// on power up and when joining. This action blinks the Assoc LED rapidly on all devices that receive the data, and 
        /// sends an API frame out the UART of API devices. This function should be disabled for large networks.
        /// </summary>
        /// <value>
        /// The join notification.
        /// </value>
        EnabledDisabledState JoinNotification { get; set; }

        /// <summary>
        /// Gets the operating pan identifier.
        /// Read the operating PAN ID (ZigBee extended PAN ID).
        /// </summary>
        /// <value>
        /// The operating pan identifier.
        /// </value>
        ulong OperatingPanId { get; }

        /// <summary>
        /// Gets the operating16 bit pan identifier.
        /// Read the 16-bit operating PAN ID.
        /// </summary>
        /// <value>
        /// The operating16 bit pan identifier.
        /// </value>
        ushort Operating16BitPanId { get; }

        /// <summary>
        /// Gets the operating channel.
        /// Read the operating channel number (Uses 802.15.4 channel numbers).
        /// </summary>
        /// <value>
        /// The operating channel.
        /// </value>
        byte OperatingChannel { get; }

        /// <summary>
        /// Gets the number of remaining children.
        /// Read the number of remaining end device children that can join this device. If NC=0, the device cannot 
        /// allow any more end device children to join.
        /// </summary>
        /// <value>
        /// The number of remaining children.
        /// </value>
        byte NumberOfRemainingChildren { get; }

        /// <summary>
        /// Gets or sets the coordinator enable.
        /// Set/read if this device is a coordinator (1) or not (0)
        /// </summary>
        /// <value>
        /// The coordinator enable.
        /// </value>
        EnabledDisabledState CoordinatorEnable { get; set; }

        /// <summary>
        /// Gets or sets the device options.
        /// Bit0 - Reserved. Bit1 - Reserved. Bit2 - Enable Best Response Joining. Bit3 - Disable NULL Transport Key.
        /// Bit4 - Disable Ext.Timeout. Bit5 - Enable NoAck IO Sampling. Bit6 - Enable High RAM Concentrator. Bit7 - Enable 
        /// ATNW to find new network before leaving the network.
        /// </summary>
        /// <value>
        /// The device options.
        /// </value>
        byte DeviceOptions { get; set; }

        /// <summary>
        /// Gets or sets the device controls.
        /// Bit0 - Enable Joiner Global Link Key. Bit1 - NWK Leave Request Not Allowed. Bit 4 - Enable Verbose 
        /// Joining Mode. Bit 7 - Enable FR after 60s of no beacon responses during join.
        /// </summary>
        /// <value>
        /// The device controls.
        /// </value>
        byte DeviceControls { get; set; }

    }
}