using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	public string flashPattern = "AAZZ";
	private int flashIndex;

	public bool isFlashing = false;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		if (isFlashing && flashPattern.Length > 0)
		{
			char value = char.ToUpper(flashPattern[flashIndex % flashPattern.Length]);
			++flashIndex;

			int zeroed = value - 'A';
			float alpha = 1f - (float)zeroed / ('Z' - 'A');

			spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
		}
		else
		{
			spriteRenderer.color = Color.white;
		}
	}
}