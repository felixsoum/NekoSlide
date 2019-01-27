using UnityEngine;

public class LevelData
{
    public char[,] GetGrid()
    {
        return new char[6, 6]
        {
        {'0','0','0','0','b','c'},
        {'0','a','a','a','b','c'},
        {'x','x','d','0','b','c'},
        {'0','0','d','0','0','0'},
        {'0','0','e','0','0','0'},
        {'0','0','e','0','0','0'}
        };
    }


}
