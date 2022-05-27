using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Controls colour selection in Simon Says with Eye-tracking.
/// Created by: Kane Adams
/// </summary>
public class TobiiColourSelectScript : MonoBehaviour
{ 
	private SimonSaysScript SSS;
	private SettingManager SM;

	private GazeAware gazeAware;

	private float counter;
	private float dwellTime = 0.5f;

	private bool isClicked;  // Prevents instantanious double-click (has to look away and then return for a second click)

	[SerializeField] private int colourSelect;

	private Color defaultColour;
	private Image buttonColour;

	private void Awake()
	{
		SSS = FindObjectOfType<SimonSaysScript>();
		SM = FindObjectOfType<SettingManager>();

		gazeAware = GetComponent<GazeAware>();
		buttonColour = GetComponent<Image>();
	}

	// Start is called before the first frame update
	void Start()
	{
		counter = 0;
		isClicked = false;
		defaultColour = gameObject.GetComponent<Image>().color;
	}

	// Update is called once per frame
	void Update()
	{
		if (SM.IsTobiiActive)
		{
			// Stops interactions if game has ended
			if (SSS.isGameOver)
			{
				buttonColour.color = defaultColour;
				return;
			}

			// If looking at button for set time, click button
			if (!SSS.canReadyUp && gameObject.GetComponent<Button>().IsActive() && gazeAware.HasGazeFocus)
			{
				counter += Time.deltaTime;
				if (counter >= dwellTime && !isClicked)
				{
					buttonColour.color = Color.grey;
					isClicked = true;
					SSS.ColourClick(colourSelect);
				}
			}
			else
			{
				counter = 0;
				isClicked = false;
				buttonColour.color = defaultColour;
			}
		}
	}
}
