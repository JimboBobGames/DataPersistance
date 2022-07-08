using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public TextMeshProUGUI userNameText;
    public string userName;
    public int highScore;
    public string highScoreName;
    private void Awake()
    {
	LoadData();
        if (Instance != null)
	{
	    Destroy(gameObject);
	    return;
        }
	Instance = this; 
	DontDestroyOnLoad(gameObject);
    }
    public void changeUserName()
    {
	userName = userNameText.text;
    }
    void Update()
    {
	SaveHighScore();
    }
    [System.Serializable]
    class SaveData
    {
	public int highScore;
	public string highScoreName;
    }
    public void SaveHighScore()
    {
	SaveData data = new SaveData();
	data.highScore = highScore;
	data.highScoreName = highScoreName;

	string json = JsonUtility.ToJson(data);

	File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadData()
    {
	string path = Application.persistentDataPath + "/savefile.json";
	if (File.Exists(path))
	{
	    string json = File.ReadAllText(path);
	    SaveData data = JsonUtility.FromJson<SaveData>(json);
	    
	    highScore = data.highScore;
	    highScoreName = data.highScoreName;
	}
    }
}
