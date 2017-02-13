using UnityEngine;
using System.Collections;

namespace proto
{
    public struct role_data_t
    {

        public void clear()
        {
            m_role_id = 0;
            m_nick = "";
        }

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out m_role_id))
            {
                return false;
            }

            if (!msg.read(out m_nick))
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
            if (!msg.write(m_role_id))
            {
                return false;
            }

            if (!msg.write(m_nick))
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

        public uint m_role_id;
        public System.String m_nick;
        public vector2_t m_pos;
        public vector2_t m_dir;
    }
}
