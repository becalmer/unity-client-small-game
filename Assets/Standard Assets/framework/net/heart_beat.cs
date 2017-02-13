using UnityEngine;
using System.Collections;

public struct heart_beat {

    public static ushort ID = 65500;

    enum state_t
    {
        INTERACTIVE,
        HEART_BEAT,
    };
	
	public void interactive()
    {
        m_mode = (long)state_t.INTERACTIVE;
        m_future_time = System.DateTime.UtcNow.Ticks + max_interactive_interval * 10000000;
    }

    public void heartbeat()
    {
        m_mode = (long)state_t.HEART_BEAT;
        m_future_time = System.DateTime.UtcNow.Ticks + max_heart_beat_interval * 10000000;
    }

    public bool die()
    {
        return (System.DateTime.UtcNow.Ticks >= m_future_time);
    }

    public bool is_interactive()
    {
        return ((long)state_t.INTERACTIVE == m_mode);
    }

    public bool is_heartbeat()
    {
        return ((long)state_t.HEART_BEAT == m_mode);
    }

    long m_mode;
    long m_future_time;

    static int max_interactive_interval = 15;
    static int max_heart_beat_interval = 15;
}
