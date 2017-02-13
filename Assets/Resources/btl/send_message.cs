using UnityEngine;
using System.Collections;

namespace ns_btl
{
    public class send_message_t
    {
        public static void pause()
        {
            proto.CLIENT_NTF_BTL_PAUSE_T pkt;

            sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_NTF_BTL_PAUSE_T.cmd);

            pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

            sys_t.g_sys.m_client.m_message_handler.m_msg.over();

            sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
        }

        public static void resume(ref Vector3 dir)
        {
            proto.CLIENT_NTF_BTL_RESUME_T pkt;

            pkt.dir.x = 0;
            pkt.dir.z = 0;

            temp_t.convert(ref dir, ref pkt.dir);

            sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_NTF_BTL_RESUME_T.cmd);

            pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

            sys_t.g_sys.m_client.m_message_handler.m_msg.over();

            sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
        }

        public static void rank()
        {
            proto.CLIENT_REQ_BTL_RANK_T pkt;

            sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_BTL_RANK_T.cmd);

            pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

            sys_t.g_sys.m_client.m_message_handler.m_msg.over();

            sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
        }
    }
}

