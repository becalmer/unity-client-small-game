using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_NTF_BTL_PAUSE_T
    {
        public static ushort cmd = 5004;

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
