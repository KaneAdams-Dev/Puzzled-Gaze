using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

/// <summary>
/// Controls the No Eye Contact mini game.
/// Player has to look at the robot without the robot knowing.
/// Created by: Kane Adams
/// </summary>
public class NoEyeContactScript : MonoBehaviour
{
	[Header("Referenced Scripts")]
	[SerializeField] private GameOverScript GOS;
	private SettingManager SM;

	private GameObject botHappy;
	private GameObject botAngry;
	private GameObject botClosedEyes;

	private Slider rageBar;
	private Slider xpBar;

	[Header("Ragebar properties")]
	public Gradient rageGradient;
	[SerializeField] private Image ragefill;

	private bool isAwake;
	private bool isStaring;
	private bool isGameOver;

	private float totalXP;
	private float totalRage;

	private float countdownTimer;
	private float time;

	private GazeAware gazeAware;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();

		gazeAware = GetComponent<GazeAware>();

		botHappy = GameObject.Find("RobotEyesHappy");
		botAngry = GameObject.Find("RobotEyesAngry");
		botClosedEyes = GameObject.Find("RobotClosedEyes");

		rageBar = GameObject.Find("RageBar").GetComponent<Slider>();
		xpBar = GameObject.Find("XPBar").GetComponent<Slider>();
	}

	// Start is called before the first frame update
	void Start()
	{
		xpBar.value = 0;
		rageBar.value = 0;
		isGameOver = false;

		OpenEyes();
	}

	// Update is called once per frame
	void Update()
	{
		if (isGameOver)
		{
			return;
		}

		if (SM.IsTobiiActive)
		{
			if (gazeAware.HasGazeFocus)
			{
				isStaring = true;
			}
			else
			{
				isStaring = false;
			}
		}

		if (isStaring)
		{
			totalXP += 0.05f * Time.deltaTime;
			if (isAwake)
			{
				botAngry.SetActive(true);
				botHappy.SetActive(false);
				totalRage += 0.5f * Time.deltaTime;
			}
		}
		else
		{
			totalRage -= 0.05f * Time.deltaTime;
			if (totalRage <= 0)
			{
				totalRage = 0;
			}

			if (isAwake)
			{
				botAngry.SetActive(false);
				botHappy.SetActive(true);
			}
		}

		if (totalXP >= 1)
		{
			GOS.GameWin();
			isGameOver = true;
		}
		else if (totalRage >= 1)
		{
			GOS.GameOver();
			isGameOver = true;
		}

		UpdateTimer();
		UpdateSliders();
	}

	/// <summary>
	/// After set time, robot closes eyes, safest time to stare
	/// </summary>
	void CloseEyes()
	{
		botClosedEyes.SetActive(true);
		botAngry.SetActive(false);
		botHappy.SetActive(false);
		isAwake = false;

		time = Random.Range(10, 15);
		countdownTimer = time;
	}

	/// <summary>
	/// After set time, robot opens eyes and can catch player staring
	/// </summary>
	void OpenEyes()
	{
		botAngry.SetActive(false);
		botHappy.SetActive(true);
		botClosedEyes.SetActive(false);

		isAwake = true;

		time = Random.Range(1, 5);
		countdownTimer = time;
	}

	/// <summary>
	/// Counts down timer then opens or closes robot's eyes
	/// </summary>
	void UpdateTimer()
	{
		countdownTimer -= Time.deltaTime;
		if (countdownTimer <= 0)
		{
			if (!isAwake)
			{
				OpenEyes();
			}
			else
			{
				CloseEyes();
			}
		}
	}

	/// <summary>
	/// Changes fill amount of sliders dependent on float value
	/// </summary>
	void UpdateSliders()
	{
		xpBar.value = totalXP;
		rageBar.value = totalRage;

		ragefill.color = rageGradient.Evaluate(rageBar.value);  //changes colour of slider to warn player
	}

	/// <summary>
	/// Player's mouse is starting at robot
	/// </summary>
	private void OnMouseEnter()
	{
		isStaring = true;
	}

	/// <summary>
	/// Player's mouse is looking away from robot
	/// </summary>
	private void OnMouseExit()
	{
		isStaring = false;
	}
}
