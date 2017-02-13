using UnityEngine;
using System.Collections;

public class map : MonoBehaviour {

    public static map m_map;

    public static float NEAR = 9.0f;

    private float m_width;

    private float m_height;

    private const float distance = 10.0f;

    public const float SPEED = 1.5f;

    public const float BULLET_SPEED = 4.0f;

    public map()
    {
        m_map = this;
    }

    // Use this for initialization
    void Start ()
    {
        m_width = gameObject.GetComponent<Terrain>().terrainData.size.x;
        m_height = gameObject.GetComponent<Terrain>().terrainData.size.z;
    }

    // Update is called once per frame
    void Update ()
    {
	}

    public bool x_can_move(float x, float d)
    {
        if(x <= distance)
        {
            if(d < 0)
            {
                return false;
            }
            return true;
        }
        else if((x + distance) >= m_width)
        {
            if(d > 0)
            {
                return false;
            }
            return true;
        }

        return true;
    }

    public bool z_can_move(float z, float d)
    {
        if (z <= distance)
        {
            if (d < 0)
            {
                return false;
            }
            return true;
        }
        else if ((z + distance) >= m_height)
        {
            if (d > 0)
            {
                return false;
            }
            return true;
        }

        return true;
    }
}
