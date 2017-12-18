using UnityEngine;
using System.Collections.Generic;

public static class GizmoUtil
{
	static Stack<Color> gizmoColorStack = new Stack<Color>();

	public static void PushColor(Color color)
	{
		gizmoColorStack.Push(Gizmos.color);
		Gizmos.color = color;
	}

	public static Color PopColor()
	{
		Color result = gizmoColorStack.Pop();
		Gizmos.color = result;
		return result;
	}

	public static void DrawCircle(Vector2 position, float radius, Color color, int segments = 11)
	{
		PushColor(color);

		float delta = 360f / segments * Mathf.Deg2Rad;
		for (int i = 0; i < segments; ++i)
		{
			float t1 = i * delta;
			float t2 = (i + 1) * delta;
			Vector3 p1 = new Vector3(Mathf.Cos(t1) * radius + position.x, Mathf.Sin(t1) * radius + position.y);
			Vector3 p2 = new Vector3(Mathf.Cos(t2) * radius + position.x, Mathf.Sin(t2) * radius + position.y);

			Gizmos.DrawLine(p1, p2);
		}

		PopColor();
	}

	public static void DrawArrow(Vector2 origin, Vector2 target, Color color)
	{
		PushColor(color);

		Gizmos.DrawLine(origin, target);

		Vector2 delta = target - origin;
		Vector2 perp = new Vector2(-delta.y, delta.x).normalized;

		Vector2 headLeft = origin + delta * 0.75f + perp * (delta.magnitude * 0.33f);
		Vector2 headRight = origin + delta * 0.75f - perp * (delta.magnitude * 0.33f);

		Gizmos.DrawLine(target, headLeft);
		Gizmos.DrawLine(target, headRight);

		PopColor();
	}
}