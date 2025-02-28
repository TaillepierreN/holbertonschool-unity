using UnityEngine;
using System.IO;

[System.Serializable]
public class Datasave
{
    public int Highscore = 0;

    public void SetHighscore(int score)
    {
        if (score > Highscore)
        {
            Highscore = score;
            SaveData();
        }
    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Datasave loadedData = JsonUtility.FromJson<Datasave>(json);
            Highscore = loadedData.Highscore; 
        }
    }
}
