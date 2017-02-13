using UnityEngine;
using System.Collections;

namespace proto
{
    public struct OBJECT_ACK_CLIENT_MATCH_T
    {
        public static ushort cmd = 50009;

        public void clear()
        {
            m_errno = 0;
        }

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out m_errno))
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

            return true;
        }

        public ushort m_errno;
    }
}
