using UnityEngine;
using System.Collections;

public class IntChallengeRoom : MonoBehaviour {

    public static bool gameIsWon;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        // we can click on stuff
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Junction")
                {
                    hit.collider.gameObject.GetComponent<Junction>().ChangeDirection();
                }
            }
        }
    }
}
