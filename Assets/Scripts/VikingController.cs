using UnityEngine;
using System.Collections.Generic;

public class VikingController : MonoBehaviour
{
	public Animator animator;
	public Rigidbody2D rigidBody;

	public Transform groundCheckOrigin;
	public float groundCheckWidth;

	public GameObject spearPrefab;

	public float moveSpeed = 5f;

	public float groundCheckDistance = 0.5f;
	public float jumpForce = 2f;
	private bool isGrounded = false;
	private bool jumpRequested = false;

	private float speed = 0f;
	private FacingDirection facing = FacingDirection.Right;

	private CameraController cameraController;

	void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}

		if (rigidBody == null)
		{
			rigidBody = GetComponent<Rigidbody2D>();
		}

		cameraController = Camera.main.GetComponent<CameraController>();
	}

	void Update()
	{
		// movement
		{
			float h = Input.GetAxis("Horizontal");
			speed = moveSpeed * h;
			Vector3 scale = transform.localScale;
			if (!Mathf.Approximately(h, 0f))
			{
				scale.x = Mathf.Sign(h);
				facing = (scale.x < 0f) ? FacingDirection.Left : FacingDirection.Right;
			}
			transform.localScale = scale;

			if (transform.position.x < cameraController.MinimumPlayerLeft ||
					(Mathf.Approximately(transform.position.x, cameraController.MinimumPlayerLeft) && h < 0f))
			{
				speed = 0f;
				Vector3 position = transform.position;
				position.x = cameraController.MinimumPlayerLeft;
				transform.position = position;
			}

			animator.SetFloat("speed", Mathf.Abs(speed));
		}

		// shooting
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Vector3 direction = (facing == FacingDirection.Right) ? Vector3.right : Vector3.left;
				Quaternion rotation = Quaternion.AngleAxis((facing == FacingDirection.Right) ? 0f : 180f, Vector3.forward);
				GameObject go = Instantiate(spearPrefab, transform.position, rotation);
				var spear = go.GetComponent<SpearController>();
				spear.Init(direction);
			}
		}
		
		// grounded check
		{
			isGrounded = checkIfGrounded();

			animator.SetBool("is_grounded", isGrounded);
		}

		// jumping
		{
			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				jumpRequested = true;
			}
		}
	}

	void FixedUpdate()
	{
		if (jumpRequested)
		{
			jumpRequested = false;
			rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
	}

	private bool checkIfGrounded()
	{
		var raycast = Physics2D.BoxCast(groundCheckOrigin.position,
			new Vector2(groundCheckWidth, 0.1f),
			0f,
			Vector2.down,
			groundCheckDistance,
			PhysicsWorld.EnvironmentMask);

		return raycast.collider != null;
	}
}