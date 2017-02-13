using UnityEngine;
using System.Collections;

namespace ns_btl_loading
{
    public class handle_message_t
    {
        public static void init()
        {
            message_cb_t.set_enter_battlefield_cb(enter_battlefield);
            message_cb_t.set_btl_start_cb(btl_start);
        }

        static void enter_battlefield(ushort err, proto.vector2_t pos, proto.vector2_t dir)
        {
            if (errno_t.E_OK != err)
            {
                sys_t.g_sys.m_errstr = "匹配失败!";
                return;
            }

            temp_t.convert(ref pos, ref sys_t.g_sys.m_game_data.m_user_data.m_pos);
            temp_t.convert(ref dir, ref sys_t.g_sys.m_game_data.m_user_data.m_dir);

            //sys_t.g_sys.m_log.write("enter battlefield, pos" + sys_t.g_sys.m_game_data.m_user_data.m_pos.ToString() + ", dir" + sys_t.g_sys.m_game_data.m_user_data.m_dir.ToString() + "\n");

            sys_t.g_sys.m_game_data.m_self_start = false;

            UnityEngine.SceneManagement.SceneManager.LoadScene("Resources/btl/btl");
        }

        static void btl_start()
        {
            sys_t.g_sys.m_game_data.m_self_start = true;
        }
    }
}
