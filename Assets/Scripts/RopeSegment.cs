using UnityEngine;
using System.Collections;

[RequireComponent (typeof(FixedJoint2D))]
public class RopeSegment : MonoBehaviour {

	public GameObject up;
	public GameObject down;
	float gravity;

	void Start ()
	{
		gravity = this.GetComponent<Rigidbody2D>().gravityScale;
	}

	public GameObject Up
	{
		get
		{
			if(up == null || isConnected(up)) { return up; }
			else { return null; }
		}
		set
		{
			if(isJoint(value)) { up = value; }
		}
	}

	public GameObject Down
	{
		get
		{
			if(down == null || isConnected(down)) { return down; }
			else { return null; }
		}
		set
		{
			if(isJoint(value)) { down = value; }
		}
	}

	bool isJoint(GameObject test)
	{
		FixedJoint2D joint = test.GetComponent<FixedJoint2D>();
		return (joint != null);
	}

	bool isConnected(GameObject test)
	{
		FixedJoint2D joint = test.GetComponent<FixedJoint2D>();
		if(joint != null)
		{
			return joint.connectedBody != null;
		}
		else
		{
			return false;
		}
	}

	public void RemoveGravity()
	{
		this.GetComponent<Rigidbody2D>().gravityScale = 0;
	}

	public void ReturnGravity()
	{

		this.GetComponent<Rigidbody2D>().gravityScale = gravity;
	}

	public void DisableCollider()
	{
		this.GetComponent<BoxCollider2D>().enabled = false;
	}

	public void EnableCollider()
	{
		this.GetComponent<BoxCollider2D>().enabled = true;
	}
}
