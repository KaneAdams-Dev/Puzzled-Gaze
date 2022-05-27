using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls customisation of bubble visual.
/// Created by: Kane Adams
/// </summary>
public class TobiiBubbleCustomisation : MonoBehaviour
{
	[Header("Referenced Scripts")]
	private SettingManager SM;

	[Header("Bubble GameObjects")]
	[SerializeField] private SpriteRenderer bubbleVisual;
	[SerializeField] private Image bubblePreview;
	
	[Header("Bubble Customisation Settings")]
	[SerializeField] private Slider bubbleRedSlider;
	[SerializeField] private Slider bubbleGreenSlider;
	[SerializeField] private Slider bubbleBlueSlider;
	[SerializeField] private Slider bubbleAlphaSlider;

	private void Awake()
	{
		SM = FindObjectOfType<SettingManager>();
	}

	// Start is called before the first frame update
	void Start()
	{
		bubbleVisual.color = new Color(SM.BubbleRed, SM.BubbleGreen, SM.BubbleBlue, SM.BubbleAlpha);
		bubblePreview.color = new Color(SM.BubbleRed, SM.BubbleGreen, SM.BubbleBlue, SM.BubbleAlpha);
		
		bubbleRedSlider.value = SM.BubbleRed;
		bubbleGreenSlider.value = SM.BubbleGreen;
		bubbleBlueSlider.value = SM.BubbleBlue;
		bubbleAlphaSlider.value = SM.BubbleAlpha;
	}

	// Update is called once per frame
	void Update()
	{
		SM.BubbleRed = bubbleVisual.color.r;
		SM.BubbleGreen = bubbleVisual.color.g;
		SM.BubbleBlue = bubbleVisual.color.b;
		SM.BubbleAlpha = bubbleVisual.color.a;
	}

	/// <summary>
	/// As red slider increases, more red is added to bubble
	/// </summary>
	/// <param name="a_value">New slider value</param>
	public void ChangeBubbleRed(float a_value)
	{
		bubbleVisual.color = new Color(a_value, bubbleVisual.color.g, bubbleVisual.color.b, bubbleVisual.color.a);
		bubblePreview.color = new Color(a_value, bubblePreview.color.g, bubblePreview.color.b, bubblePreview.color.a);

		SM.SaveSettings();
	}

	/// <summary>
	/// As green slider increases, more green is added to bubble
	/// </summary>
	/// <param name="a_value">New slider value</param>
	public void ChangeBubbleGreen(float a_value)
	{
		bubbleVisual.color = new Color(bubbleVisual.color.r, a_value, bubbleVisual.color.b, bubbleVisual.color.a);
		bubblePreview.color = new Color(bubbleVisual.color.r, a_value, bubblePreview.color.b, bubblePreview.color.a);

		SM.SaveSettings();
	}

	/// <summary>
	/// As blue slider increases, more blue is added to bubble
	/// </summary>
	/// <param name="a_value">New slider value</param>
	public void ChangeBubbleBlue(float a_value)
	{
		bubbleVisual.color = new Color(bubbleVisual.color.r, bubbleVisual.color.g, a_value, bubbleVisual.color.a);
		bubblePreview.color = new Color(bubbleVisual.color.r, bubblePreview.color.g, a_value, bubblePreview.color.a);

		SM.SaveSettings();
	}

	/// <summary>
	/// As alpha slider increases, bubble becomes more visible
	/// </summary>
	/// <param name="a_value">New slider value</param>
	public void ChangeBubbleAlpha(float a_value)
	{
		bubbleVisual.color = new Color(bubbleVisual.color.r, bubbleVisual.color.g, bubbleVisual.color.b, a_value);
		bubblePreview.color = new Color(bubbleVisual.color.r, bubblePreview.color.g, bubblePreview.color.b, a_value);

		SM.SaveSettings();
	}
}
