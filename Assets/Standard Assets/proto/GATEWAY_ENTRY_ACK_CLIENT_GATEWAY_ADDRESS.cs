using UnityEngine;
using System.Collections;

namespace proto
{
    public struct GATEWAY_ENTRY_ACK_CLIENT_GATEWAY_ADDRESS_T
    {
        public static ushort cmd = 50001;

        public void clear()
        {
            m_errno = 0;
            m_key = 0;
            m_addr.clear();
        }

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out m_errno))
            {
                return false;
            }

            if (!msg.read(out m_key))
            {
                return false;
            }

            if(!m_addr.read(ref msg))
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

            if (!msg.write(m_key))
            {
                return false;
            }

            if (!m_addr.write(ref msg))
            {
                return false;
            }

            return true;
        }

        public ushort m_errno;
		public uint m_key;

		public socket_addr_t m_addr;
    }
}
