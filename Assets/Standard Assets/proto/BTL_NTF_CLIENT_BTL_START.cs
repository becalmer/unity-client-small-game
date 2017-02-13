using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_BTL_START_T
    {
        public static ushort cmd = 50017;

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
