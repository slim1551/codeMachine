using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType
{
    Normal,
    Obstacle,
    Start,
    End
}

public class Grid : IComparable {

    public int x;
    public int y;

    public int f;
    public int g;
    public int h;

    public GridType type;

    public Grid parent;

    public Grid(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    public int CompareTo(object obj)
    {
        Grid grid = (Grid)obj;

        if (this.f < grid.f)
        {
            return -1;
        }

        if (this.f > grid.f)
        {
            return 1;
        }
        return 0;
    }
}
