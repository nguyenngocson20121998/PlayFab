using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
       

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
}
