using UnityEngine;
using System.Collections;

public delegate void AddProgress_t(bool auto, int percent);

public class sys_t {

    public static sys_t g_sys = null;

    public enum service_t
    {
        NONE,
        GATEWAY_ENTRY,
        GATEWAY,
    }

    public sys_t()
    {
        try
        {
            m_client = new framework.client_t();

			m_proto_handler = new proto_handler();

            m_temp = new temp_t();

            m_log = new framework.log_t();

            m_game_data = new game_data_t();

            m_client.set_event(on_client_connected, on_client_timeout, on_client_closed);
        }
        catch(System.Exception)
        {

        }

        m_service = service_t.NONE;

        m_errstr = "";
    }

    ~sys_t()
    {
		m_client = null;
		m_proto_handler = null;
    }

    public static sys_t ins()
    {
        if (null == g_sys)
        {
            try
            {
                g_sys = new sys_t();
            }
            catch (System.Exception)
            {

            }

			g_sys.m_proto_handler.init ();

            g_sys.m_log.set_filename("tank");

            g_sys.m_log.write("sys ins func ...\n");

            Application.targetFrameRate = -1;
        }

        return g_sys;
    }

    void req_gateway_address()
    {
        proto.CLIENT_REQ_GATEWAY_ENTRY_GATEWAY_ADDRESS_T pkt;

        m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_ENTRY_GATEWAY_ADDRESS_T.cmd);

        pkt.write(ref m_client.m_message_handler.m_msg);

        m_client.m_message_handler.m_msg.over();

        m_client.send(ref m_client.m_message_handler.m_msg);

        m_add_progress(false, 10);
    }

    void req_gateway_verify()
    {
        proto.CLIENT_REQ_GATEWAY_VERIFY_T pkt;

        pkt.m_key = m_temp.m_key;

        m_client.m_message_handler.m_msg.set_id(proto.CLIENT_REQ_GATEWAY_VERIFY_T.cmd);

        pkt.write(ref m_client.m_message_handler.m_msg);

        m_client.m_message_handler.m_msg.over();

        m_client.send(ref m_client.m_message_handler.m_msg);

        m_add_progress(false, 10);
    }

    public void try_exit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(GUI.Button(new Rect(Screen.width * 3 / 8, Screen.height * 3 / 8, Screen.width / 4, Screen.height / 4), "确定退出"))
            {
                Application.Quit();
            }            
        }
    }

    public static void on_client_connected()
    {
        switch(g_sys.m_service)
        {
            case service_t.NONE:
            {
                break;
            }
            case service_t.GATEWAY_ENTRY:
            {
                g_sys.req_gateway_address();
                break;
            }
            case service_t.GATEWAY:
            {
                g_sys.req_gateway_verify();
                break;
            }
        }
    }

    public static void on_client_timeout()
    {

    }

    public static void on_client_closed()
    {

    }

    public void tick()
    {
        m_client.tick();
        m_log.tick();
    }

    public framework.client_t m_client;

    public service_t m_service;

	public proto_handler m_proto_handler;

    public string m_errstr;

    public temp_t m_temp;

    public game_data_t m_game_data;

    public framework.log_t m_log;

    public AddProgress_t m_add_progress;
}