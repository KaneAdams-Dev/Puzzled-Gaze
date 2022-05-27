using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Controls interaction with button UI.
/// Created by: Kane Adams
/// </summary>
public class TobiiUIScript : MonoBehaviour
{
	[Header("Referenced Scripts")]
	SettingManager SM;

	private GazeAware gazeAware;
	private Image uiMeshRenderer;
	private Button button;
	private Toggle toggle;

	[Header("On Hover Colouring")]
	public Color highlightColour;	// Can be edited within Inspector
	private Color defaultColour = Color.white;
	private Color lerpColour;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();

		gazeAware = GetComponent<GazeAware>();
		uiMeshRenderer = GetComponent<Image>();
		button = GetComponent<Button>();
		toggle = GetComponent<Toggle>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SM.IsTobiiActive)
		{
			if (uiMeshRenderer != null)
			{
				if (uiMeshRenderer.color != lerpColour)
				{
					uiMeshRenderer.color = lerpColour;
				}
			}

			if (gazeAware.HasGazeFocus)
			{
				SetLerpColour(highlightColour);
				
				if (Input.GetKeyDown(KeyCode.Space))
				{
					if (button != null && button.interactable)
					{
						button.onClick.Invoke();    // Clicks UI button
					}
					if (toggle != null)
					{
						toggle.isOn = !toggle.isOn;
					}
				}
			}
			else
			{
				SetLerpColour(defaultColour);
			}
		}
	}

	/// <summary>
	/// Changes button colour
	/// </summary>
	/// <param name="a_newColour">Either select colour or button default</param>
	public void SetLerpColour(Color a_newColour)
	{
		lerpColour = a_newColour;
	}
}
