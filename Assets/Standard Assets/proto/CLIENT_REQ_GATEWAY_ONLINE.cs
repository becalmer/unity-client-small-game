using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_GATEWAY_ONLINE_T
    {
        public static ushort cmd = 111;

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