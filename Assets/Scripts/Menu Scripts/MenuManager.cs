using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI BestScoreText;

    public static MenuManager Instance;
    public string Name;
    public int BestScore;
    public string BestScoreName;

    public TMP_InputField inputField;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        BestScoreText.text = "Best Score: " + BestScoreName + " : " + BestScore;
    }

    public void StartNew()
    {
        Name = inputField.text;
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

    [System.Serializable]
    class SaveData 
    {    
        public string BestScoreName;
        public int BestScore;
    }

    public void SaveHighScore(string name, int score)
    {
        SaveData data = new SaveData();
        data.BestScore = score;
        data.BestScoreName = name;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScore = data.BestScore;
            BestScoreName = data.BestScoreName;
        }
    }

}
