using UnityEngine;
using System.Collections;

public class game : MonoBehaviour {

    public Transform m_bullet_perfab = null;

    public Transform m_tank_perfab = null;

    public Transform m_explosion_perfab = null;

    public Transform m_smoke_perfab = null;

    //public Transform m_track_perfab = null;

    //本机游戏主角
    public GameObject m_tank = null;

    public static game m_game = null;

    public game()
    {
        m_game = this;
    }

    // Use this for initialization
    void Start () {

        m_tank = null;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
