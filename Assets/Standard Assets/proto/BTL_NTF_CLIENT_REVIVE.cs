using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_REVIVE_T
    {
        public static ushort cmd = 50026;

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out role_id))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(role_id))
            {
                return false;
            }

            return true;
        }

        public uint role_id;
    }
}
