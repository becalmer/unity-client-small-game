using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_LEAVE_SIGHT_T
    {
        public static ushort cmd = 50012;

        public void clear()
        {
        }

        public bool read(ref framework.message_t msg)
        {
            byte c = 0;
            if(!msg.read(out c))
            {
                return false;
            }

            int num = c;

            for(int x = 0; x < num; ++ x)
            {
                uint r = 0;

                if(!msg.read(out r))
                {
                    return false;
                }

                m_list.Add(r);
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            int num = m_list.Count;

            byte c = (byte)num;

            if (!msg.write(c))
            {
                return false;
            }

            for (int x = 0; x < num; ++x)
            {
                uint r = m_list[x];

                if (!msg.write(r))
                {
                    return false;
                }

                m_list.Add(r);
            }

            return true;
        }

        public System.Collections.Generic.List<uint> m_list;
    }
}
