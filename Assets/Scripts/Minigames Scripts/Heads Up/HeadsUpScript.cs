using UnityEngine;
using Tobii.Gaming;
using TMPro;

/// <summary>
/// Controls player input for Heads Up.
/// Created by: Kane Adams
/// </summary>
public class HeadsUpScript : MonoBehaviour
{
	[Header("Referenced Scripts")]
	[SerializeField] private GameOverScript GOS;
	private SettingManager SM;

	private Transform currentPos;
	private Vector3 tempPos;

	private int score;
	[SerializeField] private TextMeshProUGUI scoreText;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();
	}

	// Start is called before the first frame update
	void Start()
	{
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		currentPos = gameObject.transform;
		score = 0;
		scoreText.text = "Score: " + score;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.timeScale != 0)
		{
			if (SM.IsTobiiActive)
			{
				MoveTobii();
			}
			else
			{
				MoveMouse();
			}

		}
	}

	/// <summary>
	/// Basket moves towards player's eye position (when eye-tracking active)
	/// </summary>
	void MoveTobii()
	{
		GazePoint gazePoint = TobiiAPI.GetGazePoint();
		Vector3 gazePos = Camera.main.ScreenToWorldPoint(gazePoint.Screen);
		tempPos = new Vector3(gazePos.x * (1.0f - 0.15f), currentPos.position.y, currentPos.position.z);

		currentPos.position = tempPos;
	}

	/// <summary>
	/// Basket moves towards mouse position
	/// </summary>
	void MoveMouse()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		currentPos.position = new Vector3(mousePos.x, currentPos.position.y, currentPos.position.z);
	}

	/// <summary>
	/// If touches red ball, game ends.
	/// If touches yellow ball, score added
	/// </summary>
	/// <param name="a_collision">GameObject basket touched</param>
	private void OnCollisionEnter(Collision a_collision)
	{
		if (a_collision.gameObject.CompareTag("Bad"))
		{
			GOS.GameOver();
			Destroy(gameObject);
		}
		else if (a_collision.gameObject.CompareTag("Collectable"))
		{
			score++;
			scoreText.text = "Score: " + score;

			Destroy(a_collision.gameObject);

			if (score >= 5)
			{
				GOS.GameWin();
				Destroy(gameObject);
			}
		}
	}
}
