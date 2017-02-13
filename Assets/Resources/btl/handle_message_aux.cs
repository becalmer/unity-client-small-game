using UnityEngine;
using System.Collections;

namespace ns_btl
{
    public class handle_message_aux_t
    {
        public static void spawn_tank(ref proto.role_data_t info)
        {
            GameObject tank = ((Transform)(Object.Instantiate(game.m_game.m_tank_perfab, game.m_game.m_tank_perfab.position, game.m_game.m_tank_perfab.rotation))).gameObject;

            temp_t.convert(ref info.m_dir, ref tank.GetComponent<tank>().m_dir);

            tank.GetComponent<tank>().set_name(info.m_nick);

            role_t role = new role_t();

            role.m_role_id = info.m_role_id;
            temp_t.convert(ref info.m_pos, ref role.m_pos);
            role.m_gameobject = tank;

            sys_t.g_sys.m_game_data.m_roles.Add(role);

            //sys_t.g_sys.m_log.write("[spawn tank role(" + info.m_role_id.ToString() + "), nick(" + info.m_nick + "), pos" + role.m_pos.ToString() + ", dir" + tank.GetComponent<tank>().m_dir.ToString() + "\n");
        }

        public static void remove_tank(uint role_id)
        {
            int c = sys_t.g_sys.m_game_data.m_roles.Count;

            for (int x = 0; x < c; ++x)
            {
                role_t role = sys_t.g_sys.m_game_data.m_roles[x];
                if (role_id == role.m_role_id)
                {
                    if (role.m_gameobject != null)
                    {
                        if (role.m_gameobject.GetComponent<tank>().m_bullet != null)
                        {
                            Object.Destroy(role.m_gameobject.GetComponent<tank>().m_bullet);
                        }
                    }

                    role.m_gameobject.GetComponent<tank>().DestroySelf();

                    Object.Destroy(role.m_gameobject);
                    sys_t.g_sys.m_game_data.m_roles.RemoveAt(x);
                    break;
                }
            }
        }

        static void set_tank_pos(ref GameObject obj, ref proto.role_pos_t info)
        {
            Vector3 p;

            p.x = 0;
            p.y = 0;
            p.z = 0;

            temp_t.convert(ref info.m_pos, ref p);

            //sys_t.g_sys.m_log.write("name: " + obj.GetComponent<tank>().m_name + ", cli: " + obj.transform.position.ToString() + ", srv: " + p.ToString() + "\n");

            obj.GetComponent<tank>().set_pos(ref p);
        }

        public static void update_role_pos(ref proto.role_pos_t info)
        {
            if (info.m_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                set_tank_pos(ref game.m_game.m_tank, ref info);
            }
            else
            {
                int m = sys_t.g_sys.m_game_data.m_roles.Count;
                int y = 0;

                for (; y < m; ++y)
                {
                    role_t role = sys_t.g_sys.m_game_data.m_roles[y];
                    if (info.m_role_id == role.m_role_id)
                    {
                        set_tank_pos(ref role.m_gameobject, ref info);
                        break;
                    }
                }
            }
        }

        static void set_tank_dir(ref GameObject obj, ref proto.role_dir_t info)
        {
            Vector3 rh;

            rh.x = 0;
            rh.y = 0;
            rh.z = 0;

            temp_t.convert(ref info.m_dir, ref rh);

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

            float angle_diff = to.eulerAngles.y - obj.transform.localRotation.eulerAngles.y;

            if (angle_diff > 180)
            {
                angle_diff = angle_diff - 360;
            }
            else if (angle_diff < -180)
            {
                angle_diff = angle_diff + 360;
            }

            obj.GetComponent<tank>().set_angle_diff(angle_diff);
        }

        public static void update_role_dir(ref proto.role_dir_t info)
        {
            if (info.m_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                set_tank_dir(ref game.m_game.m_tank, ref info);
            }
            else
            {
                int m = sys_t.g_sys.m_game_data.m_roles.Count;
                for (int y = 0; y < m; ++y)
                {
                    role_t role = sys_t.g_sys.m_game_data.m_roles[y];
                    if (info.m_role_id == role.m_role_id)
                    {
                        set_tank_dir(ref role.m_gameobject, ref info);
                        break;
                    }
                }
            }
        }

        public static void set_rank_info(int nth, proto.rank_role_data_t info)
        {
            int x = nth + 1;

            rank.ins().m_list.Add(info);

            rank.ins().m_nths[nth].text = x.ToString();
            rank.ins().m_names[nth].text = info.m_nick;

            if(info.m_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                rank.ins().m_nths[nth].color = Color.red;
                rank.ins().m_names[nth].color = Color.red;
            }
        }

        public static void rank_changed(proto.rank_changed_info_t info)
        {
            if(info.m_nth >= rank.ins().m_list.Count)
            {
                sys_t.g_sys.m_log.write("rank changed nth = " + info.m_nth.ToString() + ", role id = " + info.m_role_id.ToString() + "\n");
                return;
            }

            proto.rank_role_data_t data = rank.ins().m_list[info.m_nth];

            if(info.m_role_id != data.m_role_id)
            {
                int x = 0;
                for(; x < rank.ins().m_list.Count; ++ x)
                {
                    if(info.m_role_id == rank.ins().m_list[x].m_role_id)
                    {
                        rank_changed(info.m_nth, x);

                        return;
                    }
                }
            }
        }

        static void rank_changed(int x, int y)
        {
            proto.rank_role_data_t info = rank.ins().m_list[x];

            rank.ins().m_list[x] = rank.ins().m_list[y];

            rank.ins().m_list[y] = info;

            rank.ins().m_nths[x].text = rank.ins().m_list[x].m_nick;
            rank.ins().m_names[y].text = rank.ins().m_list[y].m_nick;

            if(rank.ins().m_list[x].m_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                rank.ins().m_nths[x].color = Color.red;
                rank.ins().m_names[x].color = Color.red;

                rank.ins().m_nths[y].color = Color.white;
                rank.ins().m_names[y].color = Color.white;
            }
            else if (rank.ins().m_list[y].m_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                rank.ins().m_nths[y].color = Color.red;
                rank.ins().m_names[y].color = Color.red;

                rank.ins().m_nths[x].color = Color.white;
                rank.ins().m_names[x].color = Color.white;
            }
        }
    }
}