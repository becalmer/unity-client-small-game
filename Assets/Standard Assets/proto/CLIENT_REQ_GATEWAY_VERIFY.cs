using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_REQ_GATEWAY_VERIFY_T
    {
        public static ushort cmd = 101;

        public void clear()
        {
            m_key = 0;
        }

        public bool read(ref framework.message_t msg)
        {
            if (!msg.read(out m_key))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(m_key))
            {
                return false;
            }

            return true;
        }

        public uint m_key;
    }
}