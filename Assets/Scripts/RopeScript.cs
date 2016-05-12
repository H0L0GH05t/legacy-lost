using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class RopeScript : MonoBehaviour {

	public float distanceBetween = 0.5f;
	public int subDivisions = 10;
	public GameObject rope;

	GameObject[] joints;


	// Use this for initialization
	void Start () {
		joints = new GameObject[subDivisions];
		BuildRope();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BuildRope()
	{
		GameObject ropeParent = this.gameObject;
		for(int i = 0; i < subDivisions;i++)
		{
			joints[i] = Instantiate(rope);
			joints[i].transform.position = this.transform.position;
			//DistanceJoint2D joint = joints[i].GetComponent<DistanceJoint2D>();
			//joint.connectedBody = ropeParent.GetComponent<Rigidbody2D>();
			//joint.distance = distanceBetween;

			//SpringJoint2D joint = joints[i].GetComponent<SpringJoint2D>();
			//joint.connectedBody = ropeParent.GetComponent<Rigidbody2D>();
			//joint.distance = distanceBetween;

			//HingeJoint2D joint = joints[i].GetComponent<HingeJoint2D>();
			//joint.connectedBody = ropeParent.GetComponent<Rigidbody2D>();

			FixedJoint2D joint = joints[i].GetComponent<FixedJoint2D>();
			joint.connectedBody = ropeParent.GetComponent<Rigidbody2D>();

			ropeParent = joints[i];
		}
	}
}
