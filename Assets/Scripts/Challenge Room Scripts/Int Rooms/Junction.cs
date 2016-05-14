using UnityEngine;
using System.Collections;

public class Junction : MonoBehaviour {

    // the neighboring junctions
    public Transform upJunction, downJunction, leftJunction, rightJunction;
    // which direction our junction is "facing"
    public enum DirectingDirection {up,down,left,right};
    public DirectingDirection direction;

    public Transform RedirectToNextJunction()
    {
        if (direction == DirectingDirection.down) { return downJunction; }
        else if (direction == DirectingDirection.up) { return upJunction; }
        else if (direction == DirectingDirection.right) { return rightJunction; }
        else if(direction == DirectingDirection.left) { return leftJunction; }
        // this is here so we always return something
        else { return transform; }
    }
}
