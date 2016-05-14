using UnityEngine;
using System.Collections;

public class MagicOrb : MonoBehaviour {

    // our starting junction that we should move to first
    public Transform startingJunction;
    // whatever junction we are moving towards
    Transform targetJunction;
    // speed of our magic orb
    public float speed;

	// Use this for initialization
	void Start ()
    {
        // so we can assign our starting junction in the inspector
        targetJunction = startingJunction;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        // moves us towards our target junction
        transform.position = Vector3.MoveTowards(transform.position, targetJunction.position, step);

        // changes the junction we are moving to
        if (transform.position == targetJunction.transform.position)
        {
            // get a new junction from the junction we just reached
            targetJunction = targetJunction.gameObject.GetComponent<Junction>().RedirectToNextJunction();
        }
	}
}
