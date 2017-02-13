using UnityEngine;
using System.Collections;

public class login : MonoBehaviour
{
    private string m_username = "username";
    private string m_password = "password";

    private float m_login_time;

    int m_trigger;

    public UIButton m_login_btn;

    public UIButton m_cancel_btn;

    // Use this for initialization
    void Start()
    {
        if(null != m_login_btn)
        {
            UIEventListener.Get(m_login_btn.gameObject).onClick = LoginClick;
        }

        if(null != m_cancel_btn)
        {
            UIEventListener.Get(m_cancel_btn.gameObject).onClick = CancelClick;
        }

        ns_login.handle_message_t.init();

        sys_t.ins();

        m_trigger = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        sys_t.g_sys.tick();
    }

    void Error(string errstr)
    {
        Transform child = transform.FindChild("controls");
        if(null == child)
        {
            return;
        }

        child = child.FindChild("progress_bar");
        if(null == child)
        {
            return;
        }

        child = child.FindChild("Label");
        if(null == child)
        {
            return;
        }

        child.GetComponent<UILabel>().text = errstr;
    }

    bool Check()
    {
        if(m_username.Length < 6)
        {
            Error("用户名少于6个字符");
            return false;
        }

        if (m_username.Length > 8)
        {
            Error("用户名多于8个字符");
            return false;
        }

        if (m_password.Length < 6)
        {
            Error("密码少于6个字符");
            return false;
        }

        if (m_password.Length > 8)
        {
            Error("密码多于8个字符");
            return false;
        }

        return true;
    }

    bool ReadUsername()
    {
        Transform child = transform.FindChild("controls");
        if(null == child)
        {
            return false;
        }

        child = child.transform.FindChild("username");
        if (null == child)
        {
            return false;
        }

        child = child.transform.FindChild("username_input");
        if(null == child)
        {
            return false;
        }

        child = child.transform.FindChild("Label");
        if (null == child)
        {
            return false;
        }

        m_username = child.GetComponent<UILabel>().text;

        return true;
    }

    bool ReadPassword()
    {
        Transform child = transform.FindChild("controls");
        if (null == child)
        {
            return false;
        }

        child = child.transform.FindChild("password");
        if (null == child)
        {
            return false;
        }

        child = child.transform.FindChild("password_input");
        if (null == child)
        {
            return false;
        }

        child = child.transform.FindChild("Label");
        if (null == child)
        {
            return false;
        }

        m_password = child.GetComponent<UILabel>().text;

        return true;
    }

    public void LoginClick(GameObject obj)
    {
        if(!ReadUsername())
        {
            return;
        }

        if(!ReadPassword())
        {
            return;
        }

        if(!Check())
        {
            return;
        }

        ++ m_trigger;

        m_login_time = Time.time + 5;

        sys_t.g_sys.m_temp.m_user = m_username;
        sys_t.g_sys.m_temp.m_password = m_password;

        sys_t.g_sys.m_service = sys_t.service_t.GATEWAY_ENTRY;

        sys_t.g_sys.m_client.set_addr("192.168.1.102", 18000);

        EnableLogin(false);

        sys_t.g_sys.m_add_progress = AddProgress;
    }

    public void CancelClick(GameObject obj)
    {
        Application.Quit();

        EnableCancel(false);
    }

    void EnableLogin(bool enable)
    {
        m_login_btn.GetComponent<BoxCollider>().enabled = enable;
    }

    void EnableCancel(bool enable)
    {
        m_cancel_btn.GetComponent<BoxCollider>().enabled = enable;
    }

    void UpdateProgress()
    {
        if (m_trigger == 0 && m_trigger > 1)
        {
            return;
        }

        AddProgress(true, 1);
    }

    void TryCheckTimeout()
    {
        if(m_trigger != 1)
        {
            return;
        }

        if (Time.time >= m_login_time)
        {
            Error("登录超时");
            sys_t.g_sys.m_client.clear();
            EnableLogin(true);

            ++ m_trigger;
        }
    }

    void OnGUI()
    {
        UpdateProgress();
        TryCheckTimeout();
    }

    void AddProgress(bool auto, int percent)
    {
        Transform child = transform.FindChild("controls");
        if(null == child)
        {
            return;
        }

        child = child.FindChild("progress_bar");
        if(null == child)
        {
            return;
        }

        float value = child.GetComponent<UISlider>().value;
        float nvalue = value;
        if(auto)
        {
            if(nvalue >= 0.7f)
            {
                return;
            }
            nvalue += 0.01f;
        }
        else
        {
            if(value >= 1.0f)
            {
                return;
            }
            nvalue += percent * 1.0f / 100;
        }

        value = Mathf.Clamp(nvalue, value, 1.0f);

        child.GetComponent<UISlider>().value = value;
    }
}
