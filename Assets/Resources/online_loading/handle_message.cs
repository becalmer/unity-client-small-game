using UnityEngine;
using System.Collections;

namespace ns_online_loading
{
    public class handle_message_t
    {
        public static void init()
        {
            message_cb_t.set_query_role_info_cb(query_role_info);
            message_cb_t.set_create_role_cb(create_role);
            message_cb_t.set_online_cb(online);
        }

        static void query_role_info(ushort err, string nick, uint role_id)
        {
            if (errno_t.E_OK != err)
            {
                proto.CLIENT_REQ_GATEWAY_CREATE_ROLE_T pkt;

                pkt.m_nick = sys_t.g_sys.m_temp.m_user;

                sys_t.g_sys.m_game_data.m_user_data.m_nick = sys_t.g_sys.m_temp.m_user;

                sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_CREATE_ROLE_T.cmd);

                pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                sys_t.g_sys.m_client.m_message_handler.m_msg.over();

                sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                return;
            }

            sys_t.g_sys.m_game_data.m_user_data.m_nick = nick;
            sys_t.g_sys.m_game_data.m_user_data.m_role_id = role_id;

            {
                proto.CLIENT_REQ_GATEWAY_ONLINE_T pkt;

                sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_ONLINE_T.cmd);

                pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                sys_t.g_sys.m_client.m_message_handler.m_msg.over();

                sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
            }
        }

        static void create_role(ushort err, uint role_id)
        {
            if (errno_t.E_OK != err)
            {
                sys_t.g_sys.m_errstr = "用户名已存在!";
                return;
            }

            sys_t.g_sys.m_game_data.m_user_data.m_role_id = role_id;

            {
                proto.CLIENT_REQ_GATEWAY_ONLINE_T pkt;

                sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_ONLINE_T.cmd);

                pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                sys_t.g_sys.m_client.m_message_handler.m_msg.over();

                sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
            }
        }

        static void online(ushort err)
        {
            if (errno_t.E_OK != err)
            {
                sys_t.g_sys.m_errstr = "系统异常!";
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Resources/home/home");
        }
    }
}
