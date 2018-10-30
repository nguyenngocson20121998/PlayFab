using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RememberToggle : MonoBehaviour {

    private void Awake()
    {
        if(PlayerPrefs.GetInt("Toggle")>0)
        {
            GetComponent<Toggle>().isOn = true;
        }
        else
        {
            GetComponent<Toggle>().isOn = false;
        }

    }


    public void SaveChange () {
		
        if(GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("Toggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Toggle", 0);
        }
	}
}
