using UnityEngine;
using System.Collections;

public class Junction : MonoBehaviour {

    // the neighboring junctions
    public Transform upJunction, downJunction, leftJunction, rightJunction;
    // which direction our junction is "facing"
    public enum DirectingDirection {up, right, down, left};
    public DirectingDirection direction;
    public GameObject pointingChild;

    public bool isExit;

    void Start()
    {
        // get our pointing child object
        pointingChild = transform.GetChild(0).gameObject;
        // sets up the neighboring Junctions
        FindNeighbors();
        // make sure we are pointing at a legit junction
        ChangeDirection();
        // makes sure we are facing the right junction
        PointAtJunctionWeSendTo();
    }

    void Update()
    {
        
    }

    public Transform RedirectToNextJunction()
    {
        if (direction == DirectingDirection.down)
        {
               return downJunction;  
        }
        else if (direction == DirectingDirection.up)
        {
            return upJunction;
        }
        else if (direction == DirectingDirection.right)
        {
            return rightJunction;
        }
        else if(direction == DirectingDirection.left)
        {
            return leftJunction;
        }
        // this is here so we always return something
        else { return transform; }

        PointAtJunctionWeSendTo();
    }

    public void ChangeDirection()
    {
        // This bit seems really amatuer, but rotate it to the next direction, if we don't have a junction, the go to the next available one (clockwise)
        if (direction == DirectingDirection.left)
        {
            if (upJunction)
            {
                direction = DirectingDirection.up;
            }
            else if(rightJunction)
            {
                direction = DirectingDirection.right;
            }
            else if (downJunction)
            {
                direction = DirectingDirection.down;
            }
            else
            {
                direction = DirectingDirection.left;
            }
        }
        else if (direction == DirectingDirection.up)
        {
            if (rightJunction)
            {
                direction = DirectingDirection.right;
            }
            else if (downJunction)
            {
                direction = DirectingDirection.down;
            }
            else if (leftJunction)
            {
                direction = DirectingDirection.left;
            }
            else
            {
                direction = DirectingDirection.up;
            }
        }
        else if (direction == DirectingDirection.right)
        {
            if (downJunction)
            {
                direction = DirectingDirection.down;
            }
            else if (leftJunction)
            {
                direction = DirectingDirection.left;
            }
            else if (upJunction)
            {
                direction = DirectingDirection.up;
            }
            else
            {
                direction = DirectingDirection.right;
            }
        }
        else if (direction == DirectingDirection.down)
        {
            if (leftJunction)
            {
                direction = DirectingDirection.left;
            }
            else if (upJunction)
            {
                direction = DirectingDirection.up;
            }
            else if (rightJunction)
            {
                direction = DirectingDirection.right;
            }
            else
            {
                direction = DirectingDirection.down;
            }
        }

        // face the new direction
        PointAtJunctionWeSendTo();
    }

    public void FindNeighbors()
    {
        // if we don't have junction for a direction, raycast in that direction and put whatever junction we hit as our junction
        if (!upJunction)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up, out hit, 20))
            {
                upJunction = hit.collider.gameObject.transform;
            }
        }
        if (!downJunction)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 20))
            {
                downJunction = hit.collider.gameObject.transform;
            }
        }
        if (!rightJunction)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 20))
            {
                rightJunction = hit.collider.gameObject.transform;
            }
        }
        if (!leftJunction)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 20))
            {
                leftJunction = hit.collider.gameObject.transform;
            }
        }
    }

    // moves the arrow to point at where the node will send an object next
    public void PointAtJunctionWeSendTo()
    {
        /*
        if (direction == DirectingDirection.down) { transform.Rotate(new Vector3(0,0,-180)); }
        else if (direction == DirectingDirection.up) { transform.Rotate(new Vector3(0, 0, 0)); }
        else if (direction == DirectingDirection.right) { transform.Rotate(new Vector3(0, 0, -90)); }
        else if (direction == DirectingDirection.left) { transform.Rotate(new Vector3(0, 0, 90)); }   
        */

        if (direction == DirectingDirection.down) { pointingChild.transform.position = new Vector3(transform.position.x, transform.position.y - .2f, transform.position.z); }
        else if (direction == DirectingDirection.up) { pointingChild.transform.position = (new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z)); }
        else if (direction == DirectingDirection.right) { pointingChild.transform.position = (new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z)); }
        else if (direction == DirectingDirection.left) { pointingChild.transform.position = (new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z)); }
    }
}
