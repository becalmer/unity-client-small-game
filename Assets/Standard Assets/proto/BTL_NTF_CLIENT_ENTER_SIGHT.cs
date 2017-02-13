﻿using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_ENTER_SIGHT_T
    {
        public static ushort cmd = 50011;

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
                role_data_t info;

                info.m_role_id = 0;
                info.m_nick = "";
                info.m_pos.x = 0;
                info.m_pos.z = 0;
                info.m_dir.x = 0;
                info.m_dir.z = 0;

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

        public System.Collections.Generic.List<role_data_t> m_list;
    }
}
