﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Groups;


namespace ZigBeeNet.ZCL.Clusters.Groups
{
    /// <summary>
    /// Remove Group Command value object class.
    /// <para>
    /// Cluster: Groups. Command is sent TO the server.
    /// This command is a specific command used for the Groups cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveGroupCommand : ZclCommand
    {
        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupID { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveGroupCommand()
        {
            GenericCommand = false;
            ClusterId = 4;
            CommandId = 3;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RemoveGroupCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupID=");
            builder.Append(GroupID);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
