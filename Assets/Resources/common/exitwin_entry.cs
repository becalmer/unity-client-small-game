using UnityEngine;
using System.Collections;

public class exitwin_entry : MonoBehaviour {

    public Transform m_exitwin_perfab = null;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(m_exitwin_perfab);
        }

    }
}
