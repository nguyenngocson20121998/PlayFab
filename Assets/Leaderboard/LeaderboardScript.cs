using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class LeaderboardScript : MonoBehaviour {

    [SerializeField]
    GameObject Rank, Pseudo, Score;
    PlayFabManager playFabManager;

    private void Awake()
    {
        playFabManager = GameObject.Find("PlayFabManager").GetComponent<PlayFabManager>();
        playFabManager.LoadingMessage("Loading LeaderBoard...");
        ReadLeaderBoard();
    }

    void ReadLeaderBoard()
    {
        var request = new GetLeaderboardRequest()
        {
            StatisticName = "score",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, LeaderBoardSuccess, LeaderBoardFailed);
    }

    private void LeaderBoardFailed(PlayFabError err)
    {
        playFabManager.LoadingMessage(err.ErrorMessage);
        playFabManager.LoadingHide();
    }

    private void LeaderBoardSuccess(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            GameObject GoRank = Instantiate(Rank, this.transform);
            GoRank.GetComponent<Text>().text = (item.Position + 1).ToString();

            GameObject GoPseudo = Instantiate(Pseudo, this.transform);
            GoPseudo.GetComponent<Text>().text = item.DisplayName;

            GameObject GoScore = Instantiate(Score, this.transform);
            GoScore.GetComponent<Text>().text = item.StatValue.ToString();
        }

        playFabManager.LoadingHide();
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }   
}
