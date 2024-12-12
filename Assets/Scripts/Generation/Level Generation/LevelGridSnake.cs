using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;

//Snake Class for generating a level grid
public class LevelGridSnake : MonoBehaviour
{
    Vector2Int coords;
    int gridWidth, gridHeight;
    float chanceToSpawn = 0.1f;
    float chanceToDie = 0.1f;

    RandomNumber rng;

    public LevelGridSnake(int width, int height, int spawnX, int spawnY, ref RandomNumber rng)
    {
        gridWidth = width;
        gridHeight = height;
        coords = new Vector2Int(spawnX, spawnY);
        this.rng = rng;
    }

    public void SetCoords(int x, int y)
    {
        this.coords = new Vector2Int(x, y);
    }
    public Vector2Int MoveRight()
    {
        int x = coords.x + 1;
        if (x >= gridWidth)
        {
            return new Vector2Int(gridWidth - 1, coords.y);
        }

        return new Vector2Int(x, coords.y);
    }

    public void MoveLeft()
    {
        int x = coords.x - 1;
        if (x < 0)
        {
            coords = new Vector2Int(0, coords.y);
        }
        else
        {
            coords = new Vector2Int(x, coords.y);
        }
    }

    public void MoveUp()
    {
        int y = coords.y + 1;
        if (y >= gridHeight)
        {
            coords = new Vector2Int(coords.x, gridHeight - 1);
        }
        else
        {
            coords = new Vector2Int(coords.x, y);
        }
    }

    public void MoveDown()
    {
        int y = coords.y - 1;
        if (y < 0)
        {
            coords = new Vector2Int(coords.x, 0);
        }
        else
        {
            coords = new Vector2Int(coords.x, y);
        }
    }

    public void MoveRandom()
    {
        int direction = rng.RandomInt(0, 4);
        switch (direction)
        {
            case 0:
                MoveRight();
                Debug.Log("|| MOVED RIGHT: " + coords.x + ", " + coords.y + " ||");
                break;
            case 1:
                MoveLeft();
                Debug.Log("|| MOVED LEFT: " + coords.x + ", " + coords.y + " ||");
                break;
            case 2:
                MoveUp();
                Debug.Log("|| MOVED UP: " + coords.x + ", " + coords.y + " ||");
                break;
            case 3:
                MoveDown();
                Debug.Log("|| MOVED DOWN: " + coords.x + ", " + coords.y + " ||");
                break;
        }
    }
}
