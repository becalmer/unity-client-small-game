using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_RESUME_T
    {
        public static ushort cmd = 50016;

        public bool read(ref framework.message_t msg)
        {
            if (!msg.read(out role_id))
            {
                return false;
            }

            if (!dir.read(ref msg))
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

            if (!dir.write(ref msg))
            {
                return false;
            }

            return true;
        }

        public uint role_id;
        public vector2_t dir;
    }
}
