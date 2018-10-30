using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.Advertisements;

public class ShopScript : MonoBehaviour {

    [SerializeField] Text txtCoins;
    PlayFabManager playFabMananger;
    private int carAmount = 0;

	void Awake () {
        playFabMananger = GameObject.Find("PlayFabManager").GetComponent<PlayFabManager>();
        txtCoins.text = playFabMananger.Player_Coin.ToString();
    }
	
	
	public void RedCar () {
		
        if(playFabMananger.Player_Coin >=5000)
        {
            playFabMananger.Player_Coin -= 5000;
            txtCoins.text = playFabMananger.Player_Coin.ToString();
            //Playfab Sync Data
            PlayerDataTitle("car", "rouge");
            carAmount = 5000;
        }
        else
        {
            playFabMananger.LoadingMessage("Not enough Coins...");
            playFabMananger.LoadingHide();
        }
	}

    public void GreenCar()
    {

        if (playFabMananger.Player_Coin >= 2500)
        {
            playFabMananger.Player_Coin -= 2500;
            txtCoins.text = playFabMananger.Player_Coin.ToString();
            //Playfab Sync Data
            PlayerDataTitle("car", "vert");
            carAmount = 2500; 
        }
        else
        {
            playFabMananger.LoadingMessage("Not enough Coins...");
            playFabMananger.LoadingHide();
        }
    }

    void PlayerDataTitle(string key, string keyValue)
    {
        playFabMananger.Player_Car = keyValue;

        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {key,keyValue }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, DataSuccess, DataFailed);
    }

    private void DataFailed(PlayFabError err)
    {
        playFabMananger.LoadingMessage(err.ErrorMessage);
        playFabMananger.LoadingHide();
    }

    private void DataSuccess(UpdateUserDataResult result)
    {
        playFabMananger.LoadingMessage("Updating Car Data...");
        playFabMananger.LoadingHide();
        SubstractVC(carAmount);
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    void SubstractVC(int amount)
    {    

        var request = new SubtractUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "CO",
            Amount = amount
        };

        PlayFabClientAPI.SubtractUserVirtualCurrency(request, SubSuccess, SubFailed);
        
    }

    private void SubFailed(PlayFabError err)
    {
        playFabMananger.LoadingMessage(err.ErrorMessage);
        playFabMananger.LoadingHide();
    }

    private void SubSuccess(ModifyUserVirtualCurrencyResult result)
    {
        playFabMananger.Player_Coin = result.Balance;
        txtCoins.text = playFabMananger.Player_Coin.ToString();
        playFabMananger.LoadingMessage("Updating Virtual Currency...");
        playFabMananger.LoadingHide();
    }

    // ADS

    // public void ShowRewardedAd()
    // {
    //     if(Advertisement.IsReady("rewardedVideo"))
    //     {
    //         var options = new ShowOptions
    //         {
    //             resultCallback = HandleShowResult
    //         };
          
    //         Advertisement.Show("rewardedVideo", options);

    //     }
    // }

    // private void HandleShowResult(ShowResult result)
    // {
    //     switch (result)
    //     {
    //         case ShowResult.Finished:
    //             //Ajouter des Coins
    //             playFabMananger.LoadingMessage("Updating data...");
    //             playFabMananger.Player_Coin += 500;
    //             txtCoins.text = playFabMananger.Player_Coin.ToString();
    //             AddCoins();
    //             //Update de VC sur Playfab
    //             break;

    //         case ShowResult.Skipped:
    //             Debug.Log("...");
    //             break;

    //         case ShowResult.Failed:
    //             Debug.Log("...");
    //             break;
    //     }
    // }

    void AddCoins()
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "CO",
            Amount = 500
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, AddSuccess, AddFailed);
    }

    private void AddFailed(PlayFabError err)
    {
        playFabMananger.LoadingMessage(err.ErrorMessage);
        playFabMananger.LoadingHide();
    }

    private void AddSuccess(ModifyUserVirtualCurrencyResult result)
    {
        playFabMananger.LoadingMessage("Updating Successfull");
        playFabMananger.LoadingHide();
    }
}
