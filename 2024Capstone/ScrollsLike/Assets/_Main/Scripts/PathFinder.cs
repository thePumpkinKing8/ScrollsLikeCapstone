using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile
{
    public bool isWalkable;
    public Vector2Int pos;
    public Tile previousTile;
}
public class PathFinder : Singleton<PathFinder>
{
    private Tile[,] tiles;
    private Queue<Tile> frontier;
    private List<Tile> surroundingTiles;
    public void SetGrid(int[,] grid)
    {
        tiles = new Tile[grid.GetLength(0), grid.GetLength(1)];
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                Tile t = new Tile(); ;
                t.pos = new Vector2Int(i, j);

                //1 = wall, 
                t.isWalkable = grid[i, j] != 1;

                tiles[i, j] = t;
            }
        }
    }

    public Stack<Vector2Int> GetPath(Vector2Int startPos, Vector2Int destination)
    {
        //since this is a new path, let's clear all of the previous tile values for a search
        foreach (Tile t in tiles)
        {
            t.previousTile = null;
        }
        Stack<Vector2Int> path = new Stack<Vector2Int>();
        Tile startTile = tiles[startPos.x, startPos.y];
        frontier = new Queue<Tile>();
        frontier.Enqueue(startTile);
        while (frontier.Count != 0)
        {
            Tile current = frontier.Dequeue();
            if (current.pos == destination)
            {
                while (current != startTile && current != null)
                {
                    path.Push(current.pos);
                    current = current.previousTile;
                }
                return path;
            }
            AddSurroundingTiles(current);
        }
        //if you have found no path, then return
        return path;
    }

    private void AddSurroundingTiles(Tile origin)
    {
        AddTile(origin, 1, 0);
        AddTile(origin, 0, 1);
        AddTile(origin, -1, 0);
        AddTile(origin, 0, -1);
    }
    private void AddTile(Tile origin, int dirX, int dirY)
    {
        int tileX = origin.pos.x + dirX;
        int tileY = origin.pos.y + dirY;
        if (tileX < 0 || tileX >= tiles.GetLength(0) || tileY < 0 || tileY >= tiles.GetLength(1)) //if out of bounds return
            return;
        Tile nextTile = tiles[tileX, tileY]; //grab the current tile

        if (nextTile.previousTile != null || !nextTile.isWalkable)
            return;
        nextTile.previousTile = origin;

        frontier.Enqueue(nextTile);
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
