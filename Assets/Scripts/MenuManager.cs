using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public TextMeshProUGUI nameInputField;
    public TextMeshProUGUI bestScoreText;
    public string playerName;
    public string newPlayerName;
    public int bestScore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void LoadScene()
    {
        
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StoreName()
    {
        newPlayerName = nameInputField.text;
    }

    [SerializeField]
    class StorePlayerData
    {
        public string playerName;
        public int score;
    }

    public void SavePlayerData(int score)
    {
        StorePlayerData data = new StorePlayerData();
        data.playerName = playerName;
        data.score = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            StorePlayerData data = JsonUtility.FromJson<StorePlayerData>(json);

            playerName = data.playerName;
            bestScore = data.score;

            ShowBestScore();
        }
    }

    void ShowBestScore()
    {
        bestScoreText.text = "Best Score : " + playerName + " : " + bestScore;
    }
}
