using UnityEngine;
using System.Collections;

namespace proto
{
    public struct GATEWAY_ACK_CLIENT_QUERY_ROLE_INFO_T
    {
        public static ushort cmd = 50005;

        public void clear()
        {
            m_errno = 0;
            m_info.clear();
        }

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out m_errno))
            {
                return false;
            }

            if(!m_info.read(ref msg))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(m_errno))
            {
                return false;
            }
            
            if (!m_info.write(ref msg))
            {
                return false;
            }

            return true;
        }

        public ushort m_errno;

		public grole_info_t m_info;
    }
}
