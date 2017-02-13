using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {

    public static mover m_mover;

    public const float SPEED_RATE = 4.0f;
     
    private Vector3 m_light_diff;
    private Vector3 m_camera_diff;
    public Vector3 m_circle_diff;

    public GameObject m_circle;
    public GameObject m_dir;

    private float m_total;
    private int m_frame;
    private int m_fps;

    public mover()
    {
        m_mover = this;
    }

    // Use this for initialization
    void Start()
    {
        m_total = 0.0f;
        m_frame = 0;
        m_fps = 0;

        m_circle.GetComponent<Renderer>().sortingOrder = 5;

        m_dir.GetComponent<Renderer>().sortingOrder = 10;

        m_circle.SetActive(false);
        m_dir.SetActive(false);
    }

    public void init(GameObject tank)
    {
        Vector3 pos = Camera.main.transform.position;
        pos.y -= 10.0f;

        tank.transform.position = pos;

        m_light_diff = GameObject.Find("light").transform.position - Camera.main.transform.position;
        m_camera_diff = Camera.main.transform.position - tank.transform.position;
    }

    void FixedUpdate()
    {
        if(game.m_game.m_tank == null)
        {
            return;
        }

        if (game.m_game.m_tank.GetComponent<tank>().m_pause)
        {
            return;
        }

        if (game.m_game.m_tank.GetComponent<tank>().m_moved)
        {
            Camera.main.transform.position = game.m_game.m_tank.transform.position + m_camera_diff;

            GameObject.Find("light").transform.position = Camera.main.transform.position + m_light_diff;

            m_circle.transform.position = game.m_game.m_tank.transform.position + m_circle_diff;

            m_dir.transform.position = m_circle.transform.position + game.m_game.m_tank.GetComponent<tank>().m_dir;
        }
    }

    void OnGUI()
    {
        return;

        ++m_frame;
        m_total += Time.deltaTime;

        if (m_total >= 1.0f)
        {
            m_fps = m_frame;

            m_total = 0.0f;
            m_frame = 0;
        }

        if(null == game.m_game.m_tank)
        {
            return;
        }

        string str = "m_fps = " + m_fps.ToString() + ", pos(" + game.m_game.m_tank.transform.position.x.ToString() + ", " + game.m_game.m_tank.transform.position.z.ToString() + "), " + "pause(" + game.m_game.m_tank.GetComponent<tank>().m_pause.ToString() + ")";
        int size = GUI.skin.label.fontSize;
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(10, 400, 1200, 400), str);
        GUI.skin.label.fontSize = size;
    }
}
