using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	void Start () {
        Destroy(gameObject, 1);
	}
}
