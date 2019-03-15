

//Include code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is Generate code
class Select_1View : MonoBehaviour {

    /// cached gameObject arrays
    protected GameObject[] arrs = new GameObject[8];

// Generate Code:
    public GameObject child_1 { get{ return arrs[0];} }
    public GameObject child_1_1 { get{ return arrs[1];} }
    public GameObject child_2 { get{ return arrs[2];} }
    public GameObject child_2_1 { get{ return arrs[3];} }
    public GameObject child_2_2 { get{ return arrs[4];} }
    public GameObject child_2_2_1 { get{ return arrs[5];} }
    public GameObject child_2_2_2 { get{ return arrs[6];} }
    public GameObject child_2_2_3 { get{ return arrs[7];} }

    void Awake()
    {
   
    arrs[0] = transform.Find("child_1").gameObject;
   
    arrs[1] = transform.Find("child_1/child_1_1").gameObject;
   
    arrs[2] = transform.Find("child_2").gameObject;
   
    arrs[3] = transform.Find("child_2/child_2_1").gameObject;
   
    arrs[4] = transform.Find("child_2/child_2_2").gameObject;
   
    arrs[5] = transform.Find("child_2/child_2_2/child_2_2_1").gameObject;
   
    arrs[6] = transform.Find("child_2/child_2_2/child_2_2_2").gameObject;
   
    arrs[7] = transform.Find("child_2/child_2_2/child_2_2_3").gameObject;
    }
}
