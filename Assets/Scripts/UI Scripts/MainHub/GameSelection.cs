using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls what game is showing to the player.
/// Created by: Kane Adams
/// </summary>
public class GameSelection : MonoBehaviour
{
	[SerializeField] private Button prevButton;
	[SerializeField] private Button nextButton;

	private int currentGameCard;

	private void Start()
	{
		SelectGameCard(0);
	}

	/// <summary>
	/// Changes which game can be selected
	/// </summary>
	/// <param name="a_index">Which game from the list of GameObject children</param>
	private void SelectGameCard(int a_index)
	{
		// Disables button if no more games are available
		prevButton.interactable = (a_index != 0);
		nextButton.interactable = (a_index != transform.childCount - 1);

		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == a_index);
		}
	}

	/// <summary>
	/// Determines whether next of previous game is to be shown
	/// </summary>
	/// <param name="a_change">Is it the previous or next game</param>
	public void ChangeGameCard(int a_change)
	{
		currentGameCard += a_change;
		SelectGameCard(currentGameCard);
	}

	/// <summary>
	/// When player clicks button, it opens that scene
	/// </summary>
	/// <param name="a_gameName">The Scene the player wants to open</param>
	public void ChooseGame(string a_gameName)
	{
		SceneManager.LoadScene(a_gameName);
	}
}
