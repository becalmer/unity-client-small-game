using UnityEngine;
using System.Collections;

namespace proto
{
    public struct BTL_NTF_CLIENT_DAMAGE_T
    {
        public static ushort cmd = 50020;

        public bool read(ref framework.message_t msg)
        {
            if(!msg.read(out atk_role_id))
            {
                return false;
            }

            if (!msg.read(out target_role_id))
            {
                return false;
            }

            if (!msg.read(out damage))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(atk_role_id))
            {
                return false;
            }

            if (!msg.write(target_role_id))
            {
                return false;
            }

            if (!msg.write(damage))
            {
                return false;
            }

            return true;
        }

        public uint atk_role_id;
        public uint target_role_id;
        public uint damage;
    }
}
