using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour {

    static public bool buttonPressed = false;
    public float xpos;
    private float xvel = 0;
    private float xacc = 0;

    void Start()
    {
        xpos = transform.position.x;
    }

    void Update () {
        xvel += xacc;
        xpos += xvel; 
		if (buttonPressed)
        {
            if (xpos > 0)
            {
                xacc = .05f;
            }
            else
            {
                xacc = -.05f;
            }
        }
        
        if (xvel > .5f) xvel = .5f;
        if (xvel < -.5f) xvel = -.5f;
        if (xpos > 6.5f) xpos = 6.5f;
        if (xpos < -6.5) xpos = -6.5f;

        transform.position = new Vector3(xpos, transform.position.y, transform.position.z);
	}
}
