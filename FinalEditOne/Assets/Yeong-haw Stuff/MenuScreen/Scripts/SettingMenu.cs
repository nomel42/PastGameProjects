using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour {

	public AudioMixer audioMixer;

	Resolution[] resolutions;

	public Dropdown resolutionDropdown;

	void Start(){
		GameObject FStemp = GameObject.Find("FSbutton");
		Toggle FS = FStemp.GetComponent<Toggle> ();
		FS.isOn =  Screen.fullScreen;

		GameObject DDSQtemp = GameObject.Find("Graphics");
		Dropdown DDSQ = DDSQtemp.GetComponent<Dropdown> ();
		DDSQ.value = QualitySettings.GetQualityLevel ();


		GameObject MVStemp = GameObject.Find("MVSSlider");
		GameObject SEtemp = GameObject.Find("SESlider");
		GameObject BGVtemp = GameObject.Find("BGVSlider");
		Slider MVS = MVStemp.GetComponent<Slider> ();
		Slider SE = SEtemp.GetComponent<Slider> ();
		Slider BGV = BGVtemp.GetComponent<Slider> ();

		MVS.value = GetMasterVolume ();
		SE.value = GetSFXVolume ();
		BGV.value = GetBGVolume ();

		resolutions = Screen.resolutions;

		resolutionDropdown.ClearOptions ();
		List<string> optionsRes = new List<string> ();


		int currentResolutionIndex = 0;
		for(int i = 0; i < resolutions.Length; i++)
		{
			string optionsResString = resolutions[i].width + " x " + resolutions[i].height;
			optionsRes.Add(optionsResString);

			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) 
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(optionsRes);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void back(){
		SceneManager.LoadScene ("scene_1");	
	}

	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetMasterVolume(float volume)
	{
		audioMixer.SetFloat ("MVolume", volume);
	}

	public float GetMasterVolume()
	{
		float volume;
		audioMixer.GetFloat ("MVolume", out volume);
		return volume;
	}

	public void SetSFXVolume(float volume)
	{
		audioMixer.SetFloat ("SFXVolume", volume);
	}

	public float GetSFXVolume()
	{
		float volume;
		audioMixer.GetFloat ("SFXVolume", out volume);
		return volume;
	}

	public void SetBGVolume(float volume)
	{
		audioMixer.SetFloat ("BGVolume", volume);
	}

	public float GetBGVolume()
	{
		float volume;
		audioMixer.GetFloat ("BGVolume", out volume);
		return volume;
	}

	public void SetQuality(int QIndex){
		QualitySettings.SetQualityLevel (QIndex);
	}

	public void SetFullscreen(bool isFullscreen){
		Screen.fullScreen = isFullscreen;
	}
}
