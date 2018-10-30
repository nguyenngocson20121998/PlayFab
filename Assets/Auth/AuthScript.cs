using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using System;

public class AuthScript : MonoBehaviour {

    [SerializeField]
    InputField ifEmail, ifPassword, ifDisplayName;

    [SerializeField]
    PlayFabManager playFabmanager;   

	public void RegisterPlayer()
    {
        playFabmanager.LoadingMessage("Connecting server...");
        var request = new RegisterPlayFabUserRequest()
        {
            Email = ifEmail.text,
            Password = ifPassword.text,
            DisplayName = ifDisplayName.text,
            Username = ifDisplayName.text
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, Success, Failed);
    }

    private void Failed(PlayFabError err)
    {
        playFabmanager.LoadingMessage(err.ErrorMessage);
        playFabmanager.LoadingHide();
    }

    private void Success(RegisterPlayFabUserResult success)
    {
        playFabmanager.LoadingMessage("Initialize Statistics...");
        InitStat();

    }

    private void InitStat()
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {StatisticName="score", Value=0}
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, InitStatSuccess, IniStatFailed);

    }

    private void IniStatFailed(PlayFabError err)
    {
        playFabmanager.LoadingMessage(err.ErrorMessage);
        playFabmanager.LoadingHide();
    }

    private void InitStatSuccess(UpdatePlayerStatisticsResult result)
    {
        playFabmanager.LoadingMessage("Register Success");
        playFabmanager.LoadingHide();
    }

    public void LoginPlayer()
    {
        playFabmanager.LoadingMessage("Connecting server...");

        var request = new LoginWithEmailAddressRequest()
        {
            Password = ifPassword.text,
            Email = ifEmail.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailed);
    }

    private void LoginFailed(PlayFabError err)
    {
        playFabmanager.LoadingMessage(err.ErrorMessage);
        playFabmanager.LoadingHide();
    }

    private void LoginSuccess(LoginResult success)
    {
        playFabmanager.LoadingMessage("Login Success");
        playFabmanager.Player_ID = success.PlayFabId;
        playFabmanager.LoadingHide();
        //Get DisplayName
        GetPlayerName();
    }

    void GetPlayerName()
    {
        playFabmanager.LoadingMessage("Loading DisplayName...");
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, InfoSuccess, InfoFailed);
    }

    private void InfoFailed(PlayFabError err)
    {
        playFabmanager.LoadingMessage(err.ErrorMessage);
        playFabmanager.LoadingHide();
    }

    private void InfoSuccess(GetAccountInfoResult result)
    {
        playFabmanager.Player_DisplayName= result.AccountInfo.Username;
        //read Statistics score
        ReadStatScore();
    }

    private void ReadStatScore()
    {
        playFabmanager.LoadingMessage("Loading Statistics...");

        var request = new GetPlayerStatisticsRequest();
        PlayFabClientAPI.GetPlayerStatistics(request, SuccessStat, FailedStat);
    }

    private void FailedStat(PlayFabError err)
    {
        playFabmanager.LoadingMessage(err.ErrorMessage);
        playFabmanager.LoadingHide();
    }

    private void SuccessStat(GetPlayerStatisticsResult result)
    {
        playFabmanager.Player_Score = result.Statistics[0].Value;
        playFabmanager.LoadingMessage("Loading Profile Success...");
        playFabmanager.LoadingHide();
        GetBalance();
    }

    void GetBalance()
    {
        var request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, InventorySuccess, InventoryFailed);

    }

    private void InventorySuccess(GetUserInventoryResult result)
    {
        foreach( var item in result.VirtualCurrency)
        {
            if(item.Key=="CO")
            {
                playFabmanager.Player_Coin = item.Value;
            }
        }

        playFabmanager.LoadingMessage("Loading Currency Success");
        GetCar();
    }

    private void InventoryFailed(PlayFabError err)
    {
        playFabmanager.LoadingMessage(err.ErrorMessage);
        playFabmanager.LoadingHide();
       
    }

    void GetCar()
    {
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request,SuccessCar,FailedCar);
    }

    private void FailedCar(PlayFabError err)
    {
        playFabmanager.LoadingMessage(err.ErrorMessage);
        playFabmanager.LoadingHide();
    }

    private void SuccessCar(GetUserDataResult result)
    {
        if(result.Data==null || !result.Data.ContainsKey("car"))
        {
            playFabmanager.LoadingMessage("Login Success...");           
            playFabmanager.Player_Car = "orange";
            LoadMenu();
        }
        else if (result.Data.ContainsKey("car"))
        {
            playFabmanager.Player_Car = result.Data["car"].Value;
            playFabmanager.LoadingMessage("Login Success...");
            LoadMenu();
        }
    }

    void LoadMenu()
    {
        playFabmanager.LoadingHide();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
