using UnityEngine;
using System.Collections;

public class proto_handler {

	public proto_handler()
	{
	}

	~proto_handler()
	{
	}

	public void init()
	{
		sys_t.g_sys.m_client.m_message_handler.mount (proto.GATEWAY_ENTRY_ACK_CLIENT_GATEWAY_ADDRESS_T.cmd, proto_handler.on_message_GATEWAY_ENTRY_ACK_CLIENT_GATEWAY_ADDRESS_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.GATEWAY_ACK_CLIENT_VERIFY_T.cmd, proto_handler.on_message_GATEWAY_ACK_CLIENT_VERIFY_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.GATEWAY_ACK_CLIENT_LOGIN_T.cmd, proto_handler.on_message_GATEWAY_ACK_CLIENT_LOGIN_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.GATEWAY_ACK_CLIENT_CREATE_ACCOUNT_T.cmd, proto_handler.on_message_GATEWAY_ACK_CLIENT_CREATE_ACCOUNT_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.GATEWAY_ACK_CLIENT_QUERY_ROLE_INFO_T.cmd, proto_handler.on_message_GATEWAY_ACK_CLIENT_QUERY_ROLE_INFO_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.GATEWAY_ACK_CLIENT_CREATE_ROLE_T.cmd, proto_handler.on_message_GATEWAY_ACK_CLIENT_CREATE_ROLE_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.GATEWAY_ACK_CLIENT_ONLINE_T.cmd, proto_handler.on_message_GATEWAY_ACK_CLIENT_ONLINE_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.OBJECT_ACK_CLIENT_MATCH_T.cmd, proto_handler.on_message_OBJECT_ACK_CLIENT_MATCH_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.OBJECT_NTF_CLIENT_ENTER_BATTLEFIELD_T.cmd, proto_handler.on_message_OBJECT_NTF_CLIENT_ENTER_BATTLEFIELD_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_ENTER_SIGHT_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_ENTER_SIGHT_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_LEAVE_SIGHT_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_LEAVE_SIGHT_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_ROLE_POS_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_ROLE_POS_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_ROLE_DIR_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_ROLE_DIR_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_BTL_START_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_BTL_START_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.OBJECT_NTF_CLIENT_BTL_OVER_T.cmd, proto_handler.on_message_OBJECT_NTF_CLIENT_BTL_OVER_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_PAUSE_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_PAUSE_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_RESUME_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_RESUME_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_SHOT_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_SHOT_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_DAMAGE_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_DAMAGE_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_ENTER_BTL_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_ENTER_BTL_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_LEAVE_BTL_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_LEAVE_BTL_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_ACK_CLIENT_RANK_T.cmd, proto_handler.on_message_BTL_ACK_CLIENT_RANK_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_RANK_CHANGED_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_RANK_CHANGED_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_REVIVE_COORD_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_REVIVE_COORD_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_REVIVE_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_REVIVE_handler);
        sys_t.g_sys.m_client.m_message_handler.mount(proto.BTL_NTF_CLIENT_RESUME_COMPLETE_T.cmd, proto_handler.on_message_BTL_NTF_CLIENT_RESUME_COMPLETE_handler);
    }

	static bool on_message_GATEWAY_ENTRY_ACK_CLIENT_GATEWAY_ADDRESS_handler(ref framework.message_t msg)
	{
        proto.GATEWAY_ENTRY_ACK_CLIENT_GATEWAY_ADDRESS_T pkt;

        pkt.m_errno = errno_t.E_OK;
        pkt.m_key = 0;
        pkt.m_addr.m_ip = 0;
        pkt.m_addr.m_port = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_get_gateway_address(pkt.m_errno, pkt.m_addr.m_ip, pkt.m_addr.m_port, pkt.m_key);

		return true;
	}

