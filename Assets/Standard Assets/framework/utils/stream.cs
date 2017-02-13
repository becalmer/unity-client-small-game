using UnityEngine;
using System.Collections;

namespace framework
{

    public class stream_t
    {

        public stream_t()
        {
            m_size = (1 << 24);

            try
            {
                m_buf = new byte[1 << 16];

                m_ms = new System.IO.MemoryStream(m_size);

                m_br = new System.IO.BinaryReader(m_ms);
            }
            catch(System.Exception)
            {
                throw (new System.Exception("stream constructor new failed!"));
            }
                        
            clear();
        }

        ~stream_t()
        {
            m_ms = null;

            m_br = null;
        }

        public bool write(ref byte[] buf, int len)
        {
            //sys_t.g_sys.m_log.write("[write 1 m_r_pos = " + m_r_pos.ToString() + ", m_w_pos = " + m_w_pos.ToString() + ", len = " + len.ToString() + " ]\n");

            int size = get_size();

            if ((len + size) >= m_size)
            {
                return false;
            }

            m_ms.Seek(m_w_pos, System.IO.SeekOrigin.Begin);
            
            m_ms.Write(buf, 0, len);

            m_w_pos += len;

            //sys_t.g_sys.m_log.write("[write 2 m_r_pos = " + m_r_pos.ToString() + ", m_w_pos = " + m_w_pos.ToString() + ", len = " + len.ToString() + " ]\n");

            return true;
        }

        public bool can_read(ref message_t msg)
        {
            if ((m_r_pos + 6) > m_w_pos)
            {
                return false;
            }

            m_ms.Seek(m_r_pos, System.IO.SeekOrigin.Begin);

            ushort id = m_br.ReadUInt16();

            uint sz = m_br.ReadUInt32();

            int s = 0;

            if (sz != 0)
            {
                s = m_br.Read(m_buf, 0, (int)sz);
            }

            int off = 6 + s;

            if (s != sz)
            {
                m_ms.Seek(-off, System.IO.SeekOrigin.Current);
                return false;
            }

            m_r_pos += 6 + (int)sz;

            msg.write(id);

            msg.write(sz);

            msg.write(m_buf, sz);

            offset_read(0);

            msg.overx();

            //sys_t.g_sys.m_log.write("[id = " + id.ToString() + ", size = " + sz.ToString() + ", m_r_pos = " + m_r_pos.ToString() + ", m_w_pos = " + m_w_pos.ToString() + "]\n");
            return true;
        }
        
        public void offset_read(int offset)
        {
            if(offset != 0)
            {
                m_ms.Seek(-offset, System.IO.SeekOrigin.Current);
            }

            m_r_pos += offset;

            if (m_r_pos >= m_w_pos)
            {
                m_r_pos = 0;
                m_w_pos = 0;

                m_ms.Seek(0, System.IO.SeekOrigin.Begin);
            }
        }

        public int get_size()
        {
            return (m_w_pos - m_r_pos);
        }

        public void clear()
        {
            m_r_pos = m_w_pos = 0;
        }

        public int assign_buf()
        {
            m_ms.Seek(m_r_pos, System.IO.SeekOrigin.Begin);

            int sz = get_size();

            int max = (1 << 16);

            if (sz >= max)
            {
                sz = max;
            }

            m_r_pos += sz;

            return m_ms.Read(m_buf, 0, sz);
        } 

        public byte[] m_buf;

        private System.IO.MemoryStream m_ms;

        private System.IO.BinaryReader m_br;

        public int m_r_pos;
        public int m_w_pos;
        private int m_size;
    }

}