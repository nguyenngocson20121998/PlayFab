using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RememberScript : MonoBehaviour {

    Toggle remenberToggle;

	void Awake () {
        remenberToggle = GameObject.Find("Toggle").GetComponent<Toggle>();

        if(remenberToggle.isOn)
        {
            gameObject.GetComponent<InputField>().text = PlayerPrefs.GetString(gameObject.name);
        }
	}
	
	
	public void SaveChange () {
		
        if(remenberToggle.isOn)
        {
            PlayerPrefs.SetString(gameObject.name, transform.Find("Text").GetComponent<Text>().text);
        }
	}
}
