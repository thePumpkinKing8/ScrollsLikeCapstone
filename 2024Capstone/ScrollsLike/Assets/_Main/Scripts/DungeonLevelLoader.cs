using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DungeonLevelLoader : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private LevelData levelData;

    void Start()
    {
        LoadLevel();
        InstantiateObjects();
    }

    void LoadLevel()
    {
        string path = "Assets/Level.txt";
        if (File.Exists(path))
        {
            string levelJson = File.ReadAllText(path);
            levelData = JsonConvert.DeserializeObject<LevelData>(levelJson);
        }
        else
        {
            Debug.LogError("Level file not found at: " + path);
        }
    }

    void InstantiateObjects()
    {
        for (int i = 0; i < levelData.levelWidth; i++)
        {
            for (int j = 0; j < levelData.levelHeight; j++)
            {
                int objectType = levelData.grid[i, j]; // The grid value
                if (objectType != -1) // -1 is an empty space
                {
                    Vector3 spawnPosition = new Vector3(i, 0, j); // Adjust per game grid size
                    // Instantiate the object 
                    Instantiate(objectPrefabs[objectType], spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
