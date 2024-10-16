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

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Editor", EditorStyles.boldLabel);

        // Load or initialize level data
        if (myData == null)
        {
            if (File.Exists("Assets/Level.txt"))
            {
                string myDataString = File.ReadAllText("Assets/Level.txt");
                myData = JsonConvert.DeserializeObject<LevelData>(myDataString);
            }
            else
                myData = new LevelData();
        }

        myData.levelWidth = EditorGUILayout.IntField("Level Width", myData.levelWidth);
        myData.levelHeight = EditorGUILayout.IntField("Level Height", myData.levelHeight);

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

        // Draw the grid and handle mouse input for setting object types
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
            string myDataString = JsonConvert.SerializeObject(myData);
            File.WriteAllText("Assets/Level.txt", myDataString);
        }
    }

    void Update()
    {
        this.Repaint();
    }
}
