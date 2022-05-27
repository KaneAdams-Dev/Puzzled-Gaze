using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Tobii.Gaming;

/// <summary>
/// Controls the Simon Says game.
/// Player readies up by looking at center of screen 
/// and then has to stare at the correct combination of colours to score.
/// 
/// Created by: Kane Adams
/// </summary>
public class SimonSaysScript : MonoBehaviour, IPointerEnterHandler
{
	[Header("Referenced Scripts")]
	[SerializeField] private GameOverScript GOS;
	private SettingManager SM;

	[Header("Simon Says response buttons")]
	[SerializeField] private Button[] colourButtons;

	private Image readyUp;
	private Image bgPanel;

	private List<int> colourCombo;
	private List<int> playerChoice;

	public bool canReadyUp;
	private int level;

	public bool isGameOver;

	private TextMeshProUGUI lvlText;

	private GazeAware gazeAware;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();

		readyUp = GetComponent<Image>();
		gazeAware = GetComponent<GazeAware>();

		bgPanel = GameObject.Find("BGPanel").GetComponent<Image>();
		lvlText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
	}

	// Start is called before the first frame update
	void Start()
	{
		level = 0;
		isGameOver = false;

		NextRound();
	}

	// Update is called once per frame
	void Update()
	{
		if (isGameOver)
		{
			return;
		}

		// Debugging
		if (!canReadyUp)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				ColourClick(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				ColourClick(1);
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				ColourClick(2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				ColourClick(3);
			}
		}

		if (SM.IsTobiiActive)
		{
			// If looking at center of game, start round
			if (gazeAware.HasGazeFocus && canReadyUp)
			{
				readyUp.color = Color.green;
				canReadyUp = false;
				StartCoroutine(ShowPattern());
			}
		}
	}

	/// <summary>
	/// When player moves mouse pointer to center of screen, game begins
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (canReadyUp)
		{
			readyUp.color = Color.green;
			canReadyUp = false;
			StartCoroutine(ShowPattern());
		}
	}

	/// <summary>
	/// Shows random colour in pattern and adds to list
	/// </summary>
	/// <returns>Holds colour for 1 second, waits 0.5 seconds before next colour</returns>
	IEnumerator ShowPattern()
	{
		if (colourCombo.Count < level)
		{
			int newColour = Random.Range(0, 4);
			colourCombo.Add(newColour);

			switch (newColour)
			{
				case 0:
					bgPanel.color = Color.red;
					break;

				case 1:
					bgPanel.color = Color.green;
					break;

				case 2:
					bgPanel.color = Color.yellow;
					break;

				case 3:
					bgPanel.color = Color.blue;
					break;

				default:
					break;
			}
		}

		yield return new WaitForSeconds(1);
		bgPanel.color = Color.white;        // resets to white to see colour changes

		// If there is another colour in pattern, show next colour after time.
		// Otherwise, allow player controls
		if (colourCombo.Count < level)
		{
			yield return new WaitForSeconds(0.5f);
			StartCoroutine(ShowPattern());
		}
		else
		{
			readyUp.color = Color.grey;
			ActivateButtons();
		}
	}

	/// <summary>
	/// Allows buttons to be clicked by player
	/// </summary>
	void ActivateButtons()
	{
		foreach (Button button in colourButtons)
		{
			button.enabled = true;
		}
	}

	/// <summary>
	/// Collects data on what button player clicked
	/// </summary>
	/// <param name="a_chosenColour">Colour player clicked</param>
	public void ColourClick(int a_chosenColour)
	{
		if (playerChoice.Count < level)
		{
			playerChoice.Add(a_chosenColour);

			// If player clicks a wrong colour, game ends
			if (a_chosenColour != colourCombo[playerChoice.Count - 1])
			{
				isGameOver = true;
				foreach (Button button in colourButtons)
				{
					button.enabled = false;
				}
				GOS.GameOver();
			}
		}

		// If player clicked right amount of colours for this round, begin next round
		if (playerChoice.Count == level)
		{
			NextRound();
		}
	}

	/// <summary>
	/// Starts the next round to play.
	/// If it is level 4, show win screen
	/// </summary>
	void NextRound()
	{
		if (level < 4)
		{
			// Changes round
			level++;
			lvlText.text = "Round: " + level;

			canReadyUp = true;
			readyUp.color = Color.red;

			// Resets lists
			colourCombo = new List<int>(level);
			playerChoice = new List<int>(level);

			// Disables buttons to prevent clicks when not ready
			foreach (Button button in colourButtons)
			{
				button.enabled = false;
			}
		}
		else
		{
			isGameOver = true;
			GOS.GameWin();
		}
	}
}
