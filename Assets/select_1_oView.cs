using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_1_oView : MonoBehaviour {
    private void Awake()
    {
        var sel1 = transform.Find("child_1");
        
        if(sel1 == null)
        {
            Debug.Log("not found");
        }
    }
}
