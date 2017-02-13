using UnityEngine;
using System.Collections;

namespace proto
{
    public struct vector2_t
    {

        public void clear()
        {
            x = 0;
            z = 0;
        }

        public bool read(ref framework.message_t msg)
        {
            if (!msg.read(out x))
            {
                return false;
            }

            if (!msg.read(out z))
            {
                return false;
            }

            return true;
        }

        public bool write(ref framework.message_t msg)
        {
            if (!msg.write(x))
            {
                return false;
            }

            if (!msg.write(z))
            {
                return false;
            }

            return true;
        }

        public int x;
        public int z;
    }
}
