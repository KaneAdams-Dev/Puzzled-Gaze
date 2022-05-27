using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls when different GameOver screens are shown.
/// Gives the options to exit or retry.
/// Created by: Kane Adams
/// </summary>
public class GameOverScript : MonoBehaviour
{
	private GameObject gameOverPanel;
	private GameObject winPanel;

	private void Awake()
	{
		gameOverPanel = GameObject.Find("GameOverPanel");
		winPanel = GameObject.Find("WinPanel");
	}

	// Start is called before the first frame update
	void Start()
	{
		gameOverPanel.SetActive(false);
		winPanel.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Shows the GameOver screen and player's choices
	/// </summary>
	public void GameOver()
	{
		Time.timeScale = 0f;

		gameOverPanel.SetActive(true);
		winPanel.SetActive(false);
	}

	/// <summary>
	/// Shows the Win screen and allows player to leave game
	/// </summary>
	public void GameWin()
	{
		Time.timeScale = 0f;

		winPanel.SetActive(true);
		gameOverPanel.SetActive(false);
	}

	/// <summary>
	/// Player can restart game from Round 1
	/// </summary>
	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// Player returns to Main Menu Scene
	/// </summary>
	public void ExitGame()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
