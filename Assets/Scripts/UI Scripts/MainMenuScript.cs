using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the buttons in Main Menu Scene.
/// Created by: Kane Adams
/// </summary>
public class MainMenuScript : MonoBehaviour
{
	[Header("Referenced Scripts")]
	SettingManager SM;

	[Header("Eye-tracking Settings")]
	[SerializeField] private Toggle tobiiActiveToggle;
	[SerializeField] private Toggle tobiiBubbleToggle;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();
	}

	// Start is called before the first frame update
	void Start()
	{
		tobiiActiveToggle.isOn = SM.IsTobiiActive;
		tobiiBubbleToggle.isOn = SM.IsTobiiBubble;
	}

	// Update is called once per frame
	void Update()
	{
		tobiiActiveToggle.isOn = SM.IsTobiiActive;

		if (!tobiiActiveToggle.isOn)
		{
			tobiiBubbleToggle.isOn = false;
		}
		else
		{
			tobiiBubbleToggle.isOn = SM.IsTobiiBubble;
		}
	}

	/// <summary>
	/// Loads the level
	/// </summary>
	public void PlayGame()
	{
		SM.SaveSettings();

		SceneManager.LoadScene("MainHub");
	}

	/// <summary>
	/// Closes game
	/// </summary>
	public void ExitGame()
	{
		SM.SaveSettings();

		Application.Quit();
	}
}
