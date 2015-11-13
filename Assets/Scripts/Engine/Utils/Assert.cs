using UnityEngine;
using System;

public class Assert
{
    //The same funcionality that method "assert" in C++.
    public static void assert(bool condition, string msg)
    {
        #if DEBUG
            if (!condition)
            {
                Debug.LogError("Assert Error! :" + msg);
                throw new Exception(msg);
            }
        #endif
    }
}
