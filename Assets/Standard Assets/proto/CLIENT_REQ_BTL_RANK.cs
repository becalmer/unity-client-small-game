using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_BTL_RANK_T
    {
        public static ushort cmd = 5006;

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
