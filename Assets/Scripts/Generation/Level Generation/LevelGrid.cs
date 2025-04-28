using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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

    //List of Snakes
    public List<LevelGridSnake> snakeList = new List<LevelGridSnake>();

    //Starter Room
    public GameObject startRoom;

    float chanceToSpawnSnake = 0.05f;
    float chanceToDieSnake = 0.2f;
    int maxSnakes = 3;

    int numCurrentRooms = 0;
    float maxRoomPercentage = 0.75f;

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
        //delta time
        //Seed time to current time
        uint deltaTime = BitConverter.ToUInt32(BitConverter.GetBytes(DateTime.Now.Ticks), 0);
        Debug.Log("|| DELTA TIME || " + deltaTime);
        rng.Initialize(deltaTime);

        grid[0, 0] = startRoom;
        
        //Spawn Snake
        snakeList.Add( new LevelGridSnake(gridWidth, gridHeight, 1, 0, ref rng, ref grid) );
        grid[1, 0] = startRoom;

        numCurrentRooms = 2;

        //While there are still snakes or number of rooms does not exceed grid size perecentage
        //while (snakeList.Count > 0 && numCurrentRooms < (gridWidth * gridHeight) * maxRoomPercentage)
        for (int iteration = 0; iteration < 100; iteration++) // Limit iterations to prevent infinite loop during testing
        {
            //Iterate through snakes
            for (int i = 0; i < snakeList.Count; i++)
            {
                //Get current snake
                LevelGridSnake currSnake = snakeList[i];
                Debug.Log("|| SNAKE " + i + " ||");

                //Check if this snake should die.

                //Check if this snake should spawn a new snake

                //Check where we can move next
                currSnake.CheckDirection();

                //Move the snake in a valid random direction
                currSnake.MoveSnake();
                
                //Check if snake is alive
                if(!currSnake.IsAlive())
                {
                    //Snake is dead, remove it from the list
                    snakeList.RemoveAt(i);
                    i--; // Adjust index since we removed an element
                    Debug.Log("|| SNAKE " + i + " DIED ||");
                    continue;
                } else {
                    //Mark location with room
                    grid[currSnake.GetCurrCoords().x, currSnake.GetCurrCoords().y] = startRoom;
                }
                

                Debug.Log(currSnake.GetCurrCoords().x + " " + currSnake.GetCurrCoords().y);
                currSnake.printDirections();

            }
            Debug.Log("~~ Iterations: " + iteration + " ~~");
            //Check if all sankes have died
            if(snakeList.Count ==0)
            {
                Debug.Log("|| ALL SNAKES DEAD ||");
                break;
            }
        }
        
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
                //Check if there is a room to spawn
                if(grid[x,y] != null) {
                    //Debug.Log("|| SPAWNED ROOM " + grid[x,y].name + " ||");
                    Instantiate(grid[x,y], spawnPos, Quaternion.identity);
                }
            }
        }
    }
    #endregion
}
#endregion
