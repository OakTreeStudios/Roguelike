using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Random Generator Class
* Uses XOR Shift Algorithm to generate random numbers
*/
public class RandomNumber : MonoBehaviour
{

    //Internal State of the Random Number Generator
    private uint state;

    #region Methods

    //Initialization method
    public void Initialize(uint seed)
    {
        //Check for invalid input
        if(seed == 0) throw new ArgumentException("|| RandomNumber || Seed cannot be 0, please choose a number greater than 0");
        state = seed;
    }

    //This method generates a number
    public int RandomInt()
    {
        //XOR Shift Using Constants
        state ^= state << 13;
        state ^= state >> 7;
        state ^= state << 17;
        return (int)state;
    }

    //Generats a int within a range
    public int RandomInt(int min, int max)
    {
        //Check for invalid input
        if(min >= max) throw new ArgumentException("|| RandomNumber || Min must be less than Max!");
        return min + Math.Abs(RandomInt()) % (max - min);
    }

    //Generate a float
    public float RandomFloat() 
    {
        return (float)RandomInt() / int.MaxValue;
    }

    //Generate a float within a range
    public float RandomFloat(float min, float max)
    {
        //Check for invalid input
        if(min >= max) throw new ArgumentException("|| RandomNumber || Min must be less than Max!");
        return min + Math.Abs(RandomFloat()) * (max - min);
    }
    #endregion
}
