using UnityEngine;
using System.Collections;

public class message_cb_t
{
    public delegate void message_cb_get_gateway_address(ushort err, uint ip, ushort port, uint key);
    public delegate void message_cb_gateway_verify(ushort err);
    public delegate void message_cb_login(ushort err);
    public delegate void message_cb_create_account(ushort err);
    public delegate void message_cb_query_role_info(ushort err, string nick, uint role_id);
    public delegate void message_cb_create_role(ushort err, uint role_id);
    public delegate void message_cb_online(ushort err);
    public delegate void message_cb_match(ushort err);
    public delegate void message_cb_enter_battlefield(ushort err, proto.vector2_t pos, proto.vector2_t dir);
    public delegate void message_cb_enter_sight(ref System.Collections.Generic.List<proto.role_data_t> list);
    public delegate void message_cb_leave_sight(ref System.Collections.Generic.List<uint> list);
    public delegate void message_cb_update_role_pos(ref System.Collections.Generic.List<proto.role_pos_t> list);
    public delegate void message_cb_update_role_dir(ref System.Collections.Generic.List<proto.role_dir_t> list);
    public delegate void message_cb_btl_start();
    public delegate void message_cb_btl_over();
    public delegate void message_cb_pause(uint role_id);
    public delegate void message_cb_resume(uint role_id, proto.vector2_t dir);
    public delegate void message_cb_shot(uint atk_role_id, uint target_role_id);
    public delegate void message_cb_damage(uint atk_role_id, uint target_role_id, uint damage);
    public delegate void message_cb_enter_btl(uint role_id, ref proto.vector2_t pos);
    public delegate void message_cb_leave_btl(uint role_id, ref proto.vector2_t pos);
    public delegate void message_cb_rank(byte nth, ref System.Collections.Generic.List<proto.rank_role_data_t> list);
    public delegate void message_cb_rank_changed(ref System.Collections.Generic.List<proto.rank_changed_info_t> list);
    public delegate void message_cb_revive_coord(uint role_id, ref proto.vector2_t pos);
    public delegate void message_cb_revive(uint role_id);
    public delegate void message_cb_resume_complete();

    public static void set_get_gateway_address_cb(message_cb_get_gateway_address cb)
    {
        m_message_cb_get_gateway_address = cb;
    }

    public static void set_gateway_verify_cb(message_cb_gateway_verify cb)
    {
        m_message_cb_gateway_verify = cb;
    }

    public static void set_login_cb(message_cb_login cb)
    {
        m_message_cb_login = cb;
    }

    public static void set_create_account_cb(message_cb_create_account cb)
    {
        m_message_cb_create_account = cb;
    }

    public static void set_query_role_info_cb(message_cb_query_role_info cb)
    {
        m_message_cb_query_role_info = cb;
    }

    public static void set_create_role_cb(message_cb_create_role cb)
    {
        m_message_cb_create_role = cb;
    }

    public static void set_online_cb(message_cb_online cb)
    {
        m_message_cb_online = cb;
    }

    public static void set_match_cb(message_cb_match cb)
    {
        m_message_cb_match = cb;
    }

    public static void set_enter_battlefield_cb(message_cb_enter_battlefield cb)
    {
        m_message_cb_enter_battlefield = cb;
    }

    public static void set_enter_sight_cb(message_cb_enter_sight cb)
    {
        m_message_cb_enter_sight = cb;
    }

    public static void set_leave_sight_cb(message_cb_leave_sight cb)
    {
        m_message_cb_leave_sight = cb;
    }

    public static void set_update_role_pos_cb(message_cb_update_role_pos cb)
    {
        m_message_cb_update_role_pos = cb;
    }

    public static void set_update_role_dir_cb(message_cb_update_role_dir cb)
    {
        m_message_cb_update_role_dir = cb;
    }

    public static void set_btl_over_cb(message_cb_btl_over cb)
    {
        m_message_cb_btl_over = cb;
    }

    public static void set_btl_start_cb(message_cb_btl_start cb)
    {
        m_message_cb_btl_start = cb;
    }

