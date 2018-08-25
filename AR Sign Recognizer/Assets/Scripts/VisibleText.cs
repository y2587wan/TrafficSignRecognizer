using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleText : MonoBehaviour {
    BoxCollider selfCollider;
	void Start () {
        selfCollider = GetComponent<BoxCollider>();
        var child = transform.GetChild(0);
        child.gameObject.SetActive(selfCollider.enabled);
    }
	
	// Update is called once per frame
	void Update () {
        var child = transform.GetChild(0);
        child.gameObject.SetActive(selfCollider.enabled);
    }
}
