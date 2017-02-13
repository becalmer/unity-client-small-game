using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_ACK_CLIENT_RANK_T
    {
        public static ushort cmd = 50023;

        public bool read(ref framework.message_t msg)
        {
            if (!msg.read(out m_nth))
            {
                return false;
            }

            byte c = 0;
            if(!msg.read(out c))
            {
                return false;
            }

            int num = c;

            for(int x = 0; x < num; ++ x)
            {
                rank_role_data_t info;

                info.m_role_id = 0;
                info.m_nick = "";

                if (!info.read(ref msg))
                {
                    return false;
                }
                
                m_list.Add(info);
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(m_nth))
            {
                return false;
            }

            int num = (byte)m_list.Count;

            byte c = (byte)num;

            if (!msg.write(c))
            {
                return false;
            }

            for (int x = 0; x < num; ++x)
            {
                if (!m_list[x].write(ref msg))
                {
                    return false;
                }
            }

            return true;
        }

        public byte m_nth;
        public System.Collections.Generic.List<rank_role_data_t> m_list;
    }
}
