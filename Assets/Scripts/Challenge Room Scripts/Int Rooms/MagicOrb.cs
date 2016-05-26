using UnityEngine;
using System.Collections;

public class MagicOrb : MonoBehaviour {

    // our starting junction that we should move to first
    public Transform startingJunction;
    // whatever junction we are moving towards
    Transform targetJunction;
    // speed of our magic orb
    public float speed;

    public static bool IsAlive;

	// Use this for initialization
	void Start ()
    {
        IsAlive = true;
        // so we can assign our starting junction in the inspector
        targetJunction = startingJunction;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (IsAlive)
        {
            float step = speed * Time.deltaTime;
            // moves us towards our target junction
            transform.position = Vector3.MoveTowards(transform.position, targetJunction.position, step);

            // changes the junction we are moving to
            if (transform.position == targetJunction.transform.position)
            {
                // if the junction we just reached isn't the exit, then find a new junction to move to
                if (!targetJunction.GetComponent<Junction>().isExit)
                {
                    // get the junction we just hit
                    Junction oldJunction = targetJunction.GetComponent<Junction>();
                    targetJunction = targetJunction.gameObject.GetComponent<Junction>().RedirectToNextJunction();
                    // now change the old junction
                    oldJunction.ChangeDirection();
                    
                }
                else
                {
                    // we reached the finish, yay!
                    IntChallengeRoom.gameIsWon = true;
                    Debug.Log("yay! We won!");
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IsAlive = false;
            // change our color to red so we know we dun goofed
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if(other.gameObject.tag == "BlockerPickup")
        {
            other.gameObject.GetComponent<BlockerPickup>().ActivateBlockerPickup();
        }
    }
}
