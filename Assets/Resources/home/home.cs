using UnityEngine;
using System.Collections;

public class home : MonoBehaviour
{
    public UIButton m_enter;

    void Start ()
    {
        if (null != m_enter)
        {
            UIEventListener.Get(m_enter.gameObject).onClick = Click;
        }

        ns_home.handle_message_t.init();
    }

    void OnTrigger()
    {
        ns_home.send_message_t.match();
    }
	
	// Update is called once per frame
	void Update ()
    {
        try
        {
            sys_t.g_sys.tick();
        }
        catch (System.Exception e)
        {
            sys_t.g_sys.m_log.write("[exception Message = " + e.Message + ", Source = " + e.Source + ", StackTrace = " + e.StackTrace + ", ToString = " + e.ToString() + "]\n");
        }
    }

    void OnGUI()
    {
       
    }

    public void Click(GameObject obj)
    {
        OnTrigger();

        Enable(false);
    }

    void Enable(bool enable)
    {
        m_enter.GetComponent<BoxCollider>().enabled = enable;
    }
}
