using UnityEngine;
using System.Collections.Generic;

public class PhysicsWorld : MonoBehaviour
{
	public bool enableRaycastDebugging = false;

	public static int PlayerLayer { get; private set; }
	public static int EnvironmentLayer { get; private set; }

	public static int PlayerMask { get; private set; }
	public static int EnvironmentMask { get; private set; }

	void Awake()
	{
		PlayerLayer = LayerMask.NameToLayer("Player");
		EnvironmentLayer = LayerMask.NameToLayer("Environment");

		PlayerMask = 1 << PlayerLayer;
		EnvironmentMask = 1 << EnvironmentLayer;
	}

	private struct RaycastDebugInfo
	{
		public Vector2 origin;
		public Vector2 direction;
		public float distance;
		public int layerMask;
		public RaycastHit2D hitResult;
	}

	private List<RaycastDebugInfo> raycastDebugs = new List<RaycastDebugInfo>();

	public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
	{
		RaycastHit2D result = Physics2D.Raycast(origin, direction, distance, layerMask);

		if (enableRaycastDebugging) {
			debugRaycast(new RaycastDebugInfo()
			{
				origin = origin,
				direction = direction,
				distance = distance,
				layerMask = layerMask,
				hitResult = result,
			});
		}

		return result;
	} 

	private void debugRaycast(RaycastDebugInfo info)
	{
		raycastDebugs.Add(info);
	}

	void Update()
	{
		raycastDebugs.Clear();
	}

	void OnDrawGizmos()
	{
		if (!enableRaycastDebugging)
		{
			return;
		}

		foreach (var info in raycastDebugs)
		{
			GizmoUtil.DrawCircle(info.origin, 0.1f, Color.yellow);
			GizmoUtil.DrawArrow(info.origin, info.origin + info.direction * info.distance, Color.yellow);
		}
	}
}