    static bool on_message_GATEWAY_ACK_CLIENT_VERIFY_handler(ref framework.message_t msg)
    {
        proto.GATEWAY_ACK_CLIENT_VERIFY_T pkt;
        pkt.m_errno = errno_t.E_OK;        

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_gateway_verify(pkt.m_errno);

        return true;
    }

    static bool on_message_GATEWAY_ACK_CLIENT_LOGIN_handler(ref framework.message_t msg)
    {
        proto.GATEWAY_ACK_CLIENT_LOGIN_T pkt;

        pkt.m_errno = errno_t.E_OK;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_login(pkt.m_errno);

        return true;
    }

    static bool on_message_GATEWAY_ACK_CLIENT_CREATE_ACCOUNT_handler(ref framework.message_t msg)
    {
        proto.GATEWAY_ACK_CLIENT_CREATE_ACCOUNT_T pkt;

        pkt.m_errno = errno_t.E_OK;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_create_account(pkt.m_errno);

        return true;
    }

    static bool on_message_GATEWAY_ACK_CLIENT_QUERY_ROLE_INFO_handler(ref framework.message_t msg)
    {
        proto.GATEWAY_ACK_CLIENT_QUERY_ROLE_INFO_T pkt;

        pkt.m_errno = errno_t.E_OK;
        pkt.m_info.m_nick = "";
        pkt.m_info.m_role_id = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_query_role_info(pkt.m_errno, pkt.m_info.m_nick, pkt.m_info.m_role_id);

        return true;
    }

    static bool on_message_GATEWAY_ACK_CLIENT_CREATE_ROLE_handler(ref framework.message_t msg)
    {
        proto.GATEWAY_ACK_CLIENT_CREATE_ROLE_T pkt;

        pkt.m_errno = errno_t.E_OK;
        pkt.m_role_id = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_create_role(pkt.m_errno, pkt.m_role_id);

        return true;
    }

    static bool on_message_GATEWAY_ACK_CLIENT_ONLINE_handler(ref framework.message_t msg)
    {
        proto.GATEWAY_ACK_CLIENT_ONLINE_T pkt;

        pkt.m_errno = errno_t.E_OK;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_online(pkt.m_errno);

        return true;
    }

    static bool on_message_OBJECT_ACK_CLIENT_MATCH_handler(ref framework.message_t msg)
    {
        proto.OBJECT_ACK_CLIENT_MATCH_T pkt;

        pkt.m_errno = errno_t.E_OK;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_match(pkt.m_errno);

        return true;
    }

    static bool on_message_OBJECT_NTF_CLIENT_ENTER_BATTLEFIELD_handler(ref framework.message_t msg)
    {
        proto.OBJECT_NTF_CLIENT_ENTER_BATTLEFIELD_T pkt;

        pkt.m_errno = errno_t.E_OK;
        pkt.m_pos.x = 0;
        pkt.m_pos.z = 0;
        pkt.m_dir.x = 0;
        pkt.m_dir.z = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_enter_battlefield(pkt.m_errno, pkt.m_pos, pkt.m_dir);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_ENTER_SIGHT_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_ENTER_SIGHT_T pkt;

        pkt.m_list = new System.Collections.Generic.List<proto.role_data_t>();

        if (!pkt.read(ref msg))
        {
            return false;
        }

        if(message_cb_t.m_message_cb_enter_sight == null)
        {
            sys_t.g_sys.m_temp.set_enter_sight_roles(ref pkt.m_list);

            return true;
        }

        message_cb_t.m_message_cb_enter_sight(ref pkt.m_list);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_LEAVE_SIGHT_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_LEAVE_SIGHT_T pkt;

        pkt.m_list = new System.Collections.Generic.List<uint>();

        if (!pkt.read(ref msg))
        {
            return false;
        }

        if (message_cb_t.m_message_cb_leave_sight == null)
        {
            sys_t.g_sys.m_temp.set_leave_sight_roles(ref pkt.m_list);

            return true;
        }

        message_cb_t.m_message_cb_leave_sight(ref pkt.m_list);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_ROLE_POS_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_ROLE_POS_T pkt;

        pkt.m_list = new System.Collections.Generic.List<proto.role_pos_t>();

        if (!pkt.read(ref msg))
        {
            return false;
        }

        if(message_cb_t.m_message_cb_update_role_pos == null)
        {
            sys_t.g_sys.m_temp.set_role_pos(ref pkt.m_list);

            return true;
        }

        message_cb_t.m_message_cb_update_role_pos(ref pkt.m_list);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_ROLE_DIR_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_ROLE_DIR_T pkt;

        pkt.m_list = new System.Collections.Generic.List<proto.role_dir_t>();

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_update_role_dir(ref pkt.m_list);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_BTL_START_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_BTL_START_T pkt;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_btl_start();

        return true;
    }

