using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_REVIVE_COORD_T
    {
        public static ushort cmd = 50025;

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out role_id))
            {
                return false;
            }

            if (!m_pos.read(ref msg))
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

            if (!m_pos.write(ref msg))
            {
                return false;
            }

            return true;
        }

        public uint role_id;
        public vector2_t m_pos;
    }
}
