using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePart
{

    private Vector3 oldPosition;

    private Vector3 currentPosition;

    public RopePart (Vector3 ropePos)
    {
        currentPosition = oldPosition = ropePos;
    }

    public void updatePos(Vector3 updatedPosition)
    {
        oldPosition = currentPosition;
        currentPosition = updatedPosition;
    }

    public Vector3 getCurrentPos() => currentPosition;

}
