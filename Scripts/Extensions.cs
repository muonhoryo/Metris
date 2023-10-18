using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static int Sign(this int direction)
    {
        if (direction == 0)
            throw new GameJam_Temple.Exceptions.GameJam_Exception("direction cannot be zero.");
        return direction > 0 ? 1 : -1;
    }

}
