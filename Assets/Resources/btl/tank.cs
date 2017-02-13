using UnityEngine;
using System.Collections;

/*
public struct track_info_t
{
    public Vector3 m_pos;
    public Vector3 m_dir;
};
*/

public class tank : MonoBehaviour
{
    enum audio_t
    {
        NONE,
        MOVE,
        FIRE,
    };

    audio_t m_audio_type = audio_t.NONE;

    private uint m_hp;
    uint m_hpo;

    //public Texture2D m_blood_tex;

    public string m_name;

    private Rect m_name_rect;

    private Vector2 m_name_pos;
    private Vector2 m_blood_pos;

    public Vector3 m_dir;

    public bool m_moved;

    float m_accu_ms;
    float m_accu_rate;
    float m_last_frame_ms;

    private Vector3 m_fix_pos;
    private bool m_pos_fix;

    public bool m_rotate;
    private float m_angle_diff;
    private float m_rotate_accu_ms;

    public bool m_pause;

    GUIContent m_name_gui_content;
    Vector2 m_blood_size;

    Rect m_blood_rect;

    bool m_delay_resume;

    public GameObject m_bullet;

    float m_length_square;

    float m_spawn_bullet_time;
    public float m_bullet_time;

    bool m_reviving;
    Vector3 m_revive_pos;

    bool m_btling;

    string m_str;

    int m_offset_idx;

    bool m_resume_complete;
    float m_resume_deadline;



    //Vector3 m_tracked;
    //System.Collections.Generic.Queue<track_info_t> m_track_queue;

    //System.Collections.Generic.Queue<GameObject> m_bullets;

    // Use this for initialization

    static System.Collections.Generic.Queue<int> m_list;

    void Start ()
    {
        m_resume_complete = true;

        m_btling = false;

        m_reviving = false;

        m_bullet = null;

        m_pos_fix = false;

        m_moved = false;

        m_hpo = m_hp = 100;
        
        m_accu_ms = 0.0f;
        m_accu_rate = 0.0f;
        m_last_frame_ms = Time.time;

        m_rotate = false;
        m_angle_diff = 0.0f;
        m_rotate_accu_ms = 0.0f;

        /*
        GameObject go = GameObject.Find("plane");
        if(go != null)
        {
            int x = 0;
        }
        */

        /*
        {
            Vector3 v = gameObject.GetComponent<Renderer>().bounds.size;
            int y = 0;
            y++;
        }
        */
            /*
                    Transform child = gameObject.transform.FindChild("Body");
                    if (child != null)
                    {
                        Transform tr = child;
                        child = child.FindChild("Meshes");
                        if (child != null)
                        {                
                            child = child.FindChild("BodyMesh");
                            if (child != null)
                            {
                                //Vector3 vz = child.GetComponent<Renderer>().bounds.size;
                                //int x = 0;

                                child.GetComponent<Renderer>().sortingOrder = 30;
                            }
                        }

                        child = tr.FindChild("TurretPivot");
                        if (child != null)
                        {
                            //Vector3 vz = child.GetComponent<Renderer>().bounds.size;
                            //int x = 0;

                            child = child.FindChild("arrow");
                            if (child != null)
                            {
                                child.GetComponent<Renderer>().sortingOrder = 30;
                            }
                        }
                    }
            */
            /*
            Transform blood = gameObject.transform.FindChild("blood");
            if (null != blood)
            {
                Transform t = gameObject.transform;
                Vector3 s = gameObject.GetComponent<Collider>().bounds.size;
                GameObject obj = blood.gameObject;
                s = blood.gameObject.GetComponent<Collider>().bounds.size;
                Matrix4x4 m = Matrix4x4.Scale(obj.transform.localScale);
                m = m * obj.transform.localToWorldMatrix;
                Vector3 p = m.MultiplyPoint(obj.transform.localPosition);
                int x;
            }
            */
        if (this.gameObject == game.m_game.m_tank)
        {
            Color color = new Color(0.75f, 0, 0, 1);
            SetArrowMaterialColor(ref color);

            Vector3 pos = sys_t.g_sys.m_game_data.m_user_data.m_pos;
            pos.y = 0.4f;

            gameObject.transform.position = pos;
            m_pause = true;
        }
        else
        {
            int c = sys_t.g_sys.m_game_data.m_roles.Count;
            for(int x = 0; x < c; ++ x)
            {
                role_t role = sys_t.g_sys.m_game_data.m_roles[x];
                if(this.gameObject == role.m_gameobject)
                {
                    Color color = new Color(0, 0.75f, 0, 1);
                    SetArrowMaterialColor(ref color);

                    Vector3 pos = role.m_pos;
                    pos.y = 0.4f;

                    gameObject.transform.position = pos;
                    m_pause = false;
                }
            }
        }

        init_rotate();

        m_delay_resume = false;
/*
        Vector3 vec;
        vec.x = 0.1f;
        vec.y = 0.1f;
        vec.z = 0.1f;

        gameObject.transform.localScale = vec;
*/
        if(sys_t.g_sys.m_game_data.m_self_start)
        {
            set_walk();
            ns_btl.send_message_t.rank();
        }

        m_delay_resume = false;

        play_move_audio();

        m_length_square = 2.2f * 2.2f;

        //m_track_queue = new System.Collections.Generic.Queue<track_info_t>();
        //

        //

        //m_bullets = new System.Collections.Generic.Queue<GameObject>();

        Transform child = gameObject.transform.FindChild("blood");
        //child = gameObject.transform.FindChild("blood");
        if (child != null)
        {
            child.GetComponent<Renderer>().sortingOrder = 30;
        }

        allocator_idx();
    }

