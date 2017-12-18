using UnityEngine;

[ExecuteInEditMode]
public class GroundPlatform : MonoBehaviour
{
	public int width = 2;
	public int height = 2;

	private int prevWidth = 0, prevHeight = 0;

	private new BoxCollider2D collider;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		collider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void SetSize(int width, int height)
	{
		collider.size = new Vector2(width, height);
		spriteRenderer.size = new Vector2(width, height);
	}

	void Update()
	{
		if (prevWidth != width || prevHeight != height)
		{
			SetSize(width, height);
		}

		prevWidth = width;
		prevHeight = height;
	}
}