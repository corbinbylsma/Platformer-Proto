using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	void OnTriggerEnter() {
        DoorMove.buttonPressed = true;
        transform.position = new Vector3(transform.position.x, transform.position.y - .25f, transform.position.z);
	}
}
