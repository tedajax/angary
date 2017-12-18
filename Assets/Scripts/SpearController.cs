using UnityEngine;
using System.Collections.Generic;

public class SpearController : MonoBehaviour
{
	public float moveSpeed = 10f;
	public float lifeDuration = 3f;

	private float lifetime = 0f;
	private Vector3 direction = Vector3.right;

	public void Init(Vector3 direction)
	{
		lifetime = lifeDuration;
		this.direction = direction;
	}

	void Update()
	{
		if (lifetime > 0f)
		{
			lifetime -= Time.deltaTime;
			if (lifetime <= 0f)
			{
				Destroy(gameObject);
			}
		}

		Vector3 position = transform.position;
		position += direction * moveSpeed * Time.deltaTime;
		transform.position = position;
	}
}