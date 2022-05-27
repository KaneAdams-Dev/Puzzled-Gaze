using UnityEngine;
using Tobii.Gaming;

/// <summary>
/// Contains the player's settings and allow personalisation between scenes
/// </summary>
public class SettingManager : MonoBehaviour
{
	// Tobii related settings
	private bool isTobiiActive;
	private bool isTobiiBubble;

	// Tobii bubble customisation
	private float bubbleRed;
	private float bubbleGreen;
	private float bubbleBlue;
	private float bubbleAlpha;

	// UI customisation
	private float buttonScale;

	public bool IsTobiiActive
	{
		get { return isTobiiActive; }
		set { isTobiiActive = value; }
	}

	public bool IsTobiiBubble
	{
		get { return isTobiiBubble; }
		set { isTobiiBubble = value; }
	}

	public float BubbleRed
	{
		get { return bubbleRed; }
		set { bubbleRed = value; }
	}

	public float BubbleGreen
	{
		get { return bubbleGreen; }
		set { bubbleGreen = value; }
	}

	public float BubbleBlue
	{
		get { return bubbleBlue; }
		set { bubbleBlue = value; }
	}

	public float BubbleAlpha
	{
		get { return bubbleAlpha; }
		set { bubbleAlpha = value; }
	}

	public float ButtonScale
	{
		get { return buttonScale; }
		set { buttonScale = value; }
	}

	private static SettingManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("Found more than one Setting Manager in Scene");
		}

		LoadSettings();
	}

	// Update is called once per frame
	void Update()
	{
		if (!TobiiAPI.IsConnected)
		{
			isTobiiActive = false;
		}

		if (!isTobiiActive)
		{
			isTobiiBubble = false;
		}

		SaveSettings();
	}

	/// <summary>
	/// Saves player preferences
	/// </summary>
	public void SaveSettings()
	{
		PlayerPrefs.SetInt("ISTOBIIACTIVE", (isTobiiActive ? 1 : 0));
		PlayerPrefs.SetInt("ISTOBIIBUBBLE", (isTobiiBubble ? 1 : 0));

		PlayerPrefs.SetFloat("BUBBLERED", bubbleRed);
		PlayerPrefs.SetFloat("BUBBLEGREEN", bubbleGreen);
		PlayerPrefs.SetFloat("BUBBLEBLUE", bubbleBlue);
		PlayerPrefs.SetFloat("BUBBLEALPHA", bubbleAlpha);

		PlayerPrefs.SetFloat("BUTTONSCALE", ButtonScale);
	}

	/// <summary>
	/// Loads player settings when scene starts
	/// </summary>
	void LoadSettings()
	{
		isTobiiActive = (PlayerPrefs.GetInt("ISTOBIIACTIVE") != 0);
		isTobiiBubble = (PlayerPrefs.GetInt("ISTOBIIBUBBLE") != 0);

		bubbleRed = PlayerPrefs.GetFloat("BUBBLERED");
		bubbleGreen = PlayerPrefs.GetFloat("BUBBLEGREEN");
		bubbleBlue = PlayerPrefs.GetFloat("BUBBLEBLUE");
		bubbleAlpha = PlayerPrefs.GetFloat("BUBBLEALPHA");

		buttonScale = PlayerPrefs.GetFloat("BUTTONSCALE");
	}
}
