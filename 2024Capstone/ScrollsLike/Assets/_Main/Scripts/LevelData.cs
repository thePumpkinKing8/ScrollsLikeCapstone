using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int levelWidth;
    public int levelHeight;
    public int[,] grid;
    public List<Vector2Int> patrolPoints = new List<Vector2Int>();
}
