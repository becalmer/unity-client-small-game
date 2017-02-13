using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_OBJECT_MATCH_T
    {
        public static ushort cmd = 1005;

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