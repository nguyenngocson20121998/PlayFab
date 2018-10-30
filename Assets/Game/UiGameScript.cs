using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class UiGameScript : MonoBehaviour {

    [SerializeField] Text txtCoins, txtScore;
    PlayFabManager playFabManager;
    private int InitCoins;

    private int coins;
    public int Coins
    {
        get { return coins; }

        set
        { 
            coins = value;
            txtCoins.text = coins.ToString(); 
        }
    }

    private int score;
    public int Score
    {
        get { return score; }

        set
        {
            score = value;
            txtScore.text = score.ToString();
        }
    }

    private void FixedUpdate()
    {
        if(Input.anyKey)
        {
            Score++;
        }
    }

    private void Awake()
    {
        playFabManager = GameObject.Find("PlayFabManager").GetComponent<PlayFabManager>();
        Score = playFabManager.Player_Score;
        Coins = playFabManager.Player_Coin;
        InitCoins = Coins;
    }

    private void UpdatePlayFabManagerInfo()
    {
        playFabManager.Player_Score = Score;
        playFabManager.Player_Coin = Coins;
    }


    public void Home()
    {
        //Update Score
        playFabManager.LoadingMessage("Updating data...");

        UpdatePlayFabManagerInfo();

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
               new StatisticUpdate {StatisticName ="score", Value=Score}
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, SuccessUpdate, FailedUpdate);        
       
    }

    private void SuccessUpdate(UpdatePlayerStatisticsResult result)
    {
        UpdateCoins();
      
    }

    private void FailedUpdate(PlayFabError err)
    {
        playFabManager.LoadingMessage(err.ErrorMessage);
        playFabManager.LoadingHide();

    }

    void UpdateCoins()
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.VirtualCurrency = "CO";
        request.Amount = Coins - InitCoins;

        PlayFabClientAPI.AddUserVirtualCurrency(request, UpdateCoinsSuccess, UpdateCoinsFailed);

    }

    private void UpdateCoinsFailed(PlayFabError err)
    {
        playFabManager.LoadingMessage(err.ErrorMessage);
        playFabManager.LoadingHide();
    }

    private void UpdateCoinsSuccess(ModifyUserVirtualCurrencyResult obj)
    {
        playFabManager.LoadingHide();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
