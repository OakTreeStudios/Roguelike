using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GenLevelCastle : MonoBehaviour
{
    //List of rooms that can be spawned
    public List<GameObject> rooms;
    //Starter Room
    public GameObject startRoom;
    //Break map into a grid
    public int mapWidth = 10;
    public int mapHeight = 10;
    public int roomWidth = 30;
    public int roomHeight = 17;
    
    public uint levelSeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        LevelGrid grid = new LevelGrid();
        grid.Initialize(mapWidth, mapHeight);
        grid.SetRoomList(rooms);
        grid.SetStartRoom(startRoom);

        //grid.GenerateGrid(levelSeed);

        //Snake Generation Test
        grid.SnakeGeneration(levelSeed);
        

        //grid.SpawnGrid();

    }
}
