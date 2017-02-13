using UnityEngine;
using System.Collections;

namespace ns_home
{
    public class handle_message_t
    {
        public static void init()
        {
            message_cb_t.set_match_cb(match);
        }

        static void match(ushort err)
        {
            if (errno_t.E_OK != err)
            {
                sys_t.g_sys.m_errstr = "匹配失败!";
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Resources/btl_loading/btl_loading");
        }
    }
}
