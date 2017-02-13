using UnityEngine;
using System.Collections;

namespace proto
{
    public struct CLIENT_NTF_BTL_RESUME_T
    {
        public static ushort cmd = 5005;

        public bool read(ref framework.message_t msg)
        {
            if (!dir.read(ref msg))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if(!dir.write(ref msg))
            {
                return false;
            }

            return true;
        }

        public vector2_t dir;
    }
}
