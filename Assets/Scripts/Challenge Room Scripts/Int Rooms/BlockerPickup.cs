using UnityEngine;
using System.Collections;

public class BlockerPickup : MonoBehaviour {

    public GameObject[] walls;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void ActivateBlockerPickup()
    {
        // destroy all of the walls that correspond to this pickup
        foreach(GameObject wall in walls)
        {
            wall.SetActive(false);
            Destroy(wall);
        }

        // Now that walls are broken, find the neighbors of all of the junctions
        Junction[] junctions = GameObject.FindObjectsOfType<Junction>();

        foreach(Junction junc in junctions)
        {
            junc.FindNeighbors();
        }

        // destroy the pickup
        Destroy(gameObject);
    }
}
