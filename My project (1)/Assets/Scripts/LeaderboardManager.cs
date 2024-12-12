using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, int>> playerScores;
    private string saveFilePath;
    
    private PlayerController playerControllerScript;
    public GameObject leaderboardmanager;
    public GameObject usernamePromptUI; // Parent object containing Input Field and Button
    public TMP_InputField usernameInputField; // Reference to the input field
    public Button submitButton;
    private string lastUsername;

    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        LoadScores();
        if (usernamePromptUI != null)
        {
            usernamePromptUI.SetActive(false);
        }

        // Add a listener to the button
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitUsername);
        }
    }

    void Init()
    {
        if (playerScores != null)
        {
            return;
        }
        playerScores = new Dictionary<string, Dictionary<string, int>>();
    }

    public int GetScore(string username, string scoreType)
    {
        Init();

        if (playerScores.ContainsKey(username) == false)
        {
            return 0;
        }
        if (playerScores[username].ContainsKey(scoreType) == false)
        {
            return 0;
        }

        return playerScores[username][scoreType];
    }

    public void SetScore(string username, string scoreType, int value)
    {
        Init();

        if (playerScores.ContainsKey(username) == false)
        {
            playerScores[username] = new Dictionary<string, int>();
        }
        playerScores[username][scoreType] = value;
        SaveScores();
    }

    public void ChangeScore(string username, string scoreType, int amount)
    {
        Init();

        int currScore = GetScore(username, scoreType);
        SetScore(username, scoreType, currScore + amount);
    }

    public string[] GetPlayerNames2()
    {
        Init();
        return playerScores.Keys.ToArray();
    }

    public string[] GetPlayerNames(string sortingScoreType)
    {
        Init();

        return playerScores.Keys.OrderByDescending(n => GetScore(n, sortingScoreType)).ToArray();
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(new SerializableLeaderboard(playerScores), true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Leaderboard saved to " + saveFilePath);
    }

    private void LoadScores()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SerializableLeaderboard data = JsonUtility.FromJson<SerializableLeaderboard>(json);
            playerScores = data.ToDictionary();
            Debug.Log("Leaderboard loaded from " + saveFilePath);
        }
        else
        {
            Init();
            Debug.Log("No leaderboard file found. Initialized new leaderboard.");
        }
    }

    [System.Serializable]
    private class SerializableLeaderboard
    {
        public List<PlayerData> players;

        public SerializableLeaderboard(Dictionary<string, Dictionary<string, int>> scores)
        {
            players = scores.Select(kvp => new PlayerData
            {
                username = kvp.Key,
                scores = kvp.Value.Select(score => new ScoreData { key = score.Key, value = score.Value }).ToList()
            }).ToList();
        }

        public Dictionary<string, Dictionary<string, int>> ToDictionary()
        {
            return players.ToDictionary(
                p => p.username,
                p => p.scores.ToDictionary(score => score.key, score => score.value));
        }
    }

    [System.Serializable]
    private class PlayerData
    {
        public string username;
        public List<ScoreData> scores;
    }

    [System.Serializable]
    private class ScoreData
    {
        public string key;
        public int value;
    }
    public void ShowUsernamePrompt()
    {
        if (usernamePromptUI != null)
        {
            usernamePromptUI.SetActive(true); // Show the UI
        }
    }

    private void OnSubmitUsername()
    {
        Init();
        lastUsername = usernameInputField.text.Trim(); // Get the username from the Input Field

        if (!string.IsNullOrEmpty(lastUsername))
        {
            Debug.Log("Username entered: " + lastUsername);
            AddPlayerToLeaderboard(lastUsername); // Save to leaderboard
        }

        if (usernamePromptUI != null)
        {
            usernamePromptUI.SetActive(false); // Hide the UI after submission
        }
        
    }

    private void AddPlayerToLeaderboard(string username)
    {
        playerControllerScript = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
        // Example: Add a new score to the leaderboard
        SetScore(username, "Score", playerControllerScript.score);
        Debug.Log("Player added to leaderboard: " + username);
    }
    
}