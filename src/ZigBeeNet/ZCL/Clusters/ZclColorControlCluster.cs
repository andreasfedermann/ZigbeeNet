﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters
{
    public class ZclColorControlCluster : ZclCluster
    {
        /**
         * The ZigBee Cluster Library Cluster ID
         */
        public const ushort CLUSTER_ID = 0x0300;

        /**
         * The ZigBee Cluster Library Cluster Name
         */
        public const string CLUSTER_NAME = "Color Control";

        // Attribute constants
        /**
         * The CurrentHue attribute contains the current hue value of the light. It is updated
         * as fast as practical during commands that change the hue.
         * <p>
         * The hue in degrees shall be related to the CurrentHue attribute by the relationship
         * Hue = CurrentHue x 360 / 254 (CurrentHue in the range 0 - 254 inclusive)
         * <p>
         * If this attribute is implemented then the CurrentSaturation and ColorMode
         * attributes shall also be implemented.
         */
        public const ushort ATTR_CURRENTHUE = 0x0000;
        /**
         * The CurrentSaturation attribute holds the current saturation value of the light. It is
         * updated as fast as practical during commands that change the saturation.
         * The saturation shall be related to the CurrentSaturation attribute by the
         * relationship
         * Saturation = CurrentSaturation/254 (CurrentSaturation in the range 0 - 254 inclusive)
         * If this attribute is implemented then the CurrentHue and ColorMode attributes
         * shall also be implemented.
         */
        public const ushort ATTR_CURRENTSATURATION = 0x0001;
        /**
         * The RemainingTime attribute holds the time remaining, in 1/10ths of a second,
         * until the currently active command will be complete.
         */
        public const ushort ATTR_REMAININGTIME = 0x0002;
        /**
         * The CurrentX attribute contains the current value of the normalized chromaticity
         * value x, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of x shall be related to the CurrentX attribute by the relationship
         * <p>
         * x = CurrentX / 65535 (CurrentX in the range 0 to 65279 inclusive)
         */
        public const ushort ATTR_CURRENTX = 0x0003;
        /**
         * The CurrentY attribute contains the current value of the normalized chromaticity
         * value y, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of y shall be related to the CurrentY attribute by the relationship
         * <p>
         * y = CurrentY / 65535 (CurrentY in the range 0 to 65279 inclusive)
         */
        public const ushort ATTR_CURRENTY = 0x0004;
        /**
         * The DriftCompensation attribute indicates what mechanism, if any, is in use for
         * compensation for color/intensity drift over time.
         */
        public const ushort ATTR_DRIFTCOMPENSATION = 0x0005;
        /**
         * The CompensationText attribute holds a textual indication of what mechanism, if
         * any, is in use to compensate for color/intensity drift over time.
         */
        public const ushort ATTR_COMPENSATIONTEXT = 0x0006;
        /**
         * The ColorTemperature attribute contains a scaled inverse of the current value of
         * the color temperature. It is updated as fast as practical during commands that
         * change the color.
         * <p>
         * The color temperature value in Kelvins shall be related to the ColorTemperature
         * attribute by the relationship
         * <p>
         * Color temperature = 1,000,000 / ColorTemperature (ColorTemperature in the
         * range 1 to 65279 inclusive, giving a color temperature range from 1,000,000
         * Kelvins to 15.32 Kelvins).
         * <p>
         * The value ColorTemperature = 0 indicates an undefined value. The value
         * ColorTemperature = 65535 indicates an invalid value.
         */
        public const ushort ATTR_COLORTEMPERATURE = 0x0007;
        /**
         * The ColorMode attribute indicates which attributes are currently determining the color of the device.
         * If either the CurrentHue or CurrentSaturation attribute is implemented, this attribute SHALL also be
         * implemented, otherwise it is optional. The value of the ColorMode attribute cannot be written directly
         * - it is Set upon reception of another command in to the appropriate mode for that command.
         */
        public const ushort ATTR_COLORMODE = 0x0008;
        /**
         * The EnhancedCurrentHueattribute represents non-equidistant steps along the CIE 1931 color
         * triangle, and it provides 16-bits precision. The upper 8 bits of this attribute SHALL be
         * used as an index in the implementation specific XY lookup table to provide the non-equidistance
         * steps (see the ZLL test specification for an example).  The lower 8 bits SHALL be used to
         * interpolate between these steps in a linear way in order to provide color zoom for the user.
         */
        public const ushort ATTR_ENHANCEDCURRENTHUE = 0x4000;
        /**
         * The EnhancedColorModeattribute specifies which attributes are currently determining the color of the device.
         * To provide compatibility with standard ZCL, the original ColorModeattribute SHALLindicate ‘CurrentHueand CurrentSaturation’
         * when the light uses the EnhancedCurrentHueattribute.
         */
        public const ushort ATTR_ENHANCEDCOLORMODE = 0x4001;
        /**
         * The ColorLoopActive attribute specifies the current active status of the color loop.
         * If this attribute has the value 0x00, the color loop SHALLnot be active. If this attribute
         * has the value 0x01, the color loop SHALL be active. All other values (0x02 – 0xff) are reserved.
         */
        public const ushort ATTR_COLORLOOPACTIVE = 0x4002;
        /**
         * The ColorLoopDirection attribute specifies the current direction of the color loop.
         * If this attribute has the value 0x00, the EnhancedCurrentHue attribute SHALL be decremented.
         * If this attribute has the value 0x01, the EnhancedCurrentHue attribute SHALL be incremented.
         * All other values (0x02 – 0xff) are reserved.
         */
        public const ushort ATTR_COLORLOOPDIRECTION = 0x4003;
        /**
         * The ColorLoopTime attribute specifies the number of seconds it SHALL take to perform a full
         * color loop, i.e.,to cycle all values of the EnhancedCurrentHue attribute (between 0x0000 and 0xffff).
         */
        public const ushort ATTR_COLORLOOPTIME = 0x4004;
        /**
         * The ColorLoopStartEnhancedHueattribute specifies the value of the EnhancedCurrentHue attribute
         * from which the color loop SHALL be started.
         */
        public const ushort ATTR_COLORLOOPSTARTHUE = 0x4005;
        /**
         * The ColorLoopStoredEnhancedHue attribute specifies the value of the EnhancedCurrentHue attribute
         * before the color loop was started. Once the color loop is complete, the EnhancedCurrentHue
         * attribute SHALL be restored to this value.
         */
        public const ushort ATTR_COLORLOOPSTOREDHUE = 0x4006;
        /**
         * The ColorCapabilitiesattribute specifies the color capabilities of the device supporting the
         * color control cluster.
         * <p>
         * Note:The support of the CurrentXand CurrentYattributes is mandatory regardless of color capabilities.
         */
        public const ushort ATTR_COLORCAPABILITIES = 0x400A;
        /**
         * The ColorTempPhysicalMinMiredsattribute indicates the minimum mired value
         * supported by the hardware. ColorTempPhysicalMinMiredscorresponds to the maximum
         * color temperature in kelvins supported by the hardware.
         * ColorTempPhysicalMinMireds ≤ ColorTemperatureMireds
         */
        public const ushort ATTR_COLORTEMPERATUREMIN = 0x400B;
        /**
         * The ColorTempPhysicalMaxMiredsattribute indicates the maximum mired value
         * supported by the hard-ware. ColorTempPhysicalMaxMiredscorresponds to the minimum
         * color temperature in kelvins supported by the hardware.
         * ColorTemperatureMireds ≤ ColorTempPhysicalMaxMireds.
         */
        public const ushort ATTR_COLORTEMPERATUREMAX = 0x400C;

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>();

            ZclClusterType colorControl = ZclClusterType.GetValueById(ClusterType.COLOR_CONTROL);

            attributeMap.Add(ATTR_CURRENTHUE, new ZclAttribute(colorControl, ATTR_CURRENTHUE, "CurrentHue", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_CURRENTSATURATION, new ZclAttribute(colorControl, ATTR_CURRENTSATURATION, "CurrentSaturation", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_REMAININGTIME, new ZclAttribute(colorControl, ATTR_REMAININGTIME, "RemainingTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTX, new ZclAttribute(colorControl, ATTR_CURRENTX, "CurrentX", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_CURRENTY, new ZclAttribute(colorControl, ATTR_CURRENTY, "CurrentY", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_DRIFTCOMPENSATION, new ZclAttribute(colorControl, ATTR_DRIFTCOMPENSATION, "DriftCompensation", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_COMPENSATIONTEXT, new ZclAttribute(colorControl, ATTR_COMPENSATIONTEXT, "CompensationText", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_COLORTEMPERATURE, new ZclAttribute(colorControl, ATTR_COLORTEMPERATURE, "ColorTemperature", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_COLORMODE, new ZclAttribute(colorControl, ATTR_COLORMODE, "ColorMode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_ENHANCEDCURRENTHUE, new ZclAttribute(colorControl, ATTR_ENHANCEDCURRENTHUE, "EnhancedCurrentHue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_ENHANCEDCOLORMODE, new ZclAttribute(colorControl, ATTR_ENHANCEDCOLORMODE, "EnhancedColorMode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPACTIVE, new ZclAttribute(colorControl, ATTR_COLORLOOPACTIVE, "ColorLoopActive", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPDIRECTION, new ZclAttribute(colorControl, ATTR_COLORLOOPDIRECTION, "ColorLoopDirection", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPTIME, new ZclAttribute(colorControl, ATTR_COLORLOOPTIME, "ColorLoopTime", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPSTARTHUE, new ZclAttribute(colorControl, ATTR_COLORLOOPSTARTHUE, "ColorLoopStartHue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORLOOPSTOREDHUE, new ZclAttribute(colorControl, ATTR_COLORLOOPSTOREDHUE, "ColorLoopStoredHue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORCAPABILITIES, new ZclAttribute(colorControl, ATTR_COLORCAPABILITIES, "ColorCapabilities", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, false, false));
            attributeMap.Add(ATTR_COLORTEMPERATUREMIN, new ZclAttribute(colorControl, ATTR_COLORTEMPERATUREMIN, "ColorTemperatureMin", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_COLORTEMPERATUREMAX, new ZclAttribute(colorControl, ATTR_COLORTEMPERATUREMAX, "ColorTemperatureMax", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        /**
         * Default constructor to create a Color Control cluster.
         *
         * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
         */
        public ZclColorControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /**
         * Get the <i>CurrentHue</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The CurrentHue attribute contains the current hue value of the light. It is updated
         * as fast as practical during commands that change the hue.
         * <p>
         * The hue in degrees shall be related to the CurrentHue attribute by the relationship
         * Hue = CurrentHue x 360 / 254 (CurrentHue in the range 0 - 254 inclusive)
         * <p>
         * If this attribute is implemented then the CurrentSaturation and ColorMode
         * attributes shall also be implemented.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetCurrentHueAsync()
        {
            return Read(_attributes[ATTR_CURRENTHUE]);
        }

        /**
         * Synchronously Get the <i>CurrentHue</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The CurrentHue attribute contains the current hue value of the light. It is updated
         * as fast as practical during commands that change the hue.
         * <p>
         * The hue in degrees shall be related to the CurrentHue attribute by the relationship
         * Hue = CurrentHue x 360 / 254 (CurrentHue in the range 0 - 254 inclusive)
         * <p>
         * If this attribute is implemented then the CurrentSaturation and ColorMode
         * attributes shall also be implemented.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetCurrentHue(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTHUE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_CURRENTHUE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_CURRENTHUE]);
        }

        /**
         * Set reporting for the <i>CurrentHue</i> attribute [attribute ID <b>0</b>].
         * <p>
         * The CurrentHue attribute contains the current hue value of the light. It is updated
         * as fast as practical during commands that change the hue.
         * <p>
         * The hue in degrees shall be related to the CurrentHue attribute by the relationship
         * Hue = CurrentHue x 360 / 254 (CurrentHue in the range 0 - 254 inclusive)
         * <p>
         * If this attribute is implemented then the CurrentSaturation and ColorMode
         * attributes shall also be implemented.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @param reportableChange {@link object} delta required to trigger report
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetCurrentHueReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_CURRENTHUE], minInterval, maxInterval, reportableChange);
        }

        /**
         * Get the <i>CurrentSaturation</i> attribute [attribute ID <b>1</b>].
         * <p>
         * The CurrentSaturation attribute holds the current saturation value of the light. It is
         * updated as fast as practical during commands that change the saturation.
         * The saturation shall be related to the CurrentSaturation attribute by the
         * relationship
         * Saturation = CurrentSaturation/254 (CurrentSaturation in the range 0 - 254 inclusive)
         * If this attribute is implemented then the CurrentHue and ColorMode attributes
         * shall also be implemented.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetCurrentSaturationAsync()
        {
            return Read(_attributes[ATTR_CURRENTSATURATION]);
        }

        /**
         * Synchronously Get the <i>CurrentSaturation</i> attribute [attribute ID <b>1</b>].
         * <p>
         * The CurrentSaturation attribute holds the current saturation value of the light. It is
         * updated as fast as practical during commands that change the saturation.
         * The saturation shall be related to the CurrentSaturation attribute by the
         * relationship
         * Saturation = CurrentSaturation/254 (CurrentSaturation in the range 0 - 254 inclusive)
         * If this attribute is implemented then the CurrentHue and ColorMode attributes
         * shall also be implemented.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetCurrentSaturation(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTSATURATION].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_CURRENTSATURATION].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_CURRENTSATURATION]);
        }

        /**
         * Set reporting for the <i>CurrentSaturation</i> attribute [attribute ID <b>1</b>].
         * <p>
         * The CurrentSaturation attribute holds the current saturation value of the light. It is
         * updated as fast as practical during commands that change the saturation.
         * The saturation shall be related to the CurrentSaturation attribute by the
         * relationship
         * Saturation = CurrentSaturation/254 (CurrentSaturation in the range 0 - 254 inclusive)
         * If this attribute is implemented then the CurrentHue and ColorMode attributes
         * shall also be implemented.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @param reportableChange {@link object} delta required to trigger report
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetCurrentSaturationReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_CURRENTSATURATION], minInterval, maxInterval, reportableChange);
        }

        /**
         * Get the <i>RemainingTime</i> attribute [attribute ID <b>2</b>].
         * <p>
         * The RemainingTime attribute holds the time remaining, in 1/10ths of a second,
         * until the currently active command will be complete.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetRemainingTimeAsync()
        {
            return Read(_attributes[ATTR_REMAININGTIME]);
        }

        /**
         * Synchronously Get the <i>RemainingTime</i> attribute [attribute ID <b>2</b>].
         * <p>
         * The RemainingTime attribute holds the time remaining, in 1/10ths of a second,
         * until the currently active command will be complete.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetRemainingTime(long refreshPeriod)
        {
            if (_attributes[ATTR_REMAININGTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_REMAININGTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_REMAININGTIME]);
        }

        /**
         * Get the <i>CurrentX</i> attribute [attribute ID <b>3</b>].
         * <p>
         * The CurrentX attribute contains the current value of the normalized chromaticity
         * value x, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of x shall be related to the CurrentX attribute by the relationship
         * <p>
         * x = CurrentX / 65535 (CurrentX in the range 0 to 65279 inclusive)
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetCurrentXAsync()
        {
            return Read(_attributes[ATTR_CURRENTX]);
        }

        /**
         * Synchronously Get the <i>CurrentX</i> attribute [attribute ID <b>3</b>].
         * <p>
         * The CurrentX attribute contains the current value of the normalized chromaticity
         * value x, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of x shall be related to the CurrentX attribute by the relationship
         * <p>
         * x = CurrentX / 65535 (CurrentX in the range 0 to 65279 inclusive)
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetCurrentX(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTX].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_CURRENTX].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_CURRENTX]);
        }

        /**
         * Set reporting for the <i>CurrentX</i> attribute [attribute ID <b>3</b>].
         * <p>
         * The CurrentX attribute contains the current value of the normalized chromaticity
         * value x, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of x shall be related to the CurrentX attribute by the relationship
         * <p>
         * x = CurrentX / 65535 (CurrentX in the range 0 to 65279 inclusive)
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @param reportableChange {@link object} delta required to trigger report
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetCurrentXReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_CURRENTX], minInterval, maxInterval, reportableChange);
        }

        /**
         * Get the <i>CurrentY</i> attribute [attribute ID <b>4</b>].
         * <p>
         * The CurrentY attribute contains the current value of the normalized chromaticity
         * value y, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of y shall be related to the CurrentY attribute by the relationship
         * <p>
         * y = CurrentY / 65535 (CurrentY in the range 0 to 65279 inclusive)
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetCurrentYAsync()
        {
            return Read(_attributes[ATTR_CURRENTY]);
        }

        /**
         * Synchronously Get the <i>CurrentY</i> attribute [attribute ID <b>4</b>].
         * <p>
         * The CurrentY attribute contains the current value of the normalized chromaticity
         * value y, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of y shall be related to the CurrentY attribute by the relationship
         * <p>
         * y = CurrentY / 65535 (CurrentY in the range 0 to 65279 inclusive)
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetCurrentY(long refreshPeriod)
        {
            if (_attributes[ATTR_CURRENTY].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_CURRENTY].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_CURRENTY]);
        }

        /**
         * Set reporting for the <i>CurrentY</i> attribute [attribute ID <b>4</b>].
         * <p>
         * The CurrentY attribute contains the current value of the normalized chromaticity
         * value y, as defined in the CIE xyY Color Space. It is updated as fast as practical
         * during commands that change the color.
         * <p>
         * The value of y shall be related to the CurrentY attribute by the relationship
         * <p>
         * y = CurrentY / 65535 (CurrentY in the range 0 to 65279 inclusive)
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is MANDATORY
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @param reportableChange {@link object} delta required to trigger report
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetCurrentYReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_CURRENTY], minInterval, maxInterval, reportableChange);
        }

        /**
         * Get the <i>DriftCompensation</i> attribute [attribute ID <b>5</b>].
         * <p>
         * The DriftCompensation attribute indicates what mechanism, if any, is in use for
         * compensation for color/intensity drift over time.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetDriftCompensationAsync()
        {
            return Read(_attributes[ATTR_DRIFTCOMPENSATION]);
        }

        /**
         * Synchronously Get the <i>DriftCompensation</i> attribute [attribute ID <b>5</b>].
         * <p>
         * The DriftCompensation attribute indicates what mechanism, if any, is in use for
         * compensation for color/intensity drift over time.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetDriftCompensation(long refreshPeriod)
        {
            if (_attributes[ATTR_DRIFTCOMPENSATION].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_DRIFTCOMPENSATION].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_DRIFTCOMPENSATION]);
        }

        /**
         * Get the <i>CompensationText</i> attribute [attribute ID <b>6</b>].
         * <p>
         * The CompensationText attribute holds a textual indication of what mechanism, if
         * any, is in use to compensate for color/intensity drift over time.
         * <p>
         * The attribute is of type {@link string}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetCompensationTextAsync()
        {
            return Read(_attributes[ATTR_COMPENSATIONTEXT]);
        }

        /**
         * Synchronously Get the <i>CompensationText</i> attribute [attribute ID <b>6</b>].
         * <p>
         * The CompensationText attribute holds a textual indication of what mechanism, if
         * any, is in use to compensate for color/intensity drift over time.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link string}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link string} attribute value, or null on error
         */
        public string GetCompensationText(long refreshPeriod)
        {
            if (_attributes[ATTR_COMPENSATIONTEXT].IsLastValueCurrent(refreshPeriod))
            {
                return (string)_attributes[ATTR_COMPENSATIONTEXT].LastValue;
            }

            return (string)ReadSync(_attributes[ATTR_COMPENSATIONTEXT]);
        }

        /**
         * Get the <i>ColorTemperature</i> attribute [attribute ID <b>7</b>].
         * <p>
         * The ColorTemperature attribute contains a scaled inverse of the current value of
         * the color temperature. It is updated as fast as practical during commands that
         * change the color.
         * <p>
         * The color temperature value in Kelvins shall be related to the ColorTemperature
         * attribute by the relationship
         * <p>
         * Color temperature = 1,000,000 / ColorTemperature (ColorTemperature in the
         * range 1 to 65279 inclusive, giving a color temperature range from 1,000,000
         * Kelvins to 15.32 Kelvins).
         * <p>
         * The value ColorTemperature = 0 indicates an undefined value. The value
         * ColorTemperature = 65535 indicates an invalid value.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorTemperatureAsync()
        {
            return Read(_attributes[ATTR_COLORTEMPERATURE]);
        }

        /**
         * Synchronously Get the <i>ColorTemperature</i> attribute [attribute ID <b>7</b>].
         * <p>
         * The ColorTemperature attribute contains a scaled inverse of the current value of
         * the color temperature. It is updated as fast as practical during commands that
         * change the color.
         * <p>
         * The color temperature value in Kelvins shall be related to the ColorTemperature
         * attribute by the relationship
         * <p>
         * Color temperature = 1,000,000 / ColorTemperature (ColorTemperature in the
         * range 1 to 65279 inclusive, giving a color temperature range from 1,000,000
         * Kelvins to 15.32 Kelvins).
         * <p>
         * The value ColorTemperature = 0 indicates an undefined value. The value
         * ColorTemperature = 65535 indicates an invalid value.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetColorTemperature(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORTEMPERATURE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_COLORTEMPERATURE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_COLORTEMPERATURE]);
        }

        /**
         * Set reporting for the <i>ColorTemperature</i> attribute [attribute ID <b>7</b>].
         * <p>
         * The ColorTemperature attribute contains a scaled inverse of the current value of
         * the color temperature. It is updated as fast as practical during commands that
         * change the color.
         * <p>
         * The color temperature value in Kelvins shall be related to the ColorTemperature
         * attribute by the relationship
         * <p>
         * Color temperature = 1,000,000 / ColorTemperature (ColorTemperature in the
         * range 1 to 65279 inclusive, giving a color temperature range from 1,000,000
         * Kelvins to 15.32 Kelvins).
         * <p>
         * The value ColorTemperature = 0 indicates an undefined value. The value
         * ColorTemperature = 65535 indicates an invalid value.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @param reportableChange {@link object} delta required to trigger report
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetColorTemperatureReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_COLORTEMPERATURE], minInterval, maxInterval, reportableChange);
        }

        /**
         * Get the <i>ColorMode</i> attribute [attribute ID <b>8</b>].
         * <p>
         * The ColorMode attribute indicates which attributes are currently determining the color of the device.
         * If either the CurrentHue or CurrentSaturation attribute is implemented, this attribute SHALL also be
         * implemented, otherwise it is optional. The value of the ColorMode attribute cannot be written directly
         * - it is Set upon reception of another command in to the appropriate mode for that command.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorModeAsync()
        {
            return Read(_attributes[ATTR_COLORMODE]);
        }

        /**
         * Synchronously Get the <i>ColorMode</i> attribute [attribute ID <b>8</b>].
         * <p>
         * The ColorMode attribute indicates which attributes are currently determining the color of the device.
         * If either the CurrentHue or CurrentSaturation attribute is implemented, this attribute SHALL also be
         * implemented, otherwise it is optional. The value of the ColorMode attribute cannot be written directly
         * - it is Set upon reception of another command in to the appropriate mode for that command.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetColorMode(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORMODE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_COLORMODE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_COLORMODE]);
        }

        /**
         * Get the <i>EnhancedCurrentHue</i> attribute [attribute ID <b>16384</b>].
         * <p>
         * The EnhancedCurrentHueattribute represents non-equidistant steps along the CIE 1931 color
         * triangle, and it provides 16-bits precision. The upper 8 bits of this attribute SHALL be
         * used as an index in the implementation specific XY lookup table to provide the non-equidistance
         * steps (see the ZLL test specification for an example).  The lower 8 bits SHALL be used to
         * interpolate between these steps in a linear way in order to provide color zoom for the user.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetEnhancedCurrentHueAsync()
        {
            return Read(_attributes[ATTR_ENHANCEDCURRENTHUE]);
        }

        /**
         * Synchronously Get the <i>EnhancedCurrentHue</i> attribute [attribute ID <b>16384</b>].
         * <p>
         * The EnhancedCurrentHueattribute represents non-equidistant steps along the CIE 1931 color
         * triangle, and it provides 16-bits precision. The upper 8 bits of this attribute SHALL be
         * used as an index in the implementation specific XY lookup table to provide the non-equidistance
         * steps (see the ZLL test specification for an example).  The lower 8 bits SHALL be used to
         * interpolate between these steps in a linear way in order to provide color zoom for the user.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetEnhancedCurrentHue(long refreshPeriod)
        {
            if (_attributes[ATTR_ENHANCEDCURRENTHUE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ENHANCEDCURRENTHUE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ENHANCEDCURRENTHUE]);
        }

        /**
         * Set reporting for the <i>EnhancedCurrentHue</i> attribute [attribute ID <b>16384</b>].
         * <p>
         * The EnhancedCurrentHueattribute represents non-equidistant steps along the CIE 1931 color
         * triangle, and it provides 16-bits precision. The upper 8 bits of this attribute SHALL be
         * used as an index in the implementation specific XY lookup table to provide the non-equidistance
         * steps (see the ZLL test specification for an example).  The lower 8 bits SHALL be used to
         * interpolate between these steps in a linear way in order to provide color zoom for the user.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param minInterval {@link int} minimum reporting period
         * @param maxInterval {@link int} maximum reporting period
         * @param reportableChange {@link object} delta required to trigger report
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> SetEnhancedCurrentHueReporting(ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(_attributes[ATTR_ENHANCEDCURRENTHUE], minInterval, maxInterval, reportableChange);
        }

        /**
         * Get the <i>EnhancedColorMode</i> attribute [attribute ID <b>16385</b>].
         * <p>
         * The EnhancedColorModeattribute specifies which attributes are currently determining the color of the device.
         * To provide compatibility with standard ZCL, the original ColorModeattribute SHALLindicate ‘CurrentHueand CurrentSaturation’
         * when the light uses the EnhancedCurrentHueattribute.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetEnhancedColorModeAsync()
        {
            return Read(_attributes[ATTR_ENHANCEDCOLORMODE]);
        }

        /**
         * Synchronously Get the <i>EnhancedColorMode</i> attribute [attribute ID <b>16385</b>].
         * <p>
         * The EnhancedColorModeattribute specifies which attributes are currently determining the color of the device.
         * To provide compatibility with standard ZCL, the original ColorModeattribute SHALLindicate ‘CurrentHueand CurrentSaturation’
         * when the light uses the EnhancedCurrentHueattribute.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetEnhancedColorMode(long refreshPeriod)
        {
            if (_attributes[ATTR_ENHANCEDCOLORMODE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_ENHANCEDCOLORMODE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_ENHANCEDCOLORMODE]);
        }

        /**
         * Get the <i>ColorLoopActive</i> attribute [attribute ID <b>16386</b>].
         * <p>
         * The ColorLoopActive attribute specifies the current active status of the color loop.
         * If this attribute has the value 0x00, the color loop SHALLnot be active. If this attribute
         * has the value 0x01, the color loop SHALL be active. All other values (0x02 – 0xff) are reserved.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorLoopActiveAsync()
        {
            return Read(_attributes[ATTR_COLORLOOPACTIVE]);
        }

        /**
         * Synchronously Get the <i>ColorLoopActive</i> attribute [attribute ID <b>16386</b>].
         * <p>
         * The ColorLoopActive attribute specifies the current active status of the color loop.
         * If this attribute has the value 0x00, the color loop SHALLnot be active. If this attribute
         * has the value 0x01, the color loop SHALL be active. All other values (0x02 – 0xff) are reserved.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetColorLoopActive(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORLOOPACTIVE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_COLORLOOPACTIVE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_COLORLOOPACTIVE]);
        }

        /**
         * Get the <i>ColorLoopDirection</i> attribute [attribute ID <b>16387</b>].
         * <p>
         * The ColorLoopDirection attribute specifies the current direction of the color loop.
         * If this attribute has the value 0x00, the EnhancedCurrentHue attribute SHALL be decremented.
         * If this attribute has the value 0x01, the EnhancedCurrentHue attribute SHALL be incremented.
         * All other values (0x02 – 0xff) are reserved.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorLoopDirectionAsync()
        {
            return Read(_attributes[ATTR_COLORLOOPDIRECTION]);
        }

        /**
         * Synchronously Get the <i>ColorLoopDirection</i> attribute [attribute ID <b>16387</b>].
         * <p>
         * The ColorLoopDirection attribute specifies the current direction of the color loop.
         * If this attribute has the value 0x00, the EnhancedCurrentHue attribute SHALL be decremented.
         * If this attribute has the value 0x01, the EnhancedCurrentHue attribute SHALL be incremented.
         * All other values (0x02 – 0xff) are reserved.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public byte GetColorLoopDirection(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORLOOPDIRECTION].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_COLORLOOPDIRECTION].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_COLORLOOPDIRECTION]);
        }

        /**
         * Get the <i>ColorLoopTime</i> attribute [attribute ID <b>16388</b>].
         * <p>
         * The ColorLoopTime attribute specifies the number of seconds it SHALL take to perform a full
         * color loop, i.e.,to cycle all values of the EnhancedCurrentHue attribute (between 0x0000 and 0xffff).
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorLoopTimeAsync()
        {
            return Read(_attributes[ATTR_COLORLOOPTIME]);
        }

        /**
         * Synchronously Get the <i>ColorLoopTime</i> attribute [attribute ID <b>16388</b>].
         * <p>
         * The ColorLoopTime attribute specifies the number of seconds it SHALL take to perform a full
         * color loop, i.e.,to cycle all values of the EnhancedCurrentHue attribute (between 0x0000 and 0xffff).
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetColorLoopTime(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORLOOPTIME].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_COLORLOOPTIME].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_COLORLOOPTIME]);
        }

        /**
         * Get the <i>ColorLoopStartHue</i> attribute [attribute ID <b>16389</b>].
         * <p>
         * The ColorLoopStartEnhancedHueattribute specifies the value of the EnhancedCurrentHue attribute
         * from which the color loop SHALL be started.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorLoopStartHueAsync()
        {
            return Read(_attributes[ATTR_COLORLOOPSTARTHUE]);
        }

        /**
         * Synchronously Get the <i>ColorLoopStartHue</i> attribute [attribute ID <b>16389</b>].
         * <p>
         * The ColorLoopStartEnhancedHueattribute specifies the value of the EnhancedCurrentHue attribute
         * from which the color loop SHALL be started.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetColorLoopStartHue(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORLOOPSTARTHUE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_COLORLOOPSTARTHUE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_COLORLOOPSTARTHUE]);
        }

        /**
         * Get the <i>ColorLoopStoredHue</i> attribute [attribute ID <b>16390</b>].
         * <p>
         * The ColorLoopStoredEnhancedHue attribute specifies the value of the EnhancedCurrentHue attribute
         * before the color loop was started. Once the color loop is complete, the EnhancedCurrentHue
         * attribute SHALL be restored to this value.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorLoopStoredHueAsync()
        {
            return Read(_attributes[ATTR_COLORLOOPSTOREDHUE]);
        }

        /**
         * Synchronously Get the <i>ColorLoopStoredHue</i> attribute [attribute ID <b>16390</b>].
         * <p>
         * The ColorLoopStoredEnhancedHue attribute specifies the value of the EnhancedCurrentHue attribute
         * before the color loop was started. Once the color loop is complete, the EnhancedCurrentHue
         * attribute SHALL be restored to this value.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetColorLoopStoredHue(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORLOOPSTOREDHUE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_COLORLOOPSTOREDHUE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_COLORLOOPSTOREDHUE]);
        }

        /**
         * Get the <i>ColorCapabilities</i> attribute [attribute ID <b>16394</b>].
         * <p>
         * The ColorCapabilitiesattribute specifies the color capabilities of the device supporting the
         * color control cluster.
         * <p>
         * Note:The support of the CurrentXand CurrentYattributes is mandatory regardless of color capabilities.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorCapabilitiesAsync()
        {
            return Read(_attributes[ATTR_COLORCAPABILITIES]);
        }

        /**
         * Synchronously Get the <i>ColorCapabilities</i> attribute [attribute ID <b>16394</b>].
         * <p>
         * The ColorCapabilitiesattribute specifies the color capabilities of the device supporting the
         * color control cluster.
         * <p>
         * Note:The support of the CurrentXand CurrentYattributes is mandatory regardless of color capabilities.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetColorCapabilities(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORCAPABILITIES].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_COLORCAPABILITIES].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_COLORCAPABILITIES]);
        }

        /**
         * Get the <i>ColorTemperatureMin</i> attribute [attribute ID <b>16395</b>].
         * <p>
         * The ColorTempPhysicalMinMiredsattribute indicates the minimum mired value
         * supported by the hardware. ColorTempPhysicalMinMiredscorresponds to the maximum
         * color temperature in kelvins supported by the hardware.
         * ColorTempPhysicalMinMireds ≤ ColorTemperatureMireds
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorTemperatureMinAsync()
        {
            return Read(_attributes[ATTR_COLORTEMPERATUREMIN]);
        }

        /**
         * Synchronously Get the <i>ColorTemperatureMin</i> attribute [attribute ID <b>16395</b>].
         * <p>
         * The ColorTempPhysicalMinMiredsattribute indicates the minimum mired value
         * supported by the hardware. ColorTempPhysicalMinMiredscorresponds to the maximum
         * color temperature in kelvins supported by the hardware.
         * ColorTempPhysicalMinMireds ≤ ColorTemperatureMireds
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetColorTemperatureMin(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORTEMPERATUREMIN].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_COLORTEMPERATUREMIN].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_COLORTEMPERATUREMIN]);
        }

        /**
         * Get the <i>ColorTemperatureMax</i> attribute [attribute ID <b>16396</b>].
         * <p>
         * The ColorTempPhysicalMaxMiredsattribute indicates the maximum mired value
         * supported by the hard-ware. ColorTempPhysicalMaxMiredscorresponds to the minimum
         * color temperature in kelvins supported by the hardware.
         * ColorTemperatureMireds ≤ ColorTempPhysicalMaxMireds.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> GetColorTemperatureMaxAsync()
        {
            return Read(_attributes[ATTR_COLORTEMPERATUREMAX]);
        }

        /**
         * Synchronously Get the <i>ColorTemperatureMax</i> attribute [attribute ID <b>16396</b>].
         * <p>
         * The ColorTempPhysicalMaxMiredsattribute indicates the maximum mired value
         * supported by the hard-ware. ColorTempPhysicalMaxMiredscorresponds to the minimum
         * color temperature in kelvins supported by the hardware.
         * ColorTemperatureMireds ≤ ColorTempPhysicalMaxMireds.
         * <p>
         * This method can return cached data if the attribute has already been received.
         * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received
         * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value
         * received. If <i>refreshPeriod</i> is Set to 0, then the attribute will always be updated.
         * <p>
         * This method will block until the response is received or a timeout occurs unless the current value is returned.
         * <p>
         * The attribute is of type {@link Integer}.
         * <p>
         * The implementation of this attribute by a device is OPTIONAL
         *
         * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed
         * @return the {@link Integer} attribute value, or null on error
         */
        public ushort GetColorTemperatureMax(long refreshPeriod)
        {
            if (_attributes[ATTR_COLORTEMPERATUREMAX].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_COLORTEMPERATUREMAX].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_COLORTEMPERATUREMAX]);
        }

        /**
         * The Move to Hue Command
         *
         * @param hue {@link Integer} Hue
         * @param direction {@link Integer} Direction
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveToHueCommand(byte hue, byte direction, byte transitionTime)
        {
            MoveToHueCommand command = new MoveToHueCommand();

            // Set the fields
            command.Hue = hue;
            command.Direction = direction;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Move Hue Command
         *
         * @param moveMode {@link Integer} Move mode
         * @param rate {@link Integer} Rate
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveHueCommand(byte moveMode, byte rate)
        {
            MoveHueCommand command = new MoveHueCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;

            return Send(command);
        }

        /**
         * The Step Hue Command
         *
         * @param stepMode {@link Integer} Step mode
         * @param stepSize {@link Integer} Step size
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> stepHueCommand(byte stepMode, byte stepSize, byte transitionTime)
        {
            StepHueCommand command = new StepHueCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Move to Saturation Command
         *
         * @param saturation {@link Integer} Saturation
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveToSaturationCommand(byte saturation, ushort transitionTime)
        {
            MoveToSaturationCommand command = new MoveToSaturationCommand();

            // Set the fields
            command.Saturation = saturation;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Move Saturation Command
         *
         * @param moveMode {@link Integer} Move mode
         * @param rate {@link Integer} Rate
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveSaturationCommand(byte moveMode, byte rate)
        {
            MoveSaturationCommand command = new MoveSaturationCommand();

            // Set the fields
            command.MoveMode = moveMode;
            command.Rate = rate;

            return Send(command);
        }

        /**
         * The Step Saturation Command
         *
         * @param stepMode {@link Integer} Step mode
         * @param stepSize {@link Integer} Step size
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> stepSaturationCommand(byte stepMode, byte stepSize, byte transitionTime)
        {
            StepSaturationCommand command = new StepSaturationCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Move to Hue and Saturation Command
         *
         * @param hue {@link Integer} Hue
         * @param saturation {@link Integer} Saturation
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveToHueAndSaturationCommand(byte hue, byte saturation, byte transitionTime)
        {
            MoveToHueAndSaturationCommand command = new MoveToHueAndSaturationCommand();

            // Set the fields
            command.Hue = hue;
            command.Saturation = saturation;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Move to Color Command
         *
         * @param colorX {@link Integer} ColorX
         * @param colorY {@link Integer} ColorY
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveToColorCommand(ushort colorX, ushort colorY, ushort transitionTime)
        {
            MoveToColorCommand command = new MoveToColorCommand();

            // Set the fields
            command.ColorX = colorX;
            command.ColorY = colorY;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Move Color Command
         *
         * @param rateX {@link Integer} RateX
         * @param rateY {@link Integer} RateY
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveColorCommand(short rateX, short rateY)
        {
            MoveColorCommand command = new MoveColorCommand();

            // Set the fields
            command.RateX = rateX;
            command.RateY = rateY;

            return Send(command);
        }

        /**
         * The Step Color Command
         *
         * @param stepX {@link Integer} StepX
         * @param stepY {@link Integer} StepY
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> stepColorCommand(short stepX, short stepY, ushort transitionTime)
        {
            StepColorCommand command = new StepColorCommand();

            // Set the fields
            command.StepX = stepX;
            command.StepY = stepY;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Move to Color Temperature Command
         *
         * @param colorTemperature {@link Integer} Color Temperature
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> moveToColorTemperatureCommand(ushort colorTemperature, ushort transitionTime)
        {
            MoveToColorTemperatureCommand command = new MoveToColorTemperatureCommand();

            // Set the fields
            command.ColorTemperature = colorTemperature;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Enhanced Move To Hue Command
         *
         * @param hue {@link Integer} Hue
         * @param direction {@link Integer} Direction
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> enhancedMoveToHueCommand(ushort hue, byte direction, ushort transitionTime)
        {
            EnhancedMoveToHueCommand command = new EnhancedMoveToHueCommand();

            // Set the fields
            command.Hue = hue;
            command.Direction = direction;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Enhanced Step Hue Command
         *
         * @param stepMode {@link Integer} Step Mode
         * @param stepSize {@link Integer} Step Size
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> enhancedStepHueCommand(byte stepMode, ushort stepSize, ushort transitionTime)
        {
            EnhancedStepHueCommand command = new EnhancedStepHueCommand();

            // Set the fields
            command.StepMode = stepMode;
            command.StepSize = stepSize;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Enhanced Move To Hue and Saturation Command
         *
         * @param hue {@link Integer} Hue
         * @param saturation {@link Integer} Saturation
         * @param transitionTime {@link Integer} Transition time
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> enhancedMoveToHueAndSaturationCommand(ushort hue, byte saturation, ushort transitionTime)
        {
            EnhancedMoveToHueAndSaturationCommand command = new EnhancedMoveToHueAndSaturationCommand();

            // Set the fields
            command.Hue = hue;
            command.Saturation = saturation;
            command.TransitionTime = transitionTime;

            return Send(command);
        }

        /**
         * The Color Loop Set Command
         *
         * @param updateFlags {@link Integer} Update Flags
         * @param action {@link Integer} Action
         * @param direction {@link Integer} Direction
         * @param transitionTime {@link Integer} Transition time
         * @param startHue {@link Integer} Start Hue
         * @return the {@link Task<CommandResult>} command result future
         */
        public Task<CommandResult> ColorLoopSetCommand(byte updateFlags, byte action, byte direction, ushort transitionTime, ushort startHue)
        {
            ColorLoopSetCommand command = new ColorLoopSetCommand();

            // Set the fields
            command.UpdateFlags = updateFlags;
            command.Action = action;
            command.Direction = direction;
            command.TransitionTime = transitionTime;
            command.StartHue = startHue;

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // MOVE_TO_HUE_COMMAND
                    return new MoveToHueCommand();
                case 1: // MOVE_HUE_COMMAND
                    return new MoveHueCommand();
                case 2: // STEP_HUE_COMMAND
                    return new StepHueCommand();
                case 3: // MOVE_TO_SATURATION_COMMAND
                    return new MoveToSaturationCommand();
                case 4: // MOVE_SATURATION_COMMAND
                    return new MoveSaturationCommand();
                case 5: // STEP_SATURATION_COMMAND
                    return new StepSaturationCommand();
                case 6: // MOVE_TO_HUE_AND_SATURATION_COMMAND
                    return new MoveToHueAndSaturationCommand();
                case 7: // MOVE_TO_COLOR_COMMAND
                    return new MoveToColorCommand();
                case 8: // MOVE_COLOR_COMMAND
                    return new MoveColorCommand();
                case 9: // STEP_COLOR_COMMAND
                    return new StepColorCommand();
                case 10: // MOVE_TO_COLOR_TEMPERATURE_COMMAND
                    return new MoveToColorTemperatureCommand();
                case 64: // ENHANCED_MOVE_TO_HUE_COMMAND
                    return new EnhancedMoveToHueCommand();
                case 65: // ENHANCED_STEP_HUE_COMMAND
                    return new EnhancedStepHueCommand();
                case 66: // ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND
                    return new EnhancedMoveToHueAndSaturationCommand();
                case 67: // COLOR_LOOP_SET_COMMAND
                    return new ColorLoopSetCommand();
                default:
                    return null;
            }
        }
    }
}