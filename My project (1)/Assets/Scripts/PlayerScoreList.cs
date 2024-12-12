using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class PlayerScoreList : MonoBehaviour
{
    public GameObject playerScoreEntryPrefab;
    public LeaderboardManager leaderboardManager;
    // Start is called before the first frame update
    void Start()
    {
        //leaderboardManagerScript = GameObject.Find("LeaderboardManager").GetComponent<LeaderboardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnLeaderboard()
    {
        while(this.transform.childCount>0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);
            Destroy (c.gameObject);
        }

        string[] names = leaderboardManager.GetComponent<LeaderboardManager>().GetPlayerNames("Score");
        int loops = 0;
        foreach(string name in names)
        {
            loops += 1;
            GameObject go = Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("Username").GetComponent<TMP_Text>().text = name;
            go.transform.Find("Score").GetComponent<TMP_Text>().text = leaderboardManager.GetComponent<LeaderboardManager>().GetScore(name, "Score").ToString();
            if (loops>=5)
            {
                break;
            }
        }
    }
}
