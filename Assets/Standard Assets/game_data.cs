using UnityEngine;
using System.Collections;

public class game_data_t {

    public game_data_t()
    {
        m_user_data = new user_data_t();
        m_roles = new System.Collections.Generic.List<role_t>();
        m_self_start = false;
    }

    public bool m_self_start;

    public user_data_t m_user_data;

    public System.Collections.Generic.List<role_t> m_roles;
}
