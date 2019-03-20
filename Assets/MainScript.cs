using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {
    public GameObject select_1;
	// Use this for initialization
	void Start () {
        var comp = select_1.GetComponent<Select_1View>();
        comp.child_1.name = "child_1 name changed";
        comp.child_2_2_1.name = "child_2_2_1 name changed";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
