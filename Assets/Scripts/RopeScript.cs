using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class RopeScript : MonoBehaviour {

	public float distanceBetween = 0.1f;
	public int subDivisions = 25;
	public float yOffsetOnBuild = -0.04f;
	public Transform ropeParent; //Only for sake of not clutering hierarchy, can be null
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
		GameObject previousRope = this.gameObject;
		for(int i = 0; i < subDivisions;i++)
		{
			joints[i] = Instantiate(rope);
			joints[i].transform.SetParent(ropeParent);

			FixedJoint2D joint = joints[i].GetComponent<FixedJoint2D>();
			joint.connectedBody = previousRope.GetComponent<Rigidbody2D>();

			if(previousRope.GetComponent<RopeSegment>() != null)
			{
				previousRope.GetComponent<RopeSegment>().Down = joints[i];
				joints[i].GetComponent<RopeSegment>().Up = previousRope;
			}

			Vector3 newPos = previousRope.transform.position + new Vector3(0,yOffsetOnBuild,0);
			joints[i].transform.position = newPos;

			previousRope = joints[i];
		}
	}
}
