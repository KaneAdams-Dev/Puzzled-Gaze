using UnityEngine;
using Tobii.Gaming;

/// <summary>
/// Controls GazeUI to create visuals for player to see where exactly they are looking.
/// Created by: Kane Adams
/// </summary>
public class TobiiGazePointScript : MonoBehaviour
{
	[Header("Referenced Scripts")]
	private SettingManager SM;

	[Tooltip("Distance from screen")]
	private float cursorDistance = 10f;

	[Range(0.1f, 1f), Tooltip("Smoothness of Tobii cursor movement")]
	private float smoothing = 0.15f;

	private GazePoint prevGazePoint = GazePoint.Invalid;

	private SpriteRenderer gazeBubble;

	private bool useFilter;
	private bool hasPreviousPoint;
	private Vector3 prevPoint;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();

		gazeBubble = GetComponent<SpriteRenderer>();
	}

	// Start is called before the first frame update
	void Start()
	{
		useFilter = SM.IsTobiiBubble;
		ToggleGazeBubbleVisibility();

		gazeBubble.color = new Color(SM.BubbleRed, SM.BubbleGreen, SM.BubbleBlue, SM.BubbleAlpha);
	}

	// Update is called once per frame
	void Update()
	{
		GazePoint gazePoint = TobiiAPI.GetGazePoint();

		if (gazePoint.IsRecent() && gazePoint.Timestamp > (prevGazePoint.Timestamp + float.Epsilon))
		{
			if (useFilter)
			{
				UpdateGazeBubblePosition(gazePoint);
			}

			prevGazePoint = gazePoint;
		}

		if (useFilter)
		{
			if (Time.timeScale == 0f)
			{
				gazeBubble.enabled = false;
			}
			else
			{
				gazeBubble.enabled = true;
			}
		}

		// Debugging
		if (Input.GetKeyDown(KeyCode.K) && SM.IsTobiiActive)
		{
			SM.IsTobiiBubble = !SM.IsTobiiBubble;
			useFilter = SM.IsTobiiBubble;
		}

		useFilter = SM.IsTobiiBubble;
		ToggleGazeBubbleVisibility();
	}

	/// <summary>
	/// Toggles gaze bubble to be on or off
	/// </summary>
	private void ToggleGazeBubbleVisibility()
	{
		gazeBubble.enabled = useFilter;
	}

	/// <summary>
	/// Moves gaze bubble to place that player is looking at
	/// </summary>
	/// <param name="a_gazePoint">Point player is looking at</param>
	private void UpdateGazeBubblePosition(GazePoint a_gazePoint)
	{
		Vector3 gazePointInWorld = ProjectToPlaneInWorld(a_gazePoint);
		transform.position = Smoothify(gazePointInWorld);
	}

	/// <summary>
	/// Converts player's eye position to camera coordinates
	/// </summary>
	/// <param name="a_gazePoint">Coordinate of player eyes</param>
	/// <returns>Position in camera</returns>
	public Vector3 ProjectToPlaneInWorld(GazePoint a_gazePoint)
	{
		Vector3 gazeOnScreen = a_gazePoint.Screen;
		gazeOnScreen += (transform.forward * cursorDistance);
		return Camera.main.ScreenToWorldPoint(gazeOnScreen);
	}

	/// <summary>
	/// Smooths UI movement to be more steady (due to constant eye movement)
	/// </summary>
	/// <param name="a_point">Eye position in scene</param>
	/// <returns>New UI point</returns>
	private Vector3 Smoothify(Vector3 a_point)
	{
		if (!hasPreviousPoint)
		{
			prevPoint = a_point;
			hasPreviousPoint = true;
		}

		Vector3 smoothedPoint = new Vector3(
			a_point.x * (1.0f - smoothing) + prevPoint.x * smoothing,
			a_point.y * (1.0f - smoothing) + prevPoint.y * smoothing,
			a_point.z * (1.0f - smoothing) + prevPoint.z * smoothing
			);

		prevPoint = smoothedPoint;

		return smoothedPoint;
	}
}