    static bool on_message_OBJECT_NTF_CLIENT_BTL_OVER_handler(ref framework.message_t msg)
    {
        proto.OBJECT_NTF_CLIENT_BTL_OVER_T pkt;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_btl_over();

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_PAUSE_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_PAUSE_T pkt;

        pkt.role_id = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_pause(pkt.role_id);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_RESUME_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_RESUME_T pkt;

        pkt.role_id = 0;
        pkt.dir.x = 0;
        pkt.dir.z = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_resume(pkt.role_id, pkt.dir);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_SHOT_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_SHOT_T pkt;

        pkt.atk_role_id = 0;
        pkt.target_role_id = 0;

        sys_t.g_sys.m_log.write("[BTL_NTF_CLIENT_SHOT]\n");

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_shot(pkt.atk_role_id, pkt.target_role_id);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_DAMAGE_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_DAMAGE_T pkt;

        pkt.atk_role_id = 0;
        pkt.target_role_id = 0;
        pkt.damage = 0;

        sys_t.g_sys.m_log.write("[BTL_NTF_CLIENT_DAMAGE]\n");

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_damage(pkt.atk_role_id, pkt.target_role_id, pkt.damage);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_ENTER_BTL_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_ENTER_BTL_T pkt;

        pkt.role_id = 0;
        pkt.m_pos.x = 0;
        pkt.m_pos.z = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_enter_btl(pkt.role_id, ref pkt.m_pos);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_LEAVE_BTL_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_LEAVE_BTL_T pkt;

        pkt.role_id = 0;
        pkt.m_pos.x = 0;
        pkt.m_pos.z = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_leave_btl(pkt.role_id, ref pkt.m_pos);

        return true;
    }

    static bool on_message_BTL_ACK_CLIENT_RANK_handler(ref framework.message_t msg)
    {
        proto.BTL_ACK_CLIENT_RANK_T pkt;

        pkt.m_nth = 0;
        pkt.m_list = new System.Collections.Generic.List<proto.rank_role_data_t>();

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_rank(pkt.m_nth, ref pkt.m_list);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_RANK_CHANGED_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_RANK_CHANGED_T pkt;
        
        pkt.m_list = new System.Collections.Generic.List<proto.rank_changed_info_t>();

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_rank_changed(ref pkt.m_list);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_REVIVE_COORD_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_REVIVE_COORD_T pkt;

        pkt.role_id = 0;
        pkt.m_pos.x = 0;
        pkt.m_pos.z = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_revive_coord(pkt.role_id, ref pkt.m_pos);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_REVIVE_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_REVIVE_T pkt;

        pkt.role_id = 0;

        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_revive(pkt.role_id);

        return true;
    }

    static bool on_message_BTL_NTF_CLIENT_RESUME_COMPLETE_handler(ref framework.message_t msg)
    {
        proto.BTL_NTF_CLIENT_RESUME_COMPLETE_T pkt;
        
        if (!pkt.read(ref msg))
        {
            return false;
        }

        message_cb_t.m_message_cb_resume_complete();

        return true;
    }
}
