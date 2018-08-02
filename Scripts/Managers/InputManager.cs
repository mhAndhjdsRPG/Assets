using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public bool onlyHasInput = false;
    public bool readInput = false;

    public bool fire1, fire2, fire3, fire4, _switch, roll;
    public Dictionary<string, bool> inputBoolDic = new Dictionary<string, bool>();



    [SerializeField]
    private float horizontal;
    [SerializeField]
    private float vertical;
    public float Horizontal
    {
        get
        {
            return horizontal;
        }

        set
        {
            horizontal = value;
        }
    }

    public float Vertical
    {
        get
        {
            return vertical;
        }

        set
        {
            vertical = value;
        }
    }



    private void Awake()
    {
        inputBoolDic.Add("fire1", fire1);
        inputBoolDic.Add("fire2", fire2);
        inputBoolDic.Add("fire3", fire3);
        inputBoolDic.Add("fire4", fire4);
        inputBoolDic.Add("roll", roll);
        inputBoolDic.Add("switch", _switch);

    }

    private void LateUpdate()
    {
        List<string> strList = new List<string>(inputBoolDic.Keys);
        foreach (string keyName in strList)
        {
            inputBoolDic[keyName] = false;
        }
    }

    private void Update()
    {
        if (readInput)
        {
            List<string> strList = new List<string>(inputBoolDic.Keys);
            foreach (string buttonName in strList)
            {
                if (Input.GetButtonDown(buttonName))
                {
                    inputBoolDic[buttonName] = true;
                }
            }


            if (onlyHasInput)
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    Horizontal = Input.GetAxis("Horizontal");
                }
                if (Input.GetAxis("Vertical") != 0)
                {
                    Vertical = Input.GetAxis("Vertical");
                }
            }
            else
            {
                Horizontal = Input.GetAxis("Horizontal");
                Vertical = Input.GetAxis("Vertical");
            }
        }

    }



}
