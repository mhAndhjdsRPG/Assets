using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
public class MainMenuScrollView : MonoBehaviour {
    private ScrollView scrollView;
    private Animator cameraAnimator;

    private void Awake()
    {
        scrollView = GetComponent<ScrollView>();
        cameraAnimator = Camera.main.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnContentPosChange(Vector2 vector2)
    {
        
    }
}
