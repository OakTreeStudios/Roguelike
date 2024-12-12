using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region LevelGrid Class
/*
* LevelGrid is a class for creating a grid of rooms for a level
* 
*/
public class LevelGrid : MonoBehaviour
{
    #region Variables

    //Map Size in rooms
    public int gridWidth = 10;
    public int gridHeight = 10;

    //Room size in Unity units
    public int roomWidth = 30;
    public int roomHeight = 17;

    public RandomNumber rng;


    //2D array represnting our rooms
    public GameObject[,] grid;

    //List of rooms that can be spawned
    public List<GameObject> rooms;

    //Starter Room
    public GameObject startRoom;

    #endregion

    #region Methods
    //Method of using the snake method to place rooms.
    public void SnakeGeneration(uint seed)
    {
        //To Do: Implement Snake Generation

        //Idea:
        //Do this in two passes
        //First pass: Spawn snakes
        //Second pass: Determine how rooms are connected.

        //Psuedo Code:
        //1. Mark middle left corner of grid as start room
        //2. Spawn snake to right of start room
            //a. Snakes move in a random direction if it is valid
            //b. If snake can't move in any direction, it dies
            //c. Snake has a small percentage to spawn a new snake
            //d. Snake has a small percentage to die
        //3. Wait until all snakes are dead.
        
        //Seed our random number generator
        RandomNumber rng = new RandomNumber();
        rng.Initialize(seed);

        //Calculate center of y-axis
        int midY = gridHeight / 2;
        
        //Spawn Start Room at midy, 0
        grid[0, midY] = startRoom;
        
        //Spawn Snake
        LevelGridSnake snake = new LevelGridSnake(gridWidth, gridHeight, 1, midY, ref rng);
        snake.MoveRandom();
        snake.MoveRandom();
        snake.MoveRandom();
        snake.MoveRandom();
    }

    //Initializer for the grid
    public void Initialize(int gridWidth, int gridHeight)
    {
        //Check if input is valid
        if (gridWidth <= 0 || gridHeight <= 0)
        {
            throw new System.ArgumentException("|| LevelGrid || Grid width and height must be greater than 0");
        }

        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;

        grid = new GameObject[gridWidth, gridHeight];
    }

    public void SetRoomList(List<GameObject> rooms)
    {
        this.rooms = rooms;
    }

    public void SetStartRoom(GameObject startRoom)
    {
        this.startRoom = startRoom;
    }

    //Generates the grid of rooms by assigning rooms in the grid
    public void GenerateGrid(uint seed)
    {
        //Seed our random number generator
        RandomNumber rng = new RandomNumber();
        rng.Initialize(seed);

        //Break map into a grid
        for (int x = 0; x < gridWidth - 1; x++)
        {
            for (int y = 0; y < gridHeight - 1; y++)
            {
                //Always spawn Start Room at center of map
                if (x == 0 && y == 0)
                {
                    GameObject room = startRoom;
                    grid[x, y] = room;
                    //Debug.Log("|| SPAWNED START ROOM ||");
                }
                else
                {
                    //Grab a random room
                    int roomIndex = rng.RandomInt(0, rooms.Count - 1);
                    GameObject room = rooms[roomIndex];
                    grid[x, y] = room;
                    //Debug.Log("|| SPAWNED ROOM " + room.roomObject.name + " | With Index: " + roomIndex + " ||");
                }
            }
        }
    }

    //Spawns the grid of rooms in the scene
    public void SpawnGrid()
    {
        //To Do: Needs optimizaion currently O(n^2)

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 spawnPos = new Vector3(x * roomWidth, y * roomHeight, 0);
                GameObject room = grid[x, y];

                //Check if there is a room to spawn
                if(room != null) {
                    Instantiate(room, spawnPos, Quaternion.identity);
                }
            }
        }
    }
    #endregion
}
#endregion
