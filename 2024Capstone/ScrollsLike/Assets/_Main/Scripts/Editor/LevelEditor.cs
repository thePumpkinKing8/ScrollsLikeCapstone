using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class LevelEditor : EditorWindow
{
    bool mouseDown;
    float gridSize = 40f;
    float leftPadding = 10;
    float gridPadding = 2;
    int currentOption = 1;

    Color[] options =
    {
        Color.black,
        Color.white,
        Color.red,
        Color.blue
    };

    string[] names =
    {
        "Blank",
        "Wall",
        "Boss",
        "Player"
    };

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

        if (myData == null)
        {
            LoadLevelData();
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

        for (int i = 0; i < myData.grid.GetLength(0); i++)
        {
            for (int j = 0; j < myData.grid.GetLength(1); j++)
            {
                Rect r = new Rect(leftPadding + i * (gridSize + gridPadding), 180 + (myData.grid.GetLength(1) - j) * (gridSize + gridPadding), gridSize, gridSize);
                if (mouseDown && r.Contains(e.mousePosition))
                {
                    myData.grid[i, j] = currentOption;
                }
                int objectType = myData.grid[i, j];
                EditorGUI.DrawRect(r, options[objectType]);
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
        else
        {
            myData = new LevelData();
            myData.grid = new int[myData.levelWidth, myData.levelHeight];
        }
    }

    private void SaveLevelData()
    {
        string myDataString = JsonConvert.SerializeObject(myData);
        File.WriteAllText("Assets/" + levelFileName, myDataString);
    }
}
