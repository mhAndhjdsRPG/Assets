using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [Range(0,1)]
	public float value;

    public bool useValue;

    Animator ani;

    private void Start()
    {
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (useValue)
        {
            ani.CrossFade(ani.GetCurrentAnimatorStateInfo(0).fullPathHash, 0,0,value);
        }

    }
}
