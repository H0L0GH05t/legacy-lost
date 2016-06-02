using UnityEngine;
using System.Collections;

public class IntChallengeRoom : MonoBehaviour {

    public static bool gameIsWon;
    public GameObject cursorObject;
    public static bool isUsingController;
    public float cursorSpeed;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // if we are using mouse and keyboard
        if (!isUsingController)
        {
            // move cursor to follow, make it slightly above the plane of the other objects so we can raycast from the cursor (for easy controller controls)
            cursorObject.transform.position = new Vector3(mousePos.x, mousePos.y, 5);

            // if we press start on the controller, then we need to toggle to controller controls
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                isUsingController = true;
            }
        }
        else
        {
            // get our controller input
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            // move our cursor object
            cursorObject.transform.Translate(movement * (cursorSpeed * Time.deltaTime));

            // if we click on the mouse, switch back to mouse and keyboard
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isUsingController = false;
            }
        }

        // we can click on stuff
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(cursorObject.transform.position, Vector3.forward * -1);
            RaycastHit hit;

            if (Physics.SphereCast(ray, .5f, out hit, 10))
            {
                if (hit.collider.gameObject.tag == "Junction")
                {
                    hit.collider.gameObject.GetComponent<Junction>().ChangeDirection();
                }
            }
        }

        // just dev stuff
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
