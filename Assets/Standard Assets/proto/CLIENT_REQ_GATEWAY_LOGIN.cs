using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_GATEWAY_LOGIN_T
    {
        public static ushort cmd = 103;

        public void clear()
        {
            m_user = "";
            m_password = "";
        }

        public bool read(ref framework.message_t msg)
        {
            if (!msg.read(out m_user))
            {
                return false;
            }
            if (!msg.read(out m_password))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(m_user))
            {
                return false;
            }
            if (!msg.write(m_password))
            {
                return false;
            }

            return true;
        }

        public System.String m_user;
        public System.String m_password;
    }
}