using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utills 
{

    public static Vector3 GetRandomVector3(float lower, float upper) //generates random  vector3 with values in given range
    {
        return new Vector3(Random.Range(lower, upper), Random.Range(lower, upper), Random.Range(lower, upper));
    }

    public static float GetRandomfloat(float lower,float upper) // returns a random float for a specific range
    {
        return Random.Range(lower, upper);
    }

    public static float RemoveSign(float value)
    {
        float squared = value * value;
        return Mathf.Sqrt(squared);
    }
}
