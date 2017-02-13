using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_GATEWAY_CREATE_ROLE_T
    {
        public static ushort cmd = 109;

        public void clear()
        {
            m_nick = "";
        }

        public bool read(ref framework.message_t msg)
        {
            if (!msg.read(out m_nick))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(m_nick))
            {
                return false;
            }

            return true;
        }

        public System.String m_nick;
    }
}