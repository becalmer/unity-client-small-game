using UnityEngine;
using System.Collections;

public class touch : MonoBehaviour
{

    public static touch m_touch;

    public bool m_begined;
    public bool m_moved;
    
    private RaycastHit m_hit;

    private const float DISTANCE = 1.0f;

    private const float PIX = (float)(3.14159 / 180);

    public string m_str;
        
    Vector2 m_touch_coord;

    Vector2 m_screen_center;

    public UIButton m_skill_1;
    public UIButton m_skill_2;
    public UIButton m_skill_3;

    public touch()
    {
        m_touch = this;
    }

    // Use this for initialization
    void Start()
    {
        m_begined = false;
        m_moved = false;

        m_str = "";

        m_screen_center.x = Screen.width / 2;
        m_screen_center.y = Screen.height / 2;
    }
    
    void calc_direction(Vector3 move_position, ref Vector3 dir)
    {
        dir.y = 0.0f;

        if (Mathf.Approximately(mover.m_mover.m_circle.transform.position.x, move_position.x))
        {
            if (move_position.z > mover.m_mover.m_circle.transform.position.z)
            {
                dir.z = 1.0f;
            }
            else
            {
                dir.z = 1.0f;
            }
            dir.x = 0.0f;
        }
        else if (Mathf.Approximately(mover.m_mover.m_circle.transform.position.z, move_position.z))
        {
            if (move_position.x > mover.m_mover.m_circle.transform.position.x)
            {
                dir.x = 1.0f;
            }
            else
            {
                dir.x = 1.0f;
            }
            dir.z = 0.0f;
        }
        else
        {
            float k = (move_position.z - mover.m_mover.m_circle.transform.position.z) / (move_position.x - mover.m_mover.m_circle.transform.position.x);
            float q = Mathf.Sqrt((k * k) + 1);

            if (move_position.z > mover.m_mover.m_circle.transform.position.z)
            {
                dir.z = Mathf.Abs(k / q);
            }
            else
            {
                dir.z = Mathf.Abs(k / q) * -1.0f;
            }

            if (move_position.x > mover.m_mover.m_circle.transform.position.x)
            {
                dir.x = 1 / q;
            }
            else
            {
                dir.x = -1 / q;
            }
        }
    }
    
    public static bool equal(float x, float y)
    {
        float d = x - y;

        if((d > -0.0001) && (d < 0.0001))
        {
            return true;
        }

        return false;
    }

    float CalcDegree(ref Vector2 coord)
    {
        Vector2 rh = coord - m_screen_center;

        float v = Vector2.Dot(Vector2.right, rh.normalized);//点乘 计算两个向量的夹角，及角色和目标点的夹角
        float degree = Mathf.Acos(v) * Mathf.Rad2Deg;

        if (rh.y > 0)
        {
            degree = 360 - degree + 90;
            if (degree >= 360)
            {
                degree -= 360;
            }
        }
        else
        {
            degree = degree + 90;
        }
        
        return degree;
    }

    float CalcDiffDegree(ref Vector2 coord)
    {
        float l = CalcDegree(ref m_touch_coord);
        float r = CalcDegree(ref coord);

        /*
        float x = (r - l) * 2.0f;

        if(x > 180)
        {
            x -= 360;
        }
        else if(x < -180)
        {
            x += 360;
        }
        */

        float x = r - l;

        if(x > 0)
        {
            x = 4.0f;
        }
        else
        {
            x = -4.0f;
        }

        return x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                do
                {
                    if(!m_begined)
                    {
                        break;
                    }

                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    //向屏幕发射一条射线

                    if (!Physics.Raycast(ray, out m_hit, 200)) //射线长度为200 和地面的碰撞盒做检测
                    {
                        break;
                    }

                    Vector3 move_position;
                    move_position = m_hit.point;//获取碰撞点坐标
                    move_position.y = transform.position.y;

                    if (Vector3.Distance(move_position, mover.m_mover.m_circle.transform.position) < DISTANCE)
                    {
                        break;
                    }

                    Vector2 coord = touch.position;

                    Vector3 dir;
                    dir.x = 0;
                    dir.y = 0;
                    dir.z = 0;

                    calc_direction(move_position, ref dir);
                    mover.m_mover.m_dir.transform.position = mover.m_mover.m_circle.transform.position + dir;

                    float degree = CalcDiffDegree(ref coord);

                    //m_str += "|degree = " + degree.ToString() + ", y = " + game.m_game.m_tank.transform.localRotation.eulerAngles.y.ToString() + "\n";

                    game.m_game.m_tank.GetComponent<tank>().set_angle_diff(degree);

                    m_moved = true;

                    mover.m_mover.m_dir.SetActive(true);

                    m_touch_coord = touch.position;

                } while (false);

                //m_str += "|touch event(moved), m_moved = " + m_moved.ToString();
            }
            else if (touch.phase == TouchPhase.Began)
            {
                do
                {
                    if (uicontrol.ins().HitTest(touch.position))
                    {
                        break;
                    }

                    do
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        //向屏幕发射一条射线

                        if (!Physics.Raycast(ray, out m_hit, 200)) //射线长度为200 和地面的碰撞盒做检测
                        {
                            break;
                        }

                        Vector3 v = m_hit.point;
                        v.y = transform.position.y;
                        mover.m_mover.m_circle.transform.position = v;
                        mover.m_mover.m_dir.transform.position = v;

                        m_touch_coord = touch.position;

                        mover.m_mover.m_circle_diff = mover.m_mover.m_circle.transform.position - game.m_game.m_tank.transform.position;

                        m_begined = true;

                        game.m_game.m_tank.GetComponent<tank>().pause();

                        game.m_game.m_tank.GetComponent<tank>().stop_audio();

                    } while (false);

                    mover.m_mover.m_circle.SetActive(true);

                } while (false);

                //m_str = "";
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                do
                {
                    if (!m_begined)
                    {
                        break;
                    }

                    if (!m_moved)
                    {
                        game.m_game.m_tank.GetComponent<tank>().resume();
                    }

                    // m_str += "|touch event(ended), m_begined = " + m_begined.ToString() + ", m_moved = " + m_moved.ToString();

                    m_begined = false;
                    m_moved = false;

                    mover.m_mover.m_circle.SetActive(false);
                    mover.m_mover.m_dir.SetActive(false);

                    game.m_game.m_tank.GetComponent<tank>().play_audio();

                } while (false);
            }
        }
    }

    void OnGUI()
    {
        return;

        if (game.m_game.m_tank == null)
        {
            return;
        }

        int size = 0;

        //string str = ", pause = " + game.m_game.m_tank.GetComponent<tank>().m_pause.ToString() + ", rotate = " + game.m_game.m_tank.GetComponent<tank>().m_rotate.ToString() + ", now y = " + game.m_game.m_tank.transform.localRotation.eulerAngles.y.ToString() + ", self tank dir" + game.m_game.m_tank.GetComponent<tank>().m_dir.ToString();
        string str = "";

        str += m_str;

        size = GUI.skin.label.fontSize;
        GUI.skin.label.fontSize = 40;
        GUI.Label(new Rect(10, 80, 1000, 800), str);
        GUI.skin.label.fontSize = size;

        m_str = "";
    }
}
