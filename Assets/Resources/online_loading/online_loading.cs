﻿using UnityEngine;
using System.Collections;

public class online_loading : MonoBehaviour {

    float m_loading_time;

	// Use this for initialization
	void Start ()
    {
        ns_online_loading.handle_message_t.init();

        m_loading_time = Time.time + 5;

        sys_t.g_sys.m_add_progress = AddProgress;
    }
	
	// Update is called once per frame
	void Update ()
    {
        sys_t.g_sys.tick();
    }
    
    void Error(string errstr)
    {
        Transform child = transform.FindChild("controls");
        if (null == child)
        {
            return;
        }

        child = child.FindChild("progress_bar");
        if (null == child)
        {
            return;
        }

        child = child.FindChild("Label");
        if (null == child)
        {
            return;
        }

        child.GetComponent<UILabel>().text = errstr;
    }

    void OnGUI()
    {
        AddProgress(true, 1);

        if(Time.time >= m_loading_time)
        {
            Error("上线超时");
        }
    }

    void AddProgress(bool auto, int percent)
    {
        Transform child = transform.FindChild("controls");
        if (null == child)
        {
            return;
        }

        child = child.FindChild("progress_bar");
        if (null == child)
        {
            return;
        }

        float value = child.GetComponent<UISlider>().value;
        float nvalue = value;
        if (auto)
        {
            if (nvalue >= 0.7f)
            {
                return;
            }
            nvalue += 0.01f;
        }
        else
        {
            if (value >= 1.0f)
            {
                return;
            }
            nvalue += percent * 1.0f / 100;
        }

        value = Mathf.Clamp(nvalue, value, 1.0f);

        child.GetComponent<UISlider>().value = value;
    }
}
