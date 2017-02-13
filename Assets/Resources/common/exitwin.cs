using UnityEngine;
using System.Collections;

public class exitwin : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Transform child = gameObject.transform.FindChild("root");
        if(child != null)
        {
            child = child.FindChild("center");
            if (child != null)
            {
                child = child.FindChild("win");
                if (child != null)
                {
                    Transform tc = child.FindChild("ok");
                    if(tc != null)
                    {
                        UIEventListener.Get(tc.gameObject).onClick = OkClick;
                    }

                    tc = child.FindChild("cancel");
                    if (tc != null)
                    {
                        UIEventListener.Get(tc.gameObject).onClick = CancelClick;
                    }
                }
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OkClick(GameObject obj)
    {
        Application.Quit();
    }

    public void CancelClick(GameObject obj)
    {
        Destroy(gameObject);
    }
}
