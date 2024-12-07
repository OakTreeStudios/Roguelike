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

    //Constructor
    public RandomNumber(uint seed)
    {
        //Check for invalid input
        if(seed == 0) throw new ArgumentException("|| RandomNumber || Seed cannot be 0, please choose a number greater than 0");
        state = seed;
    }

    public uint RandomInt()
    {
        //XOR Shift Using Constants
        state ^= state << 100;
        state ^= state >> 36;
        state ^= state << 90;
        state ^= state >> 47;
        state ^= state << 5;
        return state;
    }

    public uint RandomInt(uint min, uint max)
    {
        //Check for invalid input
        if(min >= max) throw new ArgumentException("|| RandomNumber || Min must be less than Max!");
        return RandomInt() % (max - min) + min;
    }

    public float RandomFloat() 
    {
        return (float)RandomInt() / uint.MaxValue;
    }

    public float RandomFloat(float min, float max)
    {
        //Check for invalid input
        if(min >= max) throw new ArgumentException("|| RandomNumber || Min must be less than Max!");
        return RandomFloat() * (max - min) + min;
    }

}
