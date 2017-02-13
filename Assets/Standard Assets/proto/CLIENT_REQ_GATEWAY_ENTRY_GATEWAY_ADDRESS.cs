using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_GATEWAY_ENTRY_GATEWAY_ADDRESS_T
    {
        public static ushort cmd = 21;

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