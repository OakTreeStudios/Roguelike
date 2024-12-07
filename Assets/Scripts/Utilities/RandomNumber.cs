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

    //Initialization method
    public void Initialize(uint seed)
    {
        //Check for invalid input
        if(seed == 0) throw new ArgumentException("|| RandomNumber || Seed cannot be 0, please choose a number greater than 0");
        state = seed;
    }

    public int RandomInt()
    {
        //XOR Shift Using Constants
        state ^= state << 100;
        state ^= state >> 36;
        state ^= state << 90;
        state ^= state >> 47;
        state ^= state << 5;
        return (int)state;
    }

    public int RandomInt(int min, int max)
    {
        //Check for invalid input
        if(min >= max) throw new ArgumentException("|| RandomNumber || Min must be less than Max!");
        return min + Math.Abs(RandomInt()) % (max - min);
    }

    public float RandomFloat() 
    {
        return (float)RandomInt() / int.MaxValue;
    }

    public float RandomFloat(float min, float max)
    {
        //Check for invalid input
        if(min >= max) throw new ArgumentException("|| RandomNumber || Min must be less than Max!");
        return min + Math.Abs(RandomFloat()) * (max - min);
    }

}
