using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_GATEWAY_QUERY_ROLE_INFO_T
    {
        public static ushort cmd = 107;

        public void clear()
        {

        }

        public bool read(ref framework.message_t msg)
        {
            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            return true;
        }

    }
}