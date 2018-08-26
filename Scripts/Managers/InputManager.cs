using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour
{
    
    [Header("====== KeyName ======")]
    public string attack1KeyName;
    public string attack2KeyName;

    public bool attack1;
    public bool attack2;
         



    public bool onlyHasInput = false;
    public bool readInput = false;

    public bool fire1, fire2, fire3, fire4, _switch,roll;
    public Dictionary<string, bool> inputBoolDic = new Dictionary<string, bool>();




    public float horizontal;
    public float vertical;
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
       
        inputBoolDic.Add("Fire1", fire1);
        inputBoolDic.Add("Fire2", fire2);
        inputBoolDic.Add("Fire3", fire3);
        inputBoolDic.Add("Fire4", fire4);
        inputBoolDic.Add("Switch", _switch);
        inputBoolDic.Add("Roll", roll);
    }

    private void LateUpdate()
    {
        fire1=inputBoolDic["Fire1"] = false;
        fire2=inputBoolDic["Fire2"] = false;
        fire3=inputBoolDic["Fire3"] = false;
        fire4=inputBoolDic["Fire4"] = false;
        _switch=inputBoolDic["Switch"] = false;
        roll=inputBoolDic["Roll"] = false;
    }

  
    private void Update()
    {

       
        if (readInput)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                fire1=inputBoolDic["Fire1"] = true;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                fire2=inputBoolDic["Fire2"] = true;

            }
            if (Input.GetButtonDown("Fire3"))
            {
                fire3=inputBoolDic["Fire3"] = true;

            }
            if (Input.GetButtonDown("Fire4"))
            {
                fire4=inputBoolDic["Fire4"] = true;

            }
            if (Input.GetButtonDown("Switch"))
            {
                _switch=inputBoolDic["Switch"] = true;
            }
            if (Input.GetButtonDown("Roll"))
            {
                roll = inputBoolDic["Roll"] = true;
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

    public List<string> GetPressingButtonNames()
    {
        var queryResult = from paire in inputBoolDic
                          where paire.Value == true
                          select paire.Key;

        return queryResult.ToList<string>();
    }

}
