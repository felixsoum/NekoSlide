using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    char[,] Grid { get; set; } =  new char[6, 6]
    {
        {'0','0','0','0','0','0'},
        {'0','0','0','0','0','0'},
        {'0','0','0','0','0','0'},
        {'0','0','0','0','0','0'},
        {'0','0','0','0','0','0'},
        {'0','0','0','0','0','0'}
    };
}
