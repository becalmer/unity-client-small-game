using UnityEngine;
using System.Collections;

namespace framework
{
    public delegate void client_event_t();

    public class client_t
    {
        enum state_t
        {
            CS_IDLE,
            CS_OPEN,
            CS_CONNECTING,
            CS_THREAD_CONNECTING,
            CS_CONNECTING_ASYNC,
            CS_CONNECTED,
        };

        public client_t()
        {

            m_state = state_t.CS_IDLE;

            m_size = (1 << 20);

            try
            {
                m_buffer = new byte[m_size];

                m_i_stream = new stream_t();

                m_o_stream = new stream_t();

                m_message_handler = new message_handler_t();

                m_read_list = new ArrayList();

                m_write_list = new ArrayList();

                m_except_list = new ArrayList();
            }
            catch(System.Exception)
            {

            }

            m_connect_deadline = 0;

        }

        ~client_t()
        {
            if(m_socket != null)
            {
                m_socket.Close();
            }

            m_buffer = null;

            m_i_stream = null;

            m_o_stream = null;

            m_message_handler = null;

            m_read_list = null;

            m_write_list = null;

            m_except_list = null;

            m_connected = null;

            m_timeout = null;

            m_closed = null;
        }

        public void clear()
        {
            if(state_t.CS_CONNECTED == m_state)
            {
                m_socket.Close();
            }

            m_i_stream.clear();

            m_o_stream.clear();

            m_closed();

            m_state = state_t.CS_IDLE;

            m_socket = null;
        }

        public void set_event(client_event_t connected, client_event_t timeout, client_event_t closed)
        {
            m_connected = connected;
            m_timeout = timeout;
            m_closed = closed;
        }

        public void set_addr(string addr, int port)
        {
            m_addr = addr;
            m_port = port;

            m_state = state_t.CS_OPEN;

            m_connect_deadline = System.DateTime.Now.Ticks + 100000000;
        }

        public void tick()
        {
            switch(m_state)
            {
                case state_t.CS_IDLE:
                {
                    break;
                }
                case state_t.CS_OPEN:
                {
                    on_open();
                    break;
                }
                case state_t.CS_CONNECTING:
                {
                    on_connecting();
                    break;
                }
                case state_t.CS_THREAD_CONNECTING:
                {
                    break;
                }
                case state_t.CS_CONNECTING_ASYNC:
                {
                    on_connecting_async();
                    break;
                }
                case state_t.CS_CONNECTED:
                {
                    on_connected();
                    break;
                }
            }
        }

        private void on_open()
        {
            try
            {
                m_socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
            }
            catch (System.Exception)
            {
                throw (new System.Exception("client construct, new failed!"));
            }

            m_state = state_t.CS_CONNECTING;
        }

        private static void connecting(object obj)
        {
            client_t cli = (client_t)obj;

            try
            {
                cli.m_socket.Connect(cli.m_addr, cli.m_port);
            }
            catch (System.Net.Sockets.SocketException)
            {

            }

            cli.m_state = state_t.CS_CONNECTING_ASYNC;
        }

        private void on_connecting()
        {
            m_state = state_t.CS_THREAD_CONNECTING;

            try
            {

                System.Threading.Thread th = new System.Threading.Thread(connecting);

                th.Start(this);

            }
            catch(System.Exception)
            {

            }

        }

        public void on_connecting_async()
        {

            if (m_socket.Connected)
            {
                m_state = state_t.CS_CONNECTED;

                m_connected();

                m_heart_beat.interactive();
            }
            else
            {
                if(System.DateTime.Now.Ticks >= m_connect_deadline)
                {
                    clear();

                    m_connect_deadline = 0;

                    m_timeout();

                    return;
                }
            }
        }

        private void on_connected()
        {
            if(!m_socket.Connected)
            {
                clear();

                return;
            }

            m_read_list.Add(m_socket);

            m_write_list.Add(m_socket);

            m_except_list.Add(m_socket);

            System.Net.Sockets.Socket.Select(m_read_list, m_write_list, m_except_list, 10);

            if(m_read_list.Count > 0)
            {
                if (m_socket.Available > 0)
                {
                    int size = m_socket.Receive(m_buffer);

                    if(size > 0)
                    {
                        //sys_t.g_sys.m_log.write("[istream]\n");
                        m_i_stream.write(ref m_buffer, size);

                        m_heart_beat.interactive();
                    }
                }
            }
            else
            {
                heart_beat_tick();
            }

            if(m_write_list.Count > 0)
            {
                int sz = m_o_stream.get_size();

                if (sz > 0)
                {
                    int len = m_o_stream.assign_buf();

                    int size = m_socket.Send(m_o_stream.m_buf, 0, len, System.Net.Sockets.SocketFlags.None);

                    if(size > 0)
                    {
                        m_o_stream.offset_read(len - size);
                        m_heart_beat.interactive();
                    }
                }
            }

            m_read_list.Clear();

            m_write_list.Clear();

            if (m_except_list.Count > 0)
            {
                clear();

                m_except_list.Clear();

                return;
            }

            m_except_list.Clear();

            if (m_i_stream.get_size() > 0)
            {
                if (m_i_stream.can_read(ref m_message_handler.m_msg))
                {
                    //sys_t.g_sys.m_log.write("[state = " + m_state.ToString() + "]\n");
                    if(!m_message_handler.handler(ref m_message_handler.m_msg))
                    {
                        clear();

                        return;
                    }

                    m_message_handler.m_msg.clear();
                }
            }
        }

        public void send(ref message_t msg)
        {
            if(m_state != state_t.CS_CONNECTED)
            {
                return;
            }

            byte[] buf = msg.m_ms.GetBuffer();

            //sys_t.g_sys.m_log.write("[ostream]\n");
            m_o_stream.write(ref buf, msg.m_w_pos);

            msg.clear();
        }

        void heart_beat_tick()
        {
            if(m_heart_beat.is_interactive())
            {
                if(m_heart_beat.die())
                {
                    heartbeat();
                    m_heart_beat.heartbeat();
                }
            }
            else if(m_heart_beat.is_heartbeat())
            {
                if(m_heart_beat.die())
                {
                    heart_beat_timeout();
                }
            }
        }

        void heartbeat()
        {
            m_message_handler.m_msg.set_id(heart_beat.ID);
            m_message_handler.m_msg.over();
            send(ref m_message_handler.m_msg);
        }

        void heart_beat_timeout()
        {
            m_closed();
            clear();
        }

        private byte[] m_buffer;

        private System.Net.Sockets.Socket m_socket;

        private state_t m_state;

        private stream_t m_i_stream;

        private stream_t m_o_stream;

        private string m_addr;
        private int m_port;

        private int m_size;

        public message_handler_t m_message_handler;

        private ArrayList m_read_list;

        private ArrayList m_write_list;

        private ArrayList m_except_list;

        private client_event_t m_connected;

        private client_event_t m_timeout;

        private client_event_t m_closed;

        private long m_connect_deadline;

        public heart_beat m_heart_beat;
    }

}
