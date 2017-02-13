using UnityEngine;
using System.Collections;

namespace proto
{
    public struct socket_addr_t
    {
        public void clear()
        {
            m_ip = 0;
            m_port = 0;
        }

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out m_ip))
            {
                return false;
            }

            if (!msg.read(out m_port))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(m_ip))
            {
                return false;
            }

            if (!msg.write(m_port))
            {
                return false;
            }

            return true;
        }
        
        public uint   m_ip;
		public ushort m_port;

    }
}
