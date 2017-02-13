using UnityEngine;
using System.Collections;

namespace proto
{
    public struct role_dir_t
    {

        public void clear()
        {
            m_role_id = 0;
        }

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out m_role_id))
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

            if (!m_dir.write(ref msg))
            {
                return false;
            }

            return true;
        }

        public uint m_role_id;
        public vector2_t m_dir;
    }
}
