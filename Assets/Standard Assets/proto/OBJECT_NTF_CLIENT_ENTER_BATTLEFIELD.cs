using UnityEngine;
using System.Collections;

namespace proto
{
    public struct OBJECT_NTF_CLIENT_ENTER_BATTLEFIELD_T
    {
        public static ushort cmd = 50010;

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

            if (!m_pos.read(ref msg))
            {
                return false;
            }

            if (!m_dir.read(ref msg))
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

            if (!m_pos.write(ref msg))
            {
                return false;
            }

            if (!m_dir.write(ref msg))
            {
                return false;
            }

            return true;
        }

        public ushort m_errno;
        public vector2_t m_pos;
        public vector2_t m_dir;
    }
}
