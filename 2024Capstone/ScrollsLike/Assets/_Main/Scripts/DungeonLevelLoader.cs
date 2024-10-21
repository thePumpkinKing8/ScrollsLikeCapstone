using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DungeonLevelLoader : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public GameObject roofPrefab;
    public GameObject groundPrefab;
    public LevelData levelData;

    private string[] levelFiles = { "Level1.txt", "Level2.txt", "Level3.txt" };
    private int currentLevelIndex = 0; 

    void Start()
    {
        currentLevelIndex = GameManager.Instance.LevelIndex;
        LoadLevel(levelFiles[currentLevelIndex]); 
    }

    public void LoadLevel(string fileName)
    {
        string path = Path.Combine("Assets", fileName);
        if (File.Exists(path))
        {
            string levelJson = File.ReadAllText(path);
            levelData = JsonConvert.DeserializeObject<LevelData>(levelJson);
        }
        else
        {
            Debug.LogError("Level file not found at: " + path);
        }
        InstantiateGround();
        InstantiateObjects();
        InstantiateRoof();
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
                int objectType = levelData.grid[i, j]; 
                if (objectType == 3)
                {
                    Vector3 spawnPosition = new Vector3(i, 1, j);
                    Instantiate(objectPrefabs[objectType], spawnPosition, Quaternion.identity);
                }
                else if (objectType != -1) 
                {
                    Vector3 spawnPosition = new Vector3(i, 0, j);
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

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levelFiles.Length)
        {
            LoadLevel(levelFiles[currentLevelIndex]);
            InstantiateGround();
            InstantiateObjects();
            InstantiateRoof();
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }
}
