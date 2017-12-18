using UnityEngine;

public class GameSystems : MonoBehaviour
{
	private static PhysicsWorld physics;
	private static GameSystems instance;

	public static PhysicsWorld Physics { get { return physics; } }

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("Attempted to create a GameSystems instance when one already exists.");
			Destroy(gameObject);
		}

		instance = this;

		physics = gameObject.AddComponent<PhysicsWorld>();
	}
}