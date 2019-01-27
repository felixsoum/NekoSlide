﻿using System.Collections.Generic;

public static class LevelData
{
    public static List<char[,]> Grids = new List<char[,]>()
    {
        new char[6, 6]//Level 1
        {
        {'a','a','0','0','0','e'},
        {'b','0','0','d','0','e'},
        {'b','x','x','d','0','e'},
        {'b','0','0','d','0','0'},
        {'c','0','0','0','f','f'},
        {'c','0','g','g','g','0'}
        },
        new char[6, 6]//Level 2
        {
        {'a','0','0','i','i','i'},
        {'a','0','0','h','0','j'},
        {'x','x','0','h','g','j'},
        {'c','c','c','0','g','j'},
        {'0','0','d','0','f','f'},
        {'b','b','d','e','e','0'}
        },
        new char[6, 6]//Level x
        {
        {'0','0','0','0','0','0'},
        {'0','0','0','0','0','0'},
        {'0','x','x','d','0','0'},
        {'0','a','a','d','0','e'},
        {'0','b','0','d','0','e'},
        {'0','b','c','c','0','e'}
        },
        new char[6, 6]//Level x
        {
        {'a','0','0','b','0','0'},
        {'a','0','0','b','0','0'},
        {'a','x','x','b','0','0'},
        {'0','0','c','f','f','f'},
        {'0','0','c','0','0','e'},
        {'0','0','d','d','d','e'}
        },
        //new char[6, 6]//Level template
        //{
        //{'0','0','0','0','0','0'},
        //{'0','0','0','0','0','0'},
        //{'0','0','0','0','0','0'},
        //{'0','0','0','0','0','0'},
        //{'0','0','0','0','0','0'},
        //{'0','0','0','0','0','0'}
        //},
    };

    public static char[,] GetGrid(int index)
    {
        char[,] originalGrid = Grids[index];
        char[,] cloneGrid = new char[6, 6];
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                cloneGrid[i, j] = originalGrid[i, j];
            }
        }
        return cloneGrid;
    }
}
