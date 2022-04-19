using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tobii.Gaming;

/// <summary>
/// Controls the buttons in Main Menu Scene.
/// Created by: Kane Adams
/// </summary>
public class MainMenuScript : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Loads the level
	/// </summary>
	public void PlayGame()
	{
		Debug.Log("Load Scene");
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	/// <summary>
	/// Closes game
	/// </summary>
	public void ExitGame()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}
}
