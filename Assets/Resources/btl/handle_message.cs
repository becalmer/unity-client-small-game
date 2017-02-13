using UnityEngine;
using System.Collections;

namespace ns_btl
{
    public class handle_message_t
    {
        public static void init()
        {
            message_cb_t.set_enter_sight_cb(enter_sight);
            message_cb_t.set_leave_sight_cb(leave_sight);
            message_cb_t.set_update_role_pos_cb(update_role_pos);
            message_cb_t.set_update_role_dir_cb(update_role_dir);
            message_cb_t.set_btl_start_cb(btl_start);
            message_cb_t.set_btl_over_cb(btl_over);
            message_cb_t.set_pause_cb(pause);
            message_cb_t.set_resume_cb(resume);
            message_cb_t.set_shot_cb(shot);
            message_cb_t.set_damage_cb(damage);
            message_cb_t.set_enter_btl_cb(enter_btl);
            message_cb_t.set_leave_btl_cb(leave_btl);
            message_cb_t.set_rank_cb(rank);
            message_cb_t.set_rank_changed_cb(rank_changed);
            message_cb_t.set_revive_coord_cb(revive_coord);
            message_cb_t.set_revive_cb(revive);
            message_cb_t.set_resume_complete_cb(resume_complete);
        }

        public static void enter_sight(ref System.Collections.Generic.List<proto.role_data_t> list)
        {
            int c = list.Count;
            if (c > 0)
            {
                for (int x = 0; x < c; ++x)
                {
                    proto.role_data_t info = list[x];
                    handle_message_aux_t.spawn_tank(ref info);
                }
            }
        }

        public static void leave_sight(ref System.Collections.Generic.List<uint> list)
        {
            int c = list.Count;
            if (c > 0)
            {
                for (int x = 0; x < c; ++x)
                {
                    uint role_id = list[x];
                    handle_message_aux_t.remove_tank(role_id);
                }
            }
        }

        public static void update_role_pos(ref System.Collections.Generic.List<proto.role_pos_t> list)
        {
            int c = list.Count;
            if (c > 0)
            {
                for (int x = 0; x < c; ++x)
                {
                    proto.role_pos_t info = list[x];
                    sys_t.g_sys.m_log.write("[update role pos, role id = " + info.m_role_id.ToString() + "]\n");
                    handle_message_aux_t.update_role_pos(ref info);
                }
            }
        }

        public static void update_role_dir(ref System.Collections.Generic.List<proto.role_dir_t> list)
        {
            int c = list.Count;
            if (c > 0)
            {
                for (int x = 0; x < c; ++x)
                {
                    proto.role_dir_t info = list[x];
                    handle_message_aux_t.update_role_dir(ref info);
                }
            }
        }

        static void btl_start()
        {
            game.m_game.m_tank.GetComponent<tank>().set_walk();

            ns_btl.send_message_t.rank();
        }

        static void btl_over()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Resources/home/home");
        }

        static void pause(uint role_id)
        {
            int c = sys_t.g_sys.m_game_data.m_roles.Count;
            if (c > 0)
            {
                int x = 0;
                for (; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if(role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().other_pause();
                        return;
                    }
                }
            }
        }

        static void resume(uint role_id, proto.vector2_t dir)
        {
            int c = sys_t.g_sys.m_game_data.m_roles.Count;
            if (c > 0)
            {
                int x = 0;
                for (; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if(role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().other_resume(ref dir);
                        return;
                    }
                }
            }
        }

