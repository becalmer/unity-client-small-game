using UnityEngine;
using System.Collections;

public class btl : MonoBehaviour
{

    int   m_rest_time;
    float m_pass_time;

    public UILabel m_btl_time;

    // Use this for initialization
    void Start()
    {
        m_rest_time = 600;
        m_pass_time = 0;

        GameObject tank = ((Transform)(Instantiate(game.m_game.m_tank_perfab, game.m_game.m_tank_perfab.position, game.m_game.m_tank_perfab.rotation))).gameObject;

        tank.GetComponent<tank>().m_dir = sys_t.g_sys.m_game_data.m_user_data.m_dir;

        tank.GetComponent<tank>().set_name(sys_t.g_sys.m_game_data.m_user_data.m_nick);

        mover.m_mover.init(tank);

        game.m_game.m_tank = tank;

        ns_btl.handle_message_t.init();

        HandleMessage();
    }

    void HandleMessage()
    {
        if(sys_t.g_sys.m_temp.m_enter_sight_roles != null)
        {
            ns_btl.handle_message_t.enter_sight(ref sys_t.g_sys.m_temp.m_enter_sight_roles);
            sys_t.g_sys.m_temp.m_enter_sight_roles = null;
        }

        if (sys_t.g_sys.m_temp.m_leave_sight_roles != null)
        {
            ns_btl.handle_message_t.leave_sight(ref sys_t.g_sys.m_temp.m_leave_sight_roles);
            sys_t.g_sys.m_temp.m_leave_sight_roles = null;
        }

        if (sys_t.g_sys.m_temp.m_role_pos != null)
        {
            ns_btl.handle_message_t.update_role_pos(ref sys_t.g_sys.m_temp.m_role_pos);
            sys_t.g_sys.m_temp.m_role_pos = null;
        }

        if (sys_t.g_sys.m_temp.m_role_dir != null)
        {
            ns_btl.handle_message_t.update_role_dir(ref sys_t.g_sys.m_temp.m_role_dir);
            sys_t.g_sys.m_temp.m_role_dir = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            sys_t.g_sys.tick();
        }
        catch (System.Exception e)
        {
            sys_t.g_sys.m_log.write("[exception Message = " + e.Message + ", Source = " + e.Source + ", StackTrace = " + e.StackTrace + ", ToString = " + e.ToString() + "]\n");
        }

        TryUpdateRestTime();
    }

    void TryUpdateRestTime()
    {
        m_pass_time += Time.deltaTime;
        if(m_pass_time >= 1.0f)
        {
            m_pass_time -= 1.0f;

            --m_rest_time;
            int m = m_rest_time / 60;
            int s = m_rest_time - m * 60;

            string lf = "";
            string se = s.ToString();
            if(se.Length < 2)
            {
                lf = "0";
            }

            string t = "time " + m.ToString() + ":" + lf + se;

            m_btl_time.text = t;
        }
    }
}