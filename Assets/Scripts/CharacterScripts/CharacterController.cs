using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
public class CharacterController : MonoBehaviour {
	//used in lateral movement
	[Header("Player Variables")]
	public float maxSpeed = 11.0f;
	public bool right = true;

	//used in vertical movement
	[Header("Jump Variables")]
	public Transform groundCheck;
	public float jumpForce = 5.0f;
	bool ground = false;
	float groundRadius = 0.05f;

	//used for rope movement
	[Header("Rope Movement")]
	public Transform ropeCheck;
	public float swingForce = 2.0f;
	public float ropeJumpMultiplier = 2.0f;
	public float secondsPerRopeClimbed = 0.5f;

	bool rope = false;
	SpringJoint2D ropeHold;
	float ropeRadius = 0.1f;
	float lastRopeTime;

	[Header("LayerMasks")]
	public LayerMask groundLayer;
	public LayerMask ropeLayer;

	Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		ropeHold = GetComponent<SpringJoint2D>();
	}

	void Update()
	{
		//jump
		if(ground && (Input.GetAxis("Jump") > 0 || Input.GetAxis("Vertical") > 0)) { Jump(jumpForce); }
		//grab rope
		if(rope && Input.GetAxis("Fire1") > 0) { GrabRope(); }
		//release rope
		if(ropeHold.enabled && Input.GetAxis("Jump") > 0) { DropRope(); }
		//climb rope
		if(ropeHold.enabled && ropeHold.connectedBody != null) { ClimbRope(); }
	}

	void FixedUpdate () 
	{
		Rigidbody2D character = GetComponent<Rigidbody2D>();
		//Check for jumping
		ground = Physics2D.OverlapCircle(groundCheck.position,groundRadius, groundLayer);
		anim.SetBool("Ground",ground);
		anim.SetFloat("VerticalSpeed", character.velocity.y);

		//release rope if our connected body broke
		if(ropeHold.connectedBody == null) { DropRope(); }

		//check for climbing
		rope = Physics2D.OverlapCircle(ropeCheck.position,ropeRadius, ropeLayer);
		float upMovement = Input.GetAxis("Vertical");
		anim.SetFloat("ClimbDirection",upMovement);
		if(ropeHold.enabled && ropeHold.enabled) { anim.SetBool("HoldingRope",true); }
		else { anim.SetBool("HoldingRope",false); }

		//check for movement
		float move = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed",Mathf.Abs(move));
		if(!ropeHold.enabled)
		{
			//move the rigidbody
			character.velocity = new Vector2(move * maxSpeed,character.velocity.y);
			//change the direction if need to
			if (move > 0 && !right) { Flip(); }
			else if(move < 0 && right) { Flip(); }
		}
		else { character.AddForce(new Vector2(move*swingForce,0),ForceMode2D.Impulse); }
	}

	void Flip()
	{
		right = !right;
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void Jump(float force)
	{
		anim.SetBool("Ground",false);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0,force),ForceMode2D.Impulse);
	}

	void GrabRope()
	{
		Collider2D collider = Physics2D.OverlapCircle(ropeCheck.position,ropeRadius, ropeLayer);
		if(collider != null)
		{
			GameObject ropeSegment = collider.gameObject;
			ropeHold.connectedBody = ropeSegment.GetComponent<Rigidbody2D>();
			ropeHold.enabled = true;
			this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		}
	}

	void DropRope()
	{
		ropeHold.connectedBody = null;
		ropeHold.enabled = false;
		this.transform.rotation = Quaternion.Euler(Vector3.zero);
		this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void ClimbRope()
	{
		if(lastRopeTime <= secondsPerRopeClimbed)
		{
			lastRopeTime += Time.deltaTime;
			return;
		}
		if(Input.GetAxis("Vertical") != 0)
		{
			GameObject ropeObj = ropeHold.connectedBody.gameObject;
			RopeSegment segment = ropeObj.GetComponent<RopeSegment>();
			if(segment != null)
			{
				lastRopeTime = 0;
				if(Input.GetAxis("Vertical") > 0)
				{
					if(segment.up != null) { ropeHold.connectedBody = segment.up.GetComponent<Rigidbody2D>(); }
					else
					{
						//launch them up slightly and drop the rope (we have reached the end)
						DropRope();
						Jump(jumpForce*ropeJumpMultiplier);
					}
				}
				else if(Input.GetAxis("Vertical") < 0)
				{
					if(segment.down != null) { ropeHold.connectedBody = segment.down.GetComponent<Rigidbody2D>(); }
					//we are at the bottom, drop the rope
					else { DropRope(); }
				}
			}
		}
	}


}
