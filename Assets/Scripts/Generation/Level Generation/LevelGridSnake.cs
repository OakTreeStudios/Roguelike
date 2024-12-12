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
            return new Vector2Int(gridWidth -1, coords.y);
        }

        return new Vector2Int(x, coords.y);
    }

    public Vector2Int MoveLeft()
    {
        int x = coords.x - 1;
        if (x < 0)
        {
            return new Vector2Int(0, coords.y);
        }

        return new Vector2Int(x, coords.y);
    }

    public Vector2Int MoveUp()
    {
        int y = coords.y + 1;
        if (y >= gridHeight)
        {
            return new Vector2Int(coords.x, gridHeight - 1);
        }

        return new Vector2Int(coords.x, y);
    }

    public Vector2Int MoveDown()
    {
        int y = coords.y - 1;
        if (y < 0)
        {
            return new Vector2Int(coords.x, 0);
        }

        return new Vector2Int(coords.x, y);
    }

    public Vector2Int ChooseRandomDirection()
    {
        int direction = rng.RandomInt(0, 4);
        Vector2Int moveCoords = new Vector2Int();
        switch (direction)
        {
            case 0:
                moveCoords = MoveRight();
                Debug.Log("|| MOVED RIGHT: " + moveCoords.x + ", " + moveCoords.y + " ||");
                break;
            case 1:
                moveCoords = MoveLeft();
                Debug.Log("|| MOVED LEFT: " + moveCoords.x + ", " + moveCoords.y + " ||");
                break;
            case 2:
                moveCoords = MoveUp();
                Debug.Log("|| MOVED UP: " + moveCoords.x + ", " + moveCoords.y + " ||");
                break;
            case 3:
                moveCoords = MoveDown();
                Debug.Log("|| MOVED DOWN: " + moveCoords.x + ", " + moveCoords.y + " ||");
                break;
        }
        
        return moveCoords;
    }
}
