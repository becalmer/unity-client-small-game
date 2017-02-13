using UnityEngine;
using System.Collections;

namespace framework
{
    public class message_t : buffer_t
    {

        public message_t()
        {

        }

        ~message_t()
        {

        }

		public new void clear()
        {
            m_sz = 0;
            base.clear();
            base.overx();
        }

        public void set_id(ushort id)
        {
            clear();

            write(id);

            uint c = 0;

            write(c);
        }

        public ushort id()
        {
            ushort x = 0;

            read(out x);

            return x;
        }

        public uint size()
        {
            uint x = 0;

            read(out x);

            return x;
        }

    }
}
