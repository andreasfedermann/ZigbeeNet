using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Complex Descriptor Response value object class.
    /// 
    /// The Complex_Desc_rsp is generated by a remote device in response to a
    /// Complex_Desc_req directed to the remote device. This command shall be unicast
    /// to the originator of the Complex_Desc_req command.
    /// 
    /// </summary>
    public class ComplexDescriptorResponse : ZdoResponse
    {
        /// <summary>
        /// NWKAddrOfInterest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Length command message field.
        /// </summary>
        public byte Length { get; set; }

        /// <summary>
        /// ComplexDescriptor command message field.
        /// </summary>
        public ComplexDescriptor ComplexDescriptor { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ComplexDescriptorResponse()
        {
            ClusterId = 0x8010;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(Length, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ComplexDescriptor, ZclDataType.Get(DataType.COMPLEX_DESCRIPTOR));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            NwkAddrOfInterest = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            Length = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ComplexDescriptor = (ComplexDescriptor)deserializer.Deserialize(ZclDataType.Get(DataType.COMPLEX_DESCRIPTOR));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ComplexDescriptorResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(", length=")
                   .Append(Length)
                   .Append(", complexDescriptor=")
                   .Append(ComplexDescriptor)
                   .Append(']');

            return builder.ToString();
        }
    }
}
