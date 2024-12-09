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
                Instantiate(groundPrefab, groundPosition, Quaternion.identity, transform);
            }
        }
    }

    void InstantiateObjects()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < levelData.levelWidth; i++)
        {
            for (int j = 0; j < levelData.levelHeight; j++)
            {
                int objectType = levelData.grid[i, j];
                if (objectType < 0 || objectType >= objectPrefabs.Length || objectPrefabs[objectType] == null) continue;

                Vector3 spawnPosition = new Vector3(i, 0, j);

                if (objectType == 2) // Player
                {
                    if (player != null)
                    {
                        player.transform.position = spawnPosition + Vector3.up * 1;
                    }
                    else
                    {
                        player = Instantiate(objectPrefabs[objectType], spawnPosition + Vector3.up * 1, Quaternion.identity);
                        player.tag = "Player";
                    }
                }
                else
                {
                    float yOffset = 0f; // Default offset for most objects

                    // Raise treasure chest a bit
                    if (objectType == 8)
                    {
                        yOffset = 0.5f; 
                    }
                    else if (objectType == 1) // Raise walls slightly
                    {
                        yOffset = 0.5f;
                    }

                    Instantiate(objectPrefabs[objectType], spawnPosition + Vector3.up * yOffset, Quaternion.identity, transform);
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
                Instantiate(roofPrefab, roofPosition, Quaternion.identity, transform);
            }
        }
    }

    public void LoadNextLevel()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // Destroy all objects from the current level
        }

        currentLevelIndex++;
        if (currentLevelIndex < levelFiles.Length)
        {
            LoadLevel(levelFiles[currentLevelIndex]);
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }
}
