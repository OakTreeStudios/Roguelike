using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

//Snake Class for generating a level grid
public class LevelGridSnake : MonoBehaviour
{
    Vector2Int PrevCoords, CurrCoords;
    int gridWidth, gridHeight;
    GameObject[,] levelGrid;

    RandomNumber rng;

    bool alive = true;
    bool Up = false;
    bool Down = false;
    bool Left = false;
    bool Right = false;

    public LevelGridSnake(int width, int height, int spawnX, int spawnY, ref RandomNumber rng, ref GameObject[,] levelGrid)
    {
        gridWidth = width;
        gridHeight = height;
        PrevCoords = new Vector2Int(spawnX, spawnY);
        CurrCoords = new Vector2Int(spawnX, spawnY);
        this.rng = rng;
        this.levelGrid = levelGrid;
    }

    private void printGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (levelGrid[x, y] == null)
                {
                    Debug.Log("|| EMPTY ||");
                }
                else
                {
                    Debug.Log("|| ROOM ||");
                }
            }
        }
    }

    public void SetCurrCoords(int x, int y)
    {
        CurrCoords = new Vector2Int(x, y);
    }
    
    public void SetPrevCoords(int x, int y)
    {
        PrevCoords = new Vector2Int(x, y);
    }

    public Vector2Int GetCurrCoords()
    {
        return CurrCoords;
    }

    public Vector2Int GetPrevCoords()
    {
        return PrevCoords;
    }

    public void CheckDirection()
    {
        //Check if we are at the edge of the grid or gird already has a room
        if (CurrCoords.x - 1 < 0 || levelGrid[CurrCoords.x - 1, CurrCoords.y] != null)
        {
            Left = false;
        }
        else
        {
            Left = true;
        }

        //Check Right
        if (CurrCoords.x + 1 >= gridWidth || levelGrid[CurrCoords.x + 1, CurrCoords.y] != null)
        {
            Right = false;
        }
        else
        {
            Right = true;
        }

        //Check Up
        if ( CurrCoords.y + 1 >= gridHeight || levelGrid[CurrCoords.x, CurrCoords.y + 1] != null)
        {
            Up = false;
        }
        else
        {
            Up = true;
        }

        //Check Down
        if (CurrCoords.y - 1 < 0 || levelGrid[CurrCoords.x, CurrCoords.y - 1] != null) 
        {
            Down = false;
        }
        else
        {
            Down = true;
        }
    }

    //Move the snake in a random direction
    public void MoveSnake()
    {
        //Check if we can move in any direction
        if (!Up && !Down && !Left && !Right)
        {
            alive = false;
            Debug.Log("|| SNAKE DIED ||");
            return;
        }

        //Create a list of possible directions to move
        List<Vector2Int> possibleDirections = new List<Vector2Int>();

        if (Up) possibleDirections.Add(new Vector2Int(0, 1));
        if (Down) possibleDirections.Add(new Vector2Int(0, -1));
        if (Left) possibleDirections.Add(new Vector2Int(-1, 0));
        if (Right) possibleDirections.Add(new Vector2Int(1, 0));

        //Select a random direction from the list
        int randomIndex = rng.RandomInt(0, possibleDirections.Count);
        Vector2Int direction = possibleDirections[randomIndex];

        //Update previous coordinates
        SetPrevCoords(CurrCoords.x, CurrCoords.y);

        //Move the snake in the selected direction
        CurrCoords += direction;

        //Debug.Log("|| SNAKE MOVED TO: " + CurrCoords.x + ", " + CurrCoords.y + " ||");
    }

    public void printDirections()
    {
        Debug.Log("|| UP: " + Up + " || DOWN: " + Down + " || LEFT: " + Left + " || RIGHT: " + Right + " ||");
    }

    public bool IsAlive() 
    {
        return alive;
    }
}
    
    
