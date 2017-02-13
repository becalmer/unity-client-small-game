using UnityEngine;
using System.Collections;

namespace ns_home
{
    public class send_message_t
    {
        public static void match()
        {
            proto.CLIENT_REQ_OBJECT_MATCH_T pkt;

            sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_OBJECT_MATCH_T.cmd);

            pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

            sys_t.g_sys.m_client.m_message_handler.m_msg.over();

            sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
        }
    }
}

