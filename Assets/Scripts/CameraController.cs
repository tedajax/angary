using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	private new Camera camera;

	public float cameraLead = 3f;
	public float smoothFactor = 0.5f;

	private float targetXPos = 0f;
	private float velocity = 0f;

	public float MinimumPlayerLeft
	{
		get
		{
			return targetXPos - camera.orthographicSize * camera.aspect + 0.5f;
		}
	}

	void Awake()
	{
		camera = GetComponent<Camera>();
	}

	void Update()
	{
		if (target == null)
		{
			return;
		}

		if (target.position.x + cameraLead > targetXPos)
		{
			targetXPos = target.position.x + cameraLead;
		}

		Vector3 position = transform.position;

		position.x = Mathf.SmoothDamp(position.x, targetXPos, ref velocity, smoothFactor);
		transform.position = position;
	}
}