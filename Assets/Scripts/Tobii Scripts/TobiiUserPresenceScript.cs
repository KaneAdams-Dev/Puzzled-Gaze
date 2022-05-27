using UnityEngine;
using Tobii.Gaming;

/// <summary>
/// Pauses the game when the Eye-tracker can't detect eyes
/// Created by: Kane Adams
/// </summary>
public class TobiiUserPresenceScript : MonoBehaviour
{
	[SerializeField] private GameObject pauseScreen;

	private SettingManager SM;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SM.IsTobiiActive)
		{
			if (TobiiAPI.GetUserPresence().IsUserPresent())
			{
				Time.timeScale = 1f;
				pauseScreen.SetActive(false);
			}
			else
			{
				Time.timeScale = 0f;
				pauseScreen.SetActive(true);
			}
		}
		else
		{
			pauseScreen.SetActive(false);
		}
	}
}
