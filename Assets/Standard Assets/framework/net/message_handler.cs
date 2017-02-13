using UnityEngine;
using System.Collections;

namespace framework
{
    public delegate bool cb_t(ref message_t msg);

    public class message_event_cb_t
    {
        private cb_t m_cb;

        public message_event_cb_t()
        {
            m_cb = null;
        }

        public void set_cb(cb_t cb)
        {
            m_cb = cb;
        }

        public bool handler(ref message_t msg)
        {
            if(null != m_cb)
            {
                return m_cb(ref msg);
            }
            return false;
        }
    }

    public class message_handler_t
    {
		private const int max = 1024;
        private const int begin = 50001;

        public message_handler_t()
        {
            try
            {
				m_events = new message_event_cb_t[max];
				for(int x = 0; x < max; ++ x)
				{
					m_events[x] = new message_event_cb_t();
				}

                m_msg = new message_t();
            }
            catch(System.Exception)
            {
                throw (new System.Exception("message_handler_t constructor new failed!"));
            }
        }

        ~message_handler_t()
        {
            m_events = null;

            m_msg = null;
        }

        public bool handler(ref message_t msg)
        {
            uint y = msg.id();

            uint size = msg.size();

            //sys_t.g_sys.m_log.write("[msg id = " + y.ToString() + ", size = " + size.ToString() + "]\n");

            if(heart_beat.ID == y)
            {
                return true;
            }

			if((y >= (begin + max)) || (y < begin))
            {
                return false;
            }

            message_event_cb_t func = m_events[y - begin];

            if (null != func)
            {
                return func.handler(ref msg);
            }

            return false;
        }

        public void mount(int id, cb_t cb)
        {
			if((id >= begin) && (id < (begin + max)))
            {
                m_events[id - begin].set_cb(cb);
            }
        }

        public message_event_cb_t[] m_events;

        public message_t m_msg;             
    }

}