using UnityEngine;
using System.IO;

public class SaveController : MonoBehaviour
{
    private string saveLocation;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData(); // Corrected object initialization
        saveData.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        string json = JsonUtility.ToJson(saveData, true); // Pretty print JSON
        File.WriteAllText(saveLocation, json);

        Debug.Log($"Game Saved at {saveLocation}");
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            string json = File.ReadAllText(saveLocation);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = saveData.playerPosition;
                Debug.Log("Game Loaded: Player position set.");
            }
            else
            {
                Debug.LogError("Player object not found!");
            }
        }
        else
        {
            Debug.LogWarning("No save file found. Creating new save.");
            SaveGame();
        }
    }
}
