using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour {

    public static PlayFabManager instance = null;

    Text txtMessage;
    GameObject panel;

    [SerializeField]
    int LoadingTimeOut = 3;

    public string Player_ID, Player_DisplayName, Player_Car;    
    public int Player_Score, Player_Coin;

    void Awake () {

        //Singleton
        if(instance==null)
        {
            instance = this;
        }
        else if(instance !=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


        panel = transform.Find("CanvasLoading").Find("Panel").gameObject;
        txtMessage = panel.GetComponentInChildren<Text>();
    }
	
	
	public void LoadingShow () {
		
        if(!panel.activeInHierarchy)
        {
            panel.SetActive(true);
        }
	}

    public void LoadingHide()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(LoadingTimeOut);
        panel.SetActive(false);
    }

    public void LoadingMessage(string msg)
    {
        LoadingShow();
        txtMessage.text = msg;
    }

}
