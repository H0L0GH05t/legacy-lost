using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	//used in lateral movement
	public float maxSpeed = 11.0f;
	public bool right = true;
	//used in vertical movement
	bool ground = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public float jumpForce = 200.0f;
	public LayerMask groundLayer;

	Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if(ground && Input.GetAxis("Jump") > 0)
		{
			anim.SetBool("Ground",false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce));
		}
	}

	void FixedUpdate () 
	{
		Rigidbody2D character = GetComponent<Rigidbody2D>();
		//checks for ground
		ground = Physics2D.OverlapCircle(groundCheck.position,groundRadius, groundLayer);
		anim.SetBool("Ground",ground);
		anim.SetFloat("VerticalSpeed", character.velocity.y);

		//get input
		float move = Input.GetAxis("Horizontal");
		//set animator
		anim.SetFloat("Speed",Mathf.Abs(move));
		//move the rigidbody
		character.velocity = new Vector2(move * maxSpeed,character.velocity.y);

		if (move > 0 && !right) { Flip(); }
		else if(move < 0 && right) { Flip(); }
	}

	void Flip()
	{
		right = !right;
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
