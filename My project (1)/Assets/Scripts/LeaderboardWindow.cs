using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardWindow : MonoBehaviour
{
    public GameObject scoreBoard;
    public GameObject playerScoreList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowLeaderboard()
    {
        scoreBoard.SetActive(!scoreBoard.activeSelf);
        playerScoreList.GetComponent<PlayerScoreList>().SpawnLeaderboard();
    }
    
}
