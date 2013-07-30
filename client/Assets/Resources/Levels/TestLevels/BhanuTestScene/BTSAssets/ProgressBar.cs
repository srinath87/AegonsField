using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	private UISlider slider;
	
	private float maxWidth;
	
	void Awake()
	{
		slider = GetComponent<UISlider>();
		
		if(slider == null)
		{
			Debug.Log ("Could not find the UISlider Component");
			return;
		}
		
		maxWidth = slider.sliderValue;
	}
	
	void Start()
	{
		
	}
	
	void Update()
	{
		UpdateDisplay (0.1f);	
	}
	
	public void UpdateDisplay(float x)
	{
		if(x < 0)
		{
			x = 0;
		}
		
		else if(x > 1)
		{
			x = 1;	
		}
		
		slider.sliderValue = slider.sliderValue + x;
	}
}