    public static void set_pause_cb(message_cb_pause cb)
    {
        m_message_cb_pause = cb;
    }

    public static void set_resume_cb(message_cb_resume cb)
    {
        m_message_cb_resume = cb;
    }

    public static void set_shot_cb(message_cb_shot cb)
    {
        m_message_cb_shot = cb;
    }

    public static void set_damage_cb(message_cb_damage cb)
    {
        m_message_cb_damage = cb;
    }

    public static void set_enter_btl_cb(message_cb_enter_btl cb)
    {
        m_message_cb_enter_btl = cb;
    }

    public static void set_leave_btl_cb(message_cb_leave_btl cb)
    {
        m_message_cb_leave_btl = cb;
    }

    public static void set_rank_cb(message_cb_rank cb)
    {
        m_message_cb_rank = cb;
    }

    public static void set_rank_changed_cb(message_cb_rank_changed cb)
    {
        m_message_cb_rank_changed = cb;
    }

    public static void set_revive_coord_cb(message_cb_revive_coord cb)
    {
        m_message_cb_revive_coord = cb;
    }

    public static void set_revive_cb(message_cb_revive cb)
    {
        m_message_cb_revive = cb;
    }

    public static void set_resume_complete_cb(message_cb_resume_complete cb)
    {
        m_message_cb_resume_complete = cb;
    }

    public static message_cb_get_gateway_address m_message_cb_get_gateway_address;
    public static message_cb_gateway_verify m_message_cb_gateway_verify;
    public static message_cb_login m_message_cb_login;
    public static message_cb_create_account m_message_cb_create_account;
    public static message_cb_query_role_info m_message_cb_query_role_info;
    public static message_cb_create_role m_message_cb_create_role;
    public static message_cb_online m_message_cb_online;
    public static message_cb_match m_message_cb_match;
    public static message_cb_enter_battlefield m_message_cb_enter_battlefield;
    public static message_cb_enter_sight m_message_cb_enter_sight;
    public static message_cb_leave_sight m_message_cb_leave_sight;
    public static message_cb_update_role_pos m_message_cb_update_role_pos;
    public static message_cb_update_role_dir m_message_cb_update_role_dir;
    public static message_cb_btl_start m_message_cb_btl_start;    
    public static message_cb_btl_over m_message_cb_btl_over;
    public static message_cb_pause m_message_cb_pause;
    public static message_cb_resume m_message_cb_resume;
    public static message_cb_shot m_message_cb_shot;
    public static message_cb_damage m_message_cb_damage;
    public static message_cb_enter_btl m_message_cb_enter_btl;
    public static message_cb_leave_btl m_message_cb_leave_btl;
    public static message_cb_rank m_message_cb_rank;
    public static message_cb_rank_changed m_message_cb_rank_changed;
    public static message_cb_revive_coord m_message_cb_revive_coord;
    public static message_cb_revive m_message_cb_revive;
    public static message_cb_resume_complete m_message_cb_resume_complete;

    public message_cb_t()
    {
        m_message_cb_get_gateway_address = null;
        m_message_cb_gateway_verify = null;
        m_message_cb_login = null;
        m_message_cb_create_account = null;
        m_message_cb_query_role_info = null;
        m_message_cb_create_role = null;
        m_message_cb_online = null;
        m_message_cb_match = null;
        m_message_cb_enter_battlefield = null;
        m_message_cb_enter_sight = null;
        m_message_cb_leave_sight = null;
        m_message_cb_update_role_pos = null;
        m_message_cb_update_role_dir = null;
        m_message_cb_btl_start = null;
        m_message_cb_btl_over = null;
        m_message_cb_pause = null;
        m_message_cb_resume = null;
        m_message_cb_shot = null;
        m_message_cb_damage = null;
        m_message_cb_enter_btl = null;
        m_message_cb_leave_btl = null;
        m_message_cb_rank = null;
        m_message_cb_rank_changed = null;
        m_message_cb_revive_coord = null;
        m_message_cb_revive = null;
        m_message_cb_resume_complete = null;
    }
}
