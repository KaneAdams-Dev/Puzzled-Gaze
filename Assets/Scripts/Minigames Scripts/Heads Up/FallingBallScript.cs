using UnityEngine;

/// <summary>
/// Destroys ball if fallen offscreen.
/// Created by: Kane Adams
/// </summary>
public class FallingBallScript : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		if (gameObject.transform.position.y <= -100)
		{
			Destroy(gameObject);
		}
	}
}
