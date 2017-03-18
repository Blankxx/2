using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Vector3 pos;
	private float speed = 2f;
	private Animator anim;
	private bool isMoving;
	public Vector2 lastMove;

	private static bool playerinLevel;

	public string startPoint;

	[SerializeField] private Transform rayUpStartPoint;
	[SerializeField] private Transform rayDownStartPoint;
	[SerializeField] private Transform rayLeftStartPoint;
	[SerializeField] private Transform rayRightStartPoint;

	[SerializeField] private LayerMask blockingLayer;

	public bool upBlocked;
	public bool downBlocked;
	public bool leftBlocked;
	public bool rightBlocked;

	public bool canMove; // new

	public bool sceneSwitched;

	void Start ()
	{
		canMove = true; // new
		pos = transform.position;
		anim = GetComponent<Animator> ();

		if (!playerinLevel)
		{
			playerinLevel = true;
			DontDestroyOnLoad (transform.gameObject); // when player loads into new scene, gameObject not destroyed.
		}
		else
		{
			Destroy (gameObject);
		}
	}
	void LateUpdate ()
	{
		Raycasting ();
		Move ();
	}
	void Move ()
	{
		if (Input.GetAxisRaw ("Vertical") > 0 && transform.position == pos && !upBlocked && canMove || Input.GetAxisRaw ("Vertical") < 0 && transform.position == pos && !downBlocked && canMove)
			{
				canMove = false; // new
				isMoving = true;
				pos += new Vector3 (0f, Input.GetAxisRaw ("Vertical"), 0f);
				lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
			}
			if (Input.GetAxisRaw ("Horizontal") < 0 && transform.position == pos && !leftBlocked && canMove|| Input.GetAxisRaw ("Horizontal") > 0 && transform.position == pos && !rightBlocked && canMove)
			{
				canMove = false; // new
				isMoving = true;
				pos += new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, 0f);
				lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
			}
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);
			if (transform.position == Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed))
			{
				canMove = true; // new
				isMoving = false;
				pos = transform.position;
			}
			anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
			anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
			anim.SetFloat ("LastMoveX", lastMove.x);
			anim.SetFloat ("LastMoveY", lastMove.y);
			anim.SetBool ("IsMoving", isMoving);
	}
	void Raycasting ()
	{
		Debug.DrawRay (rayUpStartPoint.position, Vector2.up, Color.blue, 1f);
		Debug.DrawRay (rayDownStartPoint.position, Vector2.down, Color.blue, 1f);
		Debug.DrawRay (rayLeftStartPoint.position, Vector2.left, Color.blue, 1f);
		Debug.DrawRay (rayRightStartPoint.position, Vector2.right, Color.blue, 1f);

		RaycastHit2D hitUp = Physics2D.Raycast (rayUpStartPoint.position, Vector2.up, 1f, blockingLayer);
		RaycastHit2D hitDown = Physics2D.Raycast (rayDownStartPoint.position, Vector2.down, 1f, blockingLayer);
		RaycastHit2D hitLeft = Physics2D.Raycast (rayLeftStartPoint.position, Vector2.left, 1f, blockingLayer);
		RaycastHit2D hitRight = Physics2D.Raycast (rayRightStartPoint.position, Vector2.right, 1f, blockingLayer);

		if (hitUp)
		{
			upBlocked = true;
		}
		else
		{
			upBlocked = false;
		}
		if (hitDown)
		{
			downBlocked = true;
		}
		else
		{
			downBlocked = false;
		}
		if (hitLeft)
		{
			leftBlocked = true;
		}
		else
		{
			leftBlocked = false;
		}
		if (hitRight)
		{
			rightBlocked = true;
		}
		else
		{
			rightBlocked = false;
		}
	}
	IEnumerator Delay()
	{
		yield return new WaitForSeconds (1);
	}
}