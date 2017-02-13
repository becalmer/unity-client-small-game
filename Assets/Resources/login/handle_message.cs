using UnityEngine;
using System.Collections;

namespace ns_login
{
    public class handle_message_t
    {
        public static void init()
        {
            message_cb_t.set_get_gateway_address_cb(get_gateway_address);
            message_cb_t.set_gateway_verify_cb(gateway_verify);
            message_cb_t.set_login_cb(login);
            message_cb_t.set_create_account_cb(create_account);
        }

        static void get_gateway_address(ushort err, uint ip, ushort port, uint key)
        {
            if (errno_t.E_OK != err)
            {
                sys_t.g_sys.m_errstr = "连接服务器失败!";
                return;
            }

            sys_t.g_sys.m_temp.m_key = key;

            sys_t.g_sys.m_client.clear();

            sys_t.g_sys.m_service = sys_t.service_t.GATEWAY;

            sys_t.g_sys.m_client.set_addr(framework.utils_t.to_str_ip(ip), port);

            sys_t.g_sys.m_add_progress(false, 10);
        }

        static void gateway_verify(ushort err)
        {
            if (errno_t.E_OK != err)
            {
                sys_t.g_sys.m_errstr = "连接服务器失败!";
                return;
            }

            proto.CLIENT_REQ_GATEWAY_LOGIN_T pkt;

            pkt.m_user = sys_t.g_sys.m_temp.m_user;
            pkt.m_password = sys_t.g_sys.m_temp.m_password;

            sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_LOGIN_T.cmd);

            pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

            sys_t.g_sys.m_client.m_message_handler.m_msg.over();

            sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

            sys_t.g_sys.m_add_progress(false, 10);
        }

        static void login(ushort err)
        {
            if (errno_t.E_OK != err)
            {
                //sys_t.g_sys.m_errstr = "用户名或密码有误!";
                //return false;

                proto.CLIENT_REQ_GATEWAY_CREATE_ACCOUNT_T pkt;

                pkt.m_user = sys_t.g_sys.m_temp.m_user;
                pkt.m_password = sys_t.g_sys.m_temp.m_password;

                sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_CREATE_ACCOUNT_T.cmd);

                pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                sys_t.g_sys.m_client.m_message_handler.m_msg.over();

                sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                return;
            }

            {
                proto.CLIENT_REQ_GATEWAY_QUERY_ROLE_INFO_T pkt;

                sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_QUERY_ROLE_INFO_T.cmd);

                pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                sys_t.g_sys.m_client.m_message_handler.m_msg.over();

                sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Resources/online_loading/online_loading");
        }

        static void create_account(ushort err)
        {
            if (errno_t.E_OK != err)
            {
                sys_t.g_sys.m_errstr = "用户名已存在!";
                return;
            }

            {
                proto.CLIENT_REQ_GATEWAY_QUERY_ROLE_INFO_T pkt;

                sys_t.g_sys.m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_QUERY_ROLE_INFO_T.cmd);

                pkt.write(ref sys_t.g_sys.m_client.m_message_handler.m_msg);

                sys_t.g_sys.m_client.m_message_handler.m_msg.over();

                sys_t.g_sys.m_client.send(ref sys_t.g_sys.m_client.m_message_handler.m_msg);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Resources/online_loading/online_loading");
        }
    }
}
