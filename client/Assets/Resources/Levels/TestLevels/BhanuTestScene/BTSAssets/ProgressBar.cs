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
		StartCoroutine("UpdateProgressBar");
	}
	
	void Update()
	{
		
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
	
	IEnumerator UpdateProgressBar()
	{
		yield return new WaitForSeconds(0.07f);
		UpdateDisplay(0.1f);
		StartCoroutine("UpdateProgressBar");
		
	}
}
