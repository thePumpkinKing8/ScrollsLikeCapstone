using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    bool mouseDown;
    float gridSize = 40f;
    float leftPadding = 10;
    float gridPadding = 2;
    int currentOption = 1;

    Color[] options = { Color.black, Color.white, Color.red, Color.blue, Color.green };
    string[] names = { "Blank", "Wall", "Boss", "Player", "Patrol Point" };

    private LevelData myData;
    private string levelFileName = "Level.txt";

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Editor", EditorStyles.boldLabel);

        // myData is initialized before usage
        if (myData == null)
        {
            LoadLevelData();
            if (myData == null)
            {
                myData = new LevelData();
                myData.levelWidth = 10;
                myData.levelHeight = 10;
                myData.grid = new int[myData.levelWidth, myData.levelHeight];
                myData.patrolPoints = new List<Vector2Int>();
            }
        }

        myData.levelWidth = EditorGUILayout.IntField("Level Width", myData.levelWidth);
        myData.levelHeight = EditorGUILayout.IntField("Level Height", myData.levelHeight);

        levelFileName = EditorGUILayout.TextField("Level File Name", levelFileName);

        // Reset button
        if (GUILayout.Button("Reset"))
        {
            if (EditorUtility.DisplayDialog("Reset", "Warning. This will clear your level. Are you sure?", "Yes", "...no"))
            {
                myData.grid = new int[myData.levelWidth, myData.levelHeight];
                myData.patrolPoints.Clear();
            }
        }

        Event e = Event.current;
        mouseDown = e.type == EventType.MouseDown && e.button == 0;

        // Draw object options
        for (int i = 0; i < options.Length; i++)
        {
            Rect r = new Rect(leftPadding + i * (gridSize + gridPadding), 140, gridSize, gridSize);

            if (r.Contains(e.mousePosition))
            {
                GUILayout.Label(names[i]);

                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    currentOption = i;
                }

                if (currentOption == i)
                {
                    EditorGUI.DrawRect(new Rect(r.x - 1, r.y - 1, r.width + 2, r.height + 2), Color.red);
                }
            }
            EditorGUI.DrawRect(r, options[i]);
        }

        // Ensure grid is initialized
        if (myData.grid == null || myData.grid.GetLength(0) != myData.levelWidth || myData.grid.GetLength(1) != myData.levelHeight)
        {
            myData.grid = new int[myData.levelWidth, myData.levelHeight];
        }

        for (int i = 0; i < myData.grid.GetLength(0); i++)
        {
            for (int j = 0; j < myData.grid.GetLength(1); j++)
            {
                Rect r = new Rect(leftPadding + i * (gridSize + gridPadding), 180 + (myData.grid.GetLength(1) - j) * (gridSize + gridPadding), gridSize, gridSize);

                if (mouseDown && r.Contains(e.mousePosition))
                {
                    if (currentOption == 4) // Patrol Point
                    {
                        Vector2Int patrolPoint = new Vector2Int(i, j);

                        // Toggle patrol point: add if it doesn’t exist, remove if it does
                        if (myData.patrolPoints.Contains(patrolPoint))
                        {
                            myData.patrolPoints.Remove(patrolPoint);
                        }
                        else
                        {
                            myData.patrolPoints.Add(patrolPoint);
                        }
                    }
                    else
                    {
                        myData.grid[i, j] = currentOption;
                    }
                }

                Color cellColor = options[myData.grid[i, j]];
                if (myData.patrolPoints.Contains(new Vector2Int(i, j)))
                {
                    cellColor = Color.green; 
                }

                EditorGUI.DrawRect(r, cellColor);
            }
        }

        // Save button
        if (GUILayout.Button("Save"))
        {
            SaveLevelData();
        }
    }

    void Update()
    {
        this.Repaint();
    }

    private void LoadLevelData()
    {
        if (File.Exists("Assets/" + levelFileName))
        {
            string myDataString = File.ReadAllText("Assets/" + levelFileName);
            myData = JsonConvert.DeserializeObject<LevelData>(myDataString);
        }
    }

    private void SaveLevelData()
    {
        string myDataString = JsonConvert.SerializeObject(myData);
        File.WriteAllText("Assets/" + levelFileName, myDataString);
    }
}
