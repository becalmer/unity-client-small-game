using UnityEngine;
using System.Collections;

namespace framework
{
    public class buffer_t
    {
        const int strsize = 4096;

        public buffer_t()
        {

            m_sz = 0;

            m_size = (1 << 21);

            try
            {
                m_buff = new char[strsize];

                m_ms = new System.IO.MemoryStream(m_size);

                m_br = new System.IO.BinaryReader(m_ms);

                m_bw = new System.IO.BinaryWriter(m_ms);
            }
            catch (System.Exception)
            {

            }

            clear();

        }

       ~buffer_t()
        {
            m_ms = null;

            m_br = null;

            m_bw = null;
        }

        public virtual void clear()
        {
            m_r_pos = m_w_pos = 0;
        }

        public void try_adjust(int x)
        {
            if((x == 1) || (m_r_pos >= m_w_pos))
            {
                clear();
            }
        }

        public bool write(System.String s)
        {
            System.UInt16 x = (System.UInt16)s.Length;

            if (!write(x))
            {
                return false;
            }

            if ((x + m_w_pos) > m_size)
            {
                return false;
            }

            m_bw.Write(s.ToCharArray(), 0, x);

            update_w_pos(x);

            return true;
        }

        public bool write(uint x)
        {
            if ((m_w_pos + 4) > m_size)
            {
                return false;
            }

            m_bw.Write(x);

            update_w_pos(4);

            return true;
        }

        public bool write(int x)
        {
            if ((m_w_pos + 4) > m_size)
            {
                return false;
            }

            m_bw.Write(x);

            update_w_pos(4);

            return true;
        }

        public bool write(ushort x)
        {
            if ((m_w_pos + 2) > m_size)
            {
                return false;
            }

            m_bw.Write(x);

            update_w_pos(2);

            return true;
        }

        public bool write(byte x)
        {
            if ((m_w_pos + 1) > m_size)
            {
                return false;
            }

            m_bw.Write(x);

            update_w_pos(1);

            return true;
        }

        public bool write(byte[] b, uint l)
        {
            if ((m_w_pos + l) > m_size)
            {
                return false;
            }

            m_bw.Write(b, 0, (int)l);

            update_w_pos((int)l);

            return true;
        }

        private void update_w_pos(int x)
        {
            m_w_pos += x;

            m_sz += (uint)x;
        }

        public bool read(out System.String s)
        {
            s = "";

            ushort x = 0;

            if (!read(out x))
            {
                return false;
            }
            
            if ((x + m_r_pos) > m_w_pos)
            {
                return false;
            }

            if(x > 0)
            {
                m_br.Read(m_buff, 0, x);
                s = new System.String(m_buff, 0, x);
            }

            m_r_pos += x;

            try_adjust(0);

            return true;
        }

        public bool read(out uint x)
        {
            x = 0;

            if ((m_r_pos + 4) > m_w_pos)
            {
                return false;
            }

            x = m_br.ReadUInt32();

            m_r_pos += 4;

            try_adjust(0);

            return true;
        }

        public bool read(out int x)
        {
            x = 0;

            if ((m_r_pos + 4) > m_w_pos)
            {
                return false;
            }

            x = m_br.ReadInt32();

            m_r_pos += 4;

            try_adjust(0);

            return true;
        }

        public bool read(out ushort x)
        {
            x = 0;

            if ((m_r_pos + 2) > m_w_pos)
            {
                return false;
            }

            x = m_br.ReadUInt16();

            m_r_pos += 2;

            try_adjust(0);

            return true;
        }

        public bool read(out byte x)
        {
            x = 0;

            if ((m_r_pos + 1) > m_w_pos)
            {
                return false;
            }

            x = m_br.ReadByte();

            m_r_pos += 1;

            try_adjust(0);

            return true;
        }

        public void over()
        {
            m_ms.Seek(2, System.IO.SeekOrigin.Begin);

            m_sz -= 6;

            m_bw.Write(m_sz);

            m_ms.Seek(0, System.IO.SeekOrigin.End);
        }

        public void overx()
        {
            m_ms.Seek(0, System.IO.SeekOrigin.Begin);
        }        

        public int m_size;
        public int m_r_pos;
        public int m_w_pos;

        public char[] m_buff;

        public System.IO.MemoryStream m_ms;

        public System.IO.BinaryReader m_br;
        public System.IO.BinaryWriter m_bw;

        public uint m_sz;
    }
}
