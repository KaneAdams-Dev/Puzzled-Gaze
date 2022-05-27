using System.Collections;
using UnityEngine;

/// <summary>
/// Controls rate balls spawn in.
/// Created by: Kane Adams
/// </summary>
public class SpawnerScript : MonoBehaviour
{
	[Header("Spawn Settings")]
	[SerializeField] private GameObject ballPrefab;
	[SerializeField] private float spawnDelay;
	[SerializeField] private float spawnMin;
	[SerializeField] private float spawnMax;

	private bool isActive;

	private Vector2 screenBounds;
	private float objectWidth;
	private float objectHeight;

	// Start is called before the first frame update
	void Start()
	{
		isActive = true;

		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		objectWidth = ballPrefab.GetComponent<MeshRenderer>().bounds.size.x / 2;
		objectHeight = ballPrefab.GetComponent<MeshRenderer>().bounds.size.y / 2;

		ResetDelay();
		StartCoroutine(nameof(BallGenerator));
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.timeScale == 0)
		{
			isActive = false;
		}
		else
		{
			isActive = true;
		}
	}

	/// <summary>
	/// Sets spawn delay between 1 and 2 seconds
	/// </summary>
	void ResetDelay()
	{
		spawnDelay = Random.Range(spawnMin, spawnMax);
	}

	/// <summary>
	/// After random time, ball is spawned in above screen at random X coordinate
	/// </summary>
	/// <returns>Waits between 1 and 2 seconds (dependent on spawnDelay value)</returns>
	IEnumerator BallGenerator()
	{
		yield return new WaitForSeconds(spawnDelay);

		if (isActive)
		{
			float randomX = Random.Range(screenBounds.x - objectWidth, screenBounds.x * -1 + objectWidth);
			float spawnY = (screenBounds.y + objectHeight) + 5;

			Instantiate(ballPrefab, new Vector3(randomX, spawnY, 0), ballPrefab.transform.rotation);
			ResetDelay();
		}

		StartCoroutine(nameof(BallGenerator));
	}
}
