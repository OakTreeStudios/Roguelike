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
    public int mapWidth = 5;
    public int mapHeight = 0;
    public int roomWidth = 30;
    public int roomHeight = 17;
    
    public const int CASTLESEED = 100;
    // Start is called before the first frame update
    void Start()
    {
        //Seed our random number generator
        //Eventually we will want to seed this with a level seed
        RandomNumber rng = new RandomNumber(CASTLESEED);

        //Generate a random number
        Debug.Log("|| RANDOM NUMBER TESTS ||");
        Debug.Log(rng.RandomInt());
        Debug.Log(rng.RandomInt(30, 60));
        Debug.Log(rng.RandomFloat());
        Debug.Log(rng.RandomFloat((float)3.33, (float)6.66));
        Debug.Log("|| RANDOM NUMBER TESTS ||");

        Debug.Log("|| LEVEL GENERATION ||");

        //Break map into a grid
        for (int x = 0; x < mapWidth - 1; x++)
        {
            //Always spawn Start Room at center of map
            if (x == 0)
            {
                Vector3 spawnPos = new Vector3(x * roomWidth, 0, 0);
                GameObject room = startRoom;
                Instantiate(room, spawnPos, Quaternion.identity);
                Debug.Log("|| SPAWNED START ROOM ||");
            }
            else
            {
                //Spawn a random room
                Vector3 spawnPos = new Vector3(x * roomWidth, 0, 0);
                GameObject room = rooms[rng.RandomInt(0, rooms.Count)];
                Instantiate(room, spawnPos, Quaternion.identity);
                Debug.Log("|| SPAWNED ROOM " + room.name + " ||");
            }
        }

    }
}
