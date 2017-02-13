using UnityEngine;
using System.Collections;

namespace proto
{
    public struct OBJECT_NTF_CLIENT_BTL_OVER_T
    {
        public static ushort cmd = 50018;

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
