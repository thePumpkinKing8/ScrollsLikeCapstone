using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DungeonLevelLoader : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public GameObject roofPrefab;
    public GameObject groundPrefab; 
    public LevelData levelData;

    void Start()
    {
        LoadLevel();
        InstantiateGround();
        InstantiateObjects();
        InstantiateRoof();
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

    void InstantiateGround()
    {
        for (int i = 0; i < levelData.levelWidth; i++)
        {
            for (int j = 0; j < levelData.levelHeight; j++)
            {
                Vector3 groundPosition = new Vector3(i, 0, j);
                Instantiate(groundPrefab, groundPosition, Quaternion.identity);
            }
        }
    }

    void InstantiateObjects()
    {
        for (int i = 0; i < levelData.levelWidth; i++)
        {
            for (int j = 0; j < levelData.levelHeight; j++)
            {
                int objectType = levelData.grid[i, j]; // The grid value
                if (objectType == 3)
                {
                    Vector3 spawnPosition = new Vector3(i, 1, j);
                    // Instantiate the object 
                    Instantiate(objectPrefabs[objectType], spawnPosition, Quaternion.identity);
                }
                else if (objectType != -1) // -1 is an empty space
                {
                    Vector3 spawnPosition = new Vector3(i, 0, j); 
                    // Instantiate the object 
                    Instantiate(objectPrefabs[objectType], spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    void InstantiateRoof()
    {
        int roofHeight = 3;
        for (int i = 0; i < levelData.levelWidth; i++)
        {
            for (int j = 0; j < levelData.levelHeight; j++)
            {
                Vector3 roofPosition = new Vector3(i, roofHeight, j);
                Instantiate(roofPrefab, roofPosition, Quaternion.identity);
            }
        }
    }
}
