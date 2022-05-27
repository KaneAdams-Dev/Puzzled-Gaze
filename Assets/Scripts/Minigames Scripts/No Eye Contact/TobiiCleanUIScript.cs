using UnityEngine;
using Tobii.Gaming;

/// <summary>
/// Controls UI opaqueness, dependent on whether player looking at UI.
/// Created by: Kane Adams
/// </summary>
public class TobiiCleanUIScript : MonoBehaviour
{
	private SettingManager SM;

	private GazeAware gazeAware;
	private CanvasGroup uiBar;

	private float uiAlpha = 0.1f;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();

		gazeAware = GetComponent<GazeAware>();
		uiBar = GetComponent<CanvasGroup>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SM.IsTobiiActive)
		{
			if (gazeAware.HasGazeFocus)
			{
				uiBar.alpha = 1;
			}
			else
			{
				uiBar.alpha = uiAlpha;
			}
		}
		else
		{
			uiBar.alpha = 1;
		}
	}
}