        static void shot(uint atk_role_id, uint target_role_id)
        {
            int c = sys_t.g_sys.m_game_data.m_roles.Count;

            if (atk_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (target_role_id == info.m_role_id)
                    {
                        game.m_game.m_tank.GetComponent<tank>().spawn_bullet(info.m_gameobject.transform.position);
                        return;
                    }
                }
            }
            else if (target_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (atk_role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().spawn_bullet(game.m_game.m_tank.transform.position);
                        return;
                    }
                }
            }
            else
            {
                tank _tank = null;
                GameObject _target = null;
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (atk_role_id == info.m_role_id)
                    {
                        _tank = info.m_gameobject.GetComponent<tank>();
                        if (_target != null)
                        {
                            break;
                        }
                    }
                    else if(target_role_id == info.m_role_id)
                    {
                        _target = info.m_gameobject;
                        if(_tank != null)
                        {
                            break;
                        }
                    }
                }

                if ((_target != null) && (_tank != null))
                {
                    _tank.spawn_bullet(_target.transform.position);
                }
            }
        }

        static void damage(uint atk_role_id, uint target_role_id, uint damage)
        {
            sys_t.g_sys.m_log.write("[damage atk role id = " + atk_role_id.ToString() + ", target role id = " + target_role_id.ToString() + ", damage = " + damage.ToString() + "]\n");

            int c = sys_t.g_sys.m_game_data.m_roles.Count;

            if (target_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                game.m_game.m_tank.GetComponent<tank>().damage(damage);

                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (atk_role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().destroy_bullet();

                        return;
                    }
                }
            }
            else if (atk_role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (target_role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().damage(damage);

                        break;
                    }
                }

                game.m_game.m_tank.GetComponent<tank>().destroy_bullet();
            }
            else
            {
                tank atk_tank = null;
                tank target_tank = null;

                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (atk_role_id == info.m_role_id)
                    {
                        atk_tank = info.m_gameobject.GetComponent<tank>();
                        if (target_tank != null)
                        {
                            break;
                        }
                    }
                    else if (target_role_id == info.m_role_id)
                    {
                        target_tank = info.m_gameobject.GetComponent<tank>();
                        if (atk_tank != null)
                        {
                            break;
                        }
                    }
                }

                if(target_tank != null)
                {
                    target_tank.damage(damage);
                }

                if (atk_tank != null)
                {
                    atk_tank.destroy_bullet();
                }
            }
        }

        static void enter_btl(uint role_id, ref proto.vector2_t pos)
        {
            if(role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                game.m_game.m_tank.GetComponent<tank>().enter_btl(ref pos);

                sys_t.g_sys.m_log.write("[self enter btl, role id = " + role_id.ToString() + ", pos.x = " + pos.x.ToString() + ", pos.z = " + pos.z.ToString() + "]\n");
            }
            else
            {
                int c = sys_t.g_sys.m_game_data.m_roles.Count;
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if(role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().enter_btl(ref pos);

                        sys_t.g_sys.m_log.write("[enter btl, role id = " + role_id.ToString() + ", pos.x = " + pos.x.ToString() + ", pos.z = " + pos.z.ToString() +  "]\n");

                        break;
                    }
                }
            }            
        }

        static void leave_btl(uint role_id, ref proto.vector2_t pos)
        {
            if (role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                game.m_game.m_tank.GetComponent<tank>().leave_btl(ref pos);

                sys_t.g_sys.m_log.write("[self leave btl, role id = " + role_id.ToString() + ", pos.x = " + pos.x.ToString() + ", pos.z = " + pos.z.ToString() + "]\n");
            }
            else
            {
                int c = sys_t.g_sys.m_game_data.m_roles.Count;
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().leave_btl(ref pos);

                        sys_t.g_sys.m_log.write("[leave btl, role id = " + role_id.ToString() + ", pos.x = " + pos.x.ToString() + ", pos.z = " + pos.z.ToString() + "]\n");
                        break;
                    }
                }
            }
        }

        static void rank(byte nth, ref System.Collections.Generic.List<proto.rank_role_data_t> list)
        {
            sys_t.g_sys.m_game_data.m_user_data.m_nth = nth;

            int x = 0;

            for (; x < list.Count; ++x)
            {
                handle_message_aux_t.set_rank_info(x, list[x]);
            }
        }

        static void rank_changed(ref System.Collections.Generic.List<proto.rank_changed_info_t> list)
        {
            int x = 0;

            for(; x < list.Count; ++ x)
            {
                handle_message_aux_t.rank_changed(list[x]);
            }
        }

        static void revive_coord(uint role_id, ref proto.vector2_t pos)
        {
            if (role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                game.m_game.m_tank.GetComponent<tank>().set_revive_pos(ref pos);

                sys_t.g_sys.m_log.write("[self revive_coord, role id = " + role_id.ToString() + ", pos.x = " + pos.x.ToString() + ", pos.z = " + pos.z.ToString() + "]\n");
            }
            else
            {
                int c = sys_t.g_sys.m_game_data.m_roles.Count;
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (role_id == info.m_role_id)
                    {
                        info.m_gameobject.GetComponent<tank>().set_revive_pos(ref pos);

                        sys_t.g_sys.m_log.write("[revive_coord, role id = " + role_id.ToString() + ", pos.x = " + pos.x.ToString() + ", pos.z = " + pos.z.ToString() + "]\n");
                        break;
                    }
                }
            }
        }

        static void revive(uint role_id)
        {
            if (role_id == sys_t.g_sys.m_game_data.m_user_data.m_role_id)
            {
                //game.m_game.m_tank.GetComponent<tank>().set_revive_pos(ref pos);

                sys_t.g_sys.m_log.write("[self revive, role id = " + role_id.ToString() + "]\n");
            }
            else
            {
                int c = sys_t.g_sys.m_game_data.m_roles.Count;
                for (int x = 0; x < c; ++x)
                {
                    role_t info = sys_t.g_sys.m_game_data.m_roles[x];
                    if (role_id == info.m_role_id)
                    {
                        //info.m_gameobject.GetComponent<tank>().set_revive_pos(ref pos);

                        sys_t.g_sys.m_log.write("[revive, role id = " + role_id.ToString() + "]\n");
                        break;
                    }
                }
            }
        }

        static void resume_complete()
        {
            game.m_game.m_tank.GetComponent<tank>().resume_close();
        }
    }
}