    void SetArrowMaterialColor(ref Color color)
    {
        Transform child = gameObject.transform.FindChild("arrow");
        if (null != child)
        {
            Renderer renderer = child.gameObject.GetComponent<Renderer>();
            if (null != renderer)
            {
                Material mat = renderer.sharedMaterial;
                if (null != mat)
                {
                    mat.color = color;
                }
            }
        }
    }

    void init_rotate()
    {

        return;

        Vector3 rh = m_dir;

        rh.y = 0;

        float v = Vector3.Dot(Vector3.right, rh.normalized);//点乘 计算两个向量的夹角，及角色和目标点的夹角
        float angle = Mathf.Acos(v) * Mathf.Rad2Deg;

        if (rh.z > 0)
        {
            angle = 360 - angle + 90;
            if (angle >= 360)
            {
                angle -= 360;
            }
        }
        else
        {
            angle = angle + 90;
        }

        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    public void set_name(string name)
    {
        m_name = name;
        m_name_gui_content = new GUIContent(m_name);

        Transform child = gameObject.transform.FindChild("name");
        if(child != null)
        {
            child.gameObject.GetComponent<TextMesh>().text = name;
        }

        set_blood_rate(100);
    }

    bool can_move(Vector3 pos)
    {
        if (!map.m_map.x_can_move(pos.x, m_dir.x))
        {
            return false;
        }

        if (!map.m_map.z_can_move(pos.z, m_dir.z))
        {
            return false;
        }

        return true;
    }

    public void pause()
    {
        //touch.m_touch.m_str += " self pause ---";

        stop_move_audio();

        m_pause = true;

        m_accu_ms = 0.0f;
        m_accu_rate = 0.0f;
        m_last_frame_ms = 0.0f;

        ns_btl.send_message_t.pause();
    }

    public void resume()
    {
        //touch.m_touch.m_str += " self resume ---";

        play_move_audio();

        m_last_frame_ms = Time.time;

        float degree = gameObject.transform.localRotation.eulerAngles.y;

        float v = degree / Mathf.Rad2Deg;

        m_dir.x = Mathf.Sin(v);
        m_dir.y = 0;
        m_dir.z = Mathf.Cos(v);

        int m = (int)(m_dir.x * temp_t.__RATE__);
        m_dir.x = m * 1.0f / temp_t.__RATE__;

        m = (int)(m_dir.z * temp_t.__RATE__);
        m_dir.z = m * 1.0f / temp_t.__RATE__;

        set_walk();

        resume_open();
    }

    public void set_walk()
    {
        m_pause = false;
        m_last_frame_ms = Time.time;
        m_accu_ms = 0.0f;
        m_accu_rate = 0.0f;

        play_move_audio();
    }

    void delay_resume()
    {
        m_delay_resume = true;
    }

    public void other_pause()
    {
        //touch.m_touch.m_str += " other pause ---";

        m_pause = true;

        stop_move_audio();

        m_last_frame_ms = 0.0f;
        m_accu_ms = 0.0f;
        m_accu_rate = 0.0f;
    }

    public void other_resume(ref proto.vector2_t dir)
    {
        play_move_audio();

        Vector3 rh;

        rh.x = 0;
        rh.y = 0;
        rh.z = 0;

        temp_t.convert(ref dir, ref rh);

        rh.y = 0;

        float v = Vector3.Dot(Vector3.right, rh.normalized);//点乘 计算两个向量的夹角，及角色和目标点的夹角
        float angle = Mathf.Acos(v) * Mathf.Rad2Deg;

        if (rh.z > 0)
        {
            angle = 360 - angle + 90;
            if (angle >= 360)
            {
                angle -= 360;
            }
        }
        else
        {
            angle = angle + 90;
        }

        Quaternion to = Quaternion.Euler(0.0f, angle, 0.0f);

        float angle_diff = to.eulerAngles.y - transform.localRotation.eulerAngles.y;

        if (angle_diff > 180)
        {
            angle_diff = angle_diff - 360;
        }
        else if (angle_diff < -180)
        {
            angle_diff = angle_diff + 360;
        }

        set_angle_diff(angle_diff);

        m_last_frame_ms = Time.time;
    }

    bool check_others()
    {
        if(sys_t.g_sys.m_game_data == null)
        {
            sys_t.g_sys.m_log.write("[game data null]\n");
        }

        if (sys_t.g_sys.m_game_data.m_roles == null)
        {
            sys_t.g_sys.m_log.write("[roles null]\n");
        }

        int m = sys_t.g_sys.m_game_data.m_roles.Count;
        for (int x = 0; x < m; ++ x)
        {
            Vector3 diff = gameObject.transform.position - sys_t.g_sys.m_game_data.m_roles[x].m_gameobject.transform.position;
            float d = diff.x * diff.x + diff.z * diff.z;
            if(d <= map.NEAR)
            {
                Vector3 future = gameObject.transform.position + m_dir;
                diff = future - sys_t.g_sys.m_game_data.m_roles[x].m_gameobject.transform.position;
                float e = diff.x * diff.x + diff.z * diff.z;
                if(e < d)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void move()
    {
        if(m_pause)
        {
            return;
        }

        m_moved = true;

        if (!can_move(transform.position))
        {
            m_moved = false;
        }

        if(m_moved)
        {
            m_moved = check_others();
        }

        float ms = Time.time;

        m_accu_ms += ms - m_last_frame_ms;

        float accu_rate = m_accu_ms * map.SPEED;

        if (m_moved)
        {
            float rrate = 1.0f;

            if (m_btling)
            {
                rrate = 0.5f;
            }

            float diff_rate = accu_rate - m_accu_rate;

            float md = diff_rate * rrate;

            transform.position += m_dir * md;
        }

        m_accu_rate = accu_rate;
        m_last_frame_ms = ms;

        if (m_accu_ms >= 1.0f)
        {
            m_accu_ms = 0.0f;
            m_accu_rate = 0.0f;
        }
        /*
        if (m_moved)
        {
            track_info_t info;

            info.m_pos = gameObject.transform.position;
            info.m_dir = m_dir;

            m_track_queue.Enqueue(info);
        }
        */

        m_str = "<< nick = " + m_name + ", pos" + gameObject.transform.position.ToString("F4") + ", m_dir" + m_dir.ToString("F4") + ", m_pos_fix = " + m_pos_fix.ToString() + ", m_fix_pos" + m_fix_pos.ToString("F4") + " >>\n";
    }

    public void set_angle_diff(float angle_diff)
    {
        m_rotate = true;
        m_angle_diff = angle_diff;
    }

    void rotate()
    {
        if(!m_rotate)
        {
            return;
        }

        float rotate_interval = 0.01f;
        float neg_rotate_angle = -2.0f;
        float pos_rotate_angle = 2.0f;

        m_rotate_accu_ms += Time.deltaTime;

        if(m_rotate_accu_ms >= rotate_interval)
        {
            m_rotate_accu_ms = 0.0f;
            float rot = m_angle_diff;

            if(m_angle_diff > 0)
            {
                if (m_angle_diff >= pos_rotate_angle)
                {
                    rot = pos_rotate_angle;
                }
            }
            else if(m_angle_diff < 0)
            {
                if (m_angle_diff <= neg_rotate_angle)
                {
                    rot = neg_rotate_angle;
                }
            }

            m_angle_diff -= rot;

            transform.Rotate(0.0f, rot, 0.0f);
        }

        float minprecision = -0.000001f;
        float maxprecision = 0.000001f;

        if ((m_angle_diff >= minprecision) && (m_angle_diff <= maxprecision))
        {
            m_rotate = false;

            if(gameObject == game.m_game.m_tank)
            {
                delay_resume();
            }
            else
            {
                set_walk();
            }
        }
    }

    void fix_pos()
    {
        if (!m_pos_fix)
        {
            return;
        }

        float minprecision = -0.000001f;
        float maxprecision = 0.000001f;

        float minfix = -0.01f;
        float maxfix = 0.01f;

        Vector3 compensate;

        compensate.x = 0.0f;
        compensate.y = 0.0f;
        compensate.z = 0.0f;

        bool xcom = false;
        bool zcom = false;

        if (!((m_fix_pos.x >= minprecision) && (m_fix_pos.x <= maxprecision)))
        {
            if (m_fix_pos.x > 0)
            {
                if (m_fix_pos.x >= maxfix)
                {
                    compensate.x = maxfix;
                    m_fix_pos.x -= maxfix;
                }
                else
                {
                    compensate.x = m_fix_pos.x;
                    m_fix_pos.x = 0.0f;
                }
            }
            else
            {
                if (m_fix_pos.x <= minfix)
                {
                    compensate.x = minfix;
                    m_fix_pos.x -= minfix;
                }
                else
                {
                    compensate.x = m_fix_pos.x;
                    m_fix_pos.x = 0.0f;
                }
            }
        }
        else
        {
            xcom = true;
        }

        if (!((m_fix_pos.z >= minprecision) && (m_fix_pos.z <= maxprecision)))
        {
            if (m_fix_pos.z > 0)
            {
                if (m_fix_pos.z >= maxfix)
                {
                    compensate.z = maxfix;
                    m_fix_pos.z -= maxfix;
                }
                else
                {
                    compensate.z = m_fix_pos.z;
                    m_fix_pos.z = 0.0f;
                }
            }
            else
            {
                if (m_fix_pos.z <= minfix)
                {
                    compensate.z = minfix;
                    m_fix_pos.z -= minfix;
                }
                else
                {
                    compensate.z = m_fix_pos.z;
                    m_fix_pos.z = 0.0f;
                }
            }
        }
        else
        {
            zcom = true;
        }

        transform.position = transform.position + compensate;

        if(xcom && zcom)
        {
            m_pos_fix = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        move();
        rotate();
        fix_pos();
        //destroy_bullets();
        //spawn_track();
	}
    /*
    void spawn_track()
    {
        if(m_track_queue.Count <= 0)
        {
            return;
        }

        track_info_t info = m_track_queue.Peek();

        Vector3 diff = info.m_pos - gameObject.transform.position;

        if(diff.sqrMagnitude < m_length_square)
        {
            return;
        }

        do
        {
            diff = info.m_pos - m_tracked;
            if (diff.sqrMagnitude < 0.01)
            {
                break;
            }

            m_tracked = info.m_pos;

            GameObject track = ((Transform)(Instantiate(game.m_game.m_track_perfab, game.m_game.m_track_perfab.position, game.m_game.m_track_perfab.rotation))).gameObject;

            track.GetComponent<Renderer>().sortingOrder = 5;

            track.GetComponent<trackx>().m_pos = info.m_pos;

            float v = Vector3.Dot(Vector3.right, info.m_dir);//点乘 计算两个向量的夹角，及角色和目标点的夹角
            float angle = Mathf.Acos(v) * Mathf.Rad2Deg;

            if (info.m_dir.z > 0)
            {
                angle = 360 - angle + 90;
                if (angle >= 360)
                {
                    angle -= 360;
                }
            }
            else
            {
                angle = angle + 90;
            }

            track.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        } while (false);

        m_track_queue.Dequeue();
    }
    */
    void Update()
    {
        tick_delay_resume();
        tick_resume();
    }

    void tick_delay_resume()
    {
        if(m_delay_resume)
        {
            if(touch.m_touch.m_begined)
            {
                return;
            }
            resume();
            m_delay_resume = false;
        }
    }

    /*
    void fix_name_pos(Vector2 pos)
    {
        float diff = pos.x - m_name_pos.x;
        if((diff >= 0.2) || (diff <= -0.2))
        {
            m_name_pos = pos;
            return;
        }
        diff = pos.y - m_name_pos.y;
        if ((diff >= 0.2) || (diff <= -0.2))
        {
            m_name_pos = pos;
            return;
        }
    }
    
    void fix_blood_pos(Vector2 pos)
    {
        float diff = pos.x - m_blood_pos.x;
        if ((diff >= 0.2) || (diff <= -0.2))
        {
            m_name_pos = pos;
            return;
        }
        diff = pos.y - m_blood_pos.y;
        if ((diff >= 0.2) || (diff <= -0.2))
        {
            m_blood_pos = pos;
            return;
        }
    }

    void DrawName()
    {
        if(null == m_name_gui_content)
        {
            return;
        }

        Vector2 position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //得到真实NPC头顶的2D坐标
        position.y = Screen.height - position.y + 30;

        fix_name_pos(position);

        int size = GUI.skin.label.fontSize;
        GUI.skin.label.fontSize = 20;

        Vector2 name_size = GUI.skin.label.CalcSize(m_name_gui_content);
        //设置显示颜色为黄色
        GUI.color = Color.blue;

        m_name_rect.x = m_name_pos.x - (name_size.x / 2);
        m_name_rect.y = m_name_pos.y - name_size.y;
        m_name_rect.width = name_size.x;
        m_name_rect.height = name_size.y;

        GUI.Label(m_name_rect, m_name);
        GUI.skin.label.fontSize = size;
    }

    void DrawBlood()
    {
        Vector2 position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //得到真实NPC头顶的2D坐标
        position.x -= 30;
        position.y = Screen.height - position.y + 60;

        fix_name_pos(position);

        //通过血值计算红色血条显示区域
        //先绘制血条

        if(!m_calc_blood)
        {
            m_calc_blood = true;
            //计算出血条的宽高
            m_blood_size = GUI.skin.label.CalcSize(new GUIContent(m_blood_tex));
        }

        m_blood_rect.x = position.x - (m_blood_size.x / 2);
        m_blood_rect.y = position.y - m_blood_size.y;
        m_blood_rect.width = m_blood_size.x * 2;
        m_blood_rect.height = m_blood_size.y * 2;

        GUI.DrawTexture(m_blood_rect, m_blood_tex);
    }
    */

    void OnGUI()
    {
        //DrawName();
        //DrawBlood();

        int size = GUI.skin.label.fontSize;
        GUI.skin.label.fontSize = 30;
        GUI.Label(new Rect(10, 260 + m_offset_idx * 50, 1920, 50), m_str);
        GUI.skin.label.fontSize = size;
    }

    public void set_pos(ref Vector3 pos)
    {
        //gameObject.transform.position = pos;

        //return;

        m_pos_fix = true;
        m_fix_pos = pos - gameObject.transform.position;

        sys_t.g_sys.m_log.write("[pos x = " + gameObject.transform.position.x.ToString() + ", y = " + gameObject.transform.position.y.ToString() + ", z = " + gameObject.transform.position.z.ToString() + ", fix pos x = " + m_fix_pos.x.ToString() + ", y = " + m_fix_pos.y.ToString() + ", z = " + m_fix_pos.z.ToString() + "]\n");

        float minprecision = -0.000001f;
        float maxprecision = 0.000001f;
        if ((m_fix_pos.x >= minprecision) && (m_fix_pos.x <= maxprecision))
        {
            if ((m_fix_pos.z >= minprecision) && (m_fix_pos.z <= maxprecision))
            {
                m_pos_fix = false;
            }
        }
    }

    void play_move_audio()
    {
        Transform child = gameObject.transform.FindChild("move");
        if (child != null)
        {
            AudioSource audio = child.gameObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();

                m_audio_type = audio_t.MOVE;
            }
        }
    }

    void stop_move_audio()
    {
        Transform child = gameObject.transform.FindChild("move");
        if (child != null)
        {
            AudioSource audio = child.gameObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Stop();

                m_audio_type = audio_t.NONE;
            }
        }
    }

    void play_fire_audio()
    {
        Transform child = gameObject.transform.FindChild("fire");
        if(child != null)
        {
            AudioSource audio = child.gameObject.GetComponent<AudioSource>();
            if(audio != null)
            {
                audio.Play();

                m_audio_type = audio_t.FIRE;
            }
        }
    }

    void stop_fire_audio()
    {
        Transform child = gameObject.transform.FindChild("fire");
        if (child != null)
        {
            AudioSource audio = child.gameObject.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Stop();

                m_audio_type = audio_t.NONE;
            }
        }
    }

    public void stop_audio()
    {
        audio_t tp = m_audio_type;

        switch(tp)
        {
            case audio_t.MOVE:
            {
                stop_move_audio();
                break;
            }
            case audio_t.FIRE:
            {
                stop_fire_audio();
                break;
            }
        }

        m_audio_type = tp;
    }

    public void play_audio()
    {
        switch (m_audio_type)
        {
            case audio_t.MOVE:
            {
                play_move_audio();
                break;
            }
            case audio_t.FIRE:
            {
                play_fire_audio();
                break;
            }
        }
    }

    public void spawn_bullet(Vector3 target_pos)
    {
        if (null != m_bullet)
        {
            destroy_bullet();
        }

        m_spawn_bullet_time = Time.time;

        Vector3 bullet_pos = gameObject.transform.position + m_dir * 1.8f;

        target_pos.y = 0;
        bullet_pos.y = 0;

        Vector3 bullet_dir = target_pos - bullet_pos;

        bullet_dir.y = 0;

        bullet_dir.Normalize();

        bullet_dir.y = 0;

        bullet_pos.y = 1.5f;

        GameObject bullet = ((Transform)(Instantiate(game.m_game.m_bullet_perfab, game.m_game.m_bullet_perfab.position, game.m_game.m_bullet_perfab.rotation))).gameObject;
        bullet.GetComponent<bullet>().set_data(ref target_pos, ref bullet_pos, ref bullet_dir);

        bullet.GetComponent<Renderer>().sortingOrder = 10;

        //sys_t.g_sys.m_log.write("[exist = " + exist.ToString() + ", spawn_bullet role id = " + role_id.ToString() + "]\n");        

        float v = Vector3.Dot(Vector3.right, bullet_dir);//点乘 计算两个向量的夹角，及角色和目标点的夹角
        float angle = Mathf.Acos(v) * Mathf.Rad2Deg;

        if (bullet_dir.z > 0)
        {
            angle = 360 - angle + 90;
            if (angle >= 360)
            {
                angle -= 360;
            }
        }
        else
        {
            angle = angle + 90;
        }

        bullet.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        m_bullet = bullet;
                
        play_fire_audio();
    }

    public void destroy_bullet()
    {
        if (null != m_bullet)
        {
            Vector3 p = m_bullet.GetComponent<bullet>().m_target;
            p.y = 1.0f;

            Instantiate(game.m_game.m_explosion_perfab, p, m_bullet.transform.rotation);

            sys_t.g_sys.m_log.write("[destroy_bullet]\n");
            Destroy(m_bullet);

            m_bullet = null;
        }

        m_bullet_time = Time.time - m_spawn_bullet_time;
                
        stop_fire_audio();
    }

    public void damage(uint damage)
    {
        if(damage > 0)
        {
            if(m_hp > damage)
            {
                m_hp = m_hp - damage;
            }
            else
            {
                m_hp = 0;
            }

            int rate = (int)(m_hp * 100.0 / m_hpo);

            sys_t.g_sys.m_log.write("[ rate = " + rate.ToString() + "]\n");

            set_blood_rate(rate);
        }

        sys_t.g_sys.m_log.write("[damage, hp = " + m_hp.ToString() + "]\n");
    }

    public void enter_btl(ref proto.vector2_t pos)
    {
        m_btling = true;

        Vector3 p;
        p.x = 0;
        p.y = 0;
        p.z = 0;

        temp_t.convert(ref pos, ref p);

        set_pos(ref p);
    }

    public void leave_btl(ref proto.vector2_t pos)
    {
        m_btling = false;

        Vector3 p;

        p.x = 0;
        p.y = 0;
        p.z = 0;

        temp_t.convert(ref pos, ref p);

        set_pos(ref p);

        m_last_frame_ms = Time.time;
        m_accu_ms = 0.0f;
        m_accu_rate = 0.0f;
    }

    /*
    void positive_shake()
    {
        if(Time.time < m_shake_deadline)
        {
            float x = (m_shake_deadline - Time.time) / 0.8f;
            float y = x - m_shake_rate;
            m_shake_rate = x;
            Vector3 v = m_share_dir_vec * y;
            m_shake_off += v;

            gameObject.transform.position += v;
        }
        else
        {
            set_negative_shake();
        }
    }

    void negative_shake()
    {
        if (Time.time < m_shake_deadline)
        {
            float x = (m_shake_deadline - Time.time) / 0.2f;
            float y = x - m_shake_rate;
            m_shake_rate = x;
            Vector3 v = m_shake_off * y;

            gameObject.transform.position += v;

        }
        else
        {
            m_shake_dir = shake_dir_t.NONE;
        }
    }

    void set_positive_shake()
    {
        if(m_bullet != null)
        {
            m_shake_dir = shake_dir_t.POSITIVE;
            m_shake_deadline = Time.time + 0.8f;
            m_shake_rate = 0;
            m_share_dir_vec = m_bullet.GetComponent<bullet>().m_dir;
            m_shake_off.x = 0;
            m_shake_off.y = 0;
            m_shake_off.z = 0;
        }        
    }

    void set_negative_shake()
    {
        m_shake_dir = shake_dir_t.NEGATIVE;
        m_shake_deadline = Time.time + 0.2f;
        m_shake_off *= -1;
        m_shake_rate = 0;
    }
    */

    public void set_blood_rate(int rate)
    {
        Transform child = gameObject.transform.FindChild("blood");
        if(child != null)
        {
            child.GetComponent<Renderer>().sharedMaterial.SetInt("_Rate", rate);
        }
    }

    /*
    void add_die_bullet()
    {
        m_bullet.GetComponent<bullet>().die();

        m_bullets.Enqueue(m_bullet);
    }

    void destroy_bullets()
    {
        do
        {
            if (m_bullets.Count <= 0)
            {
                break;
            }
            GameObject bullet = m_bullets.Peek();
            if (bullet.GetComponent<bullet>().done())
            {
                m_bullets.Dequeue();
            }
        } while (false);
    }
    */

    public void set_revive_pos(ref proto.vector2_t p)
    {
        Vector3 v;
        v.x = 0;
        v.y = 0;
        v.z = 0;

        temp_t.convert(ref p, ref v);

        m_revive_pos = v;

        m_reviving = true;
    }

    public void smoke_die()
    {
        
    }

    void tick_resume()
    {
        if(m_resume_complete)
        {
            return;
        }

        if(Time.time >= m_resume_deadline)
        {
            resume_open();
        }
    }

    void resume_open()
    {
        m_resume_complete = false;
        m_resume_deadline = Time.time + 3.0f;

        ns_btl.send_message_t.resume(ref m_dir);
    }

    public void resume_close()
    {
        m_resume_complete = true;
    }

    void allocator_idx()
    {
        if (m_list == null)
        {
            m_list = new System.Collections.Generic.Queue<int>();
            for (int x = 0; x < 10; ++x)
            {
                m_list.Enqueue(x);
            }
        }

        m_offset_idx = m_list.Dequeue();
    }

    void deallocator_idx()
    {
        m_list.Enqueue(m_offset_idx);
    }

    public void DestroySelf()
    {
        deallocator_idx();
    }
}
