using UnityEngine;
using System.Collections;

public class rank : MonoBehaviour
{
    public UILabel[] m_nths;
    public UILabel[] m_names;

    static rank m_ins;

    void Start()
    {
        m_ins = this;
        m_list = new System.Collections.Generic.List<proto.rank_role_data_t>();
    }

    public static rank ins()
    {
        return m_ins;
    }

    public System.Collections.Generic.List<proto.rank_role_data_t> m_list;
}
