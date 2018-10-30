using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour {
    [SerializeField]
    float speed=300f;

    [SerializeField] UiGameScript uiGameScript;

	void Update () {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
	}

    private void OnTriggerStay()
    {
        uiGameScript.Coins ++;
    }

   

}
