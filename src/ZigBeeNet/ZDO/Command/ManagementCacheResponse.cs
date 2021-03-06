using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Cache Response value object class.
    /// 
    /// The Mgmt_Cache_rsp is generated in response to an Mgmt_Cache_req. If this
    /// management command is not supported, or the Remote Device is not a Primary
    /// Cache Device, a status of NOT_SUPPORTED shall be returned and all parameter
    /// fields after the Status field shall be omitted. Otherwise, the Remote Device shall
    /// implement the following processing. Upon receipt of the Mgmt_Cache_req and
    /// after support for the Mgmt_Cache_req has been verified, the Remote Device shall
    /// access an internally maintained list of registered ZigBee End Devices utilizing the
    /// discovery cache on this Primary Discovery Cache device. The entries reported
    /// shall be those, starting with StartIndex and including whole DiscoveryCacheList
    /// records until the limit on MSDU size, i.e., aMaxMACFrameSize, is reached. Within
    /// the Mgmt_Cache_rsp command, the
    /// DiscoveryCacheListEntries field shall represent the total number of registered
    /// entries in the Remote Device. The parameter DiscoveryCacheListCount shall be
    /// the number of entries reported in the DiscoveryCacheList field of the
    /// Mgmt_Cache_rsp command.
    /// 
    /// </summary>
    public class ManagementCacheResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementCacheResponse()
        {
            ClusterId = 0x8037;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementCacheResponse [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}
