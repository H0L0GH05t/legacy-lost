using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class RopeScript : MonoBehaviour {

	public float distanceBetween = 0.1f;
	public int subDivisions = 25;
	public float yOffsetOnBuild = -0.04f;
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

			FixedJoint2D joint = joints[i].GetComponent<FixedJoint2D>();
			joint.connectedBody = ropeParent.GetComponent<Rigidbody2D>();

			if(ropeParent.GetComponent<RopeSegment>() != null)
			{
				ropeParent.GetComponent<RopeSegment>().down = joints[i];
				joints[i].GetComponent<RopeSegment>().up = ropeParent;
			}

			Vector3 newPos = ropeParent.transform.position + new Vector3(0,yOffsetOnBuild,0);
			joints[i].transform.position = newPos;

			ropeParent = joints[i];
		}
	}
}
