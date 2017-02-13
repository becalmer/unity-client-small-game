using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_RESUME_COMPLETE_T
    {
        public static ushort cmd = 50027;

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
