using UnityEngine;
using System.Collections;

namespace proto
{
    public struct rank_changed_info_t
    {

        public void clear()
        {
            m_nth = 0;
            m_role_id = 0;
        }

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out m_nth))
            {
                return false;
            }

            if (!msg.read(out m_role_id))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(m_nth))
            {
                return false;
            }

            if (!msg.write(m_role_id))
            {
                return false;
            }

            return true;
        }

        public byte m_nth;
        public uint m_role_id;
    }
}
