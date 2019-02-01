using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	GameObject audioTemp = GameObject.Find("audioOutPut");
	AudioSource thisAudio;

	void Start(){

		thisAudio = audioTemp.GetComponent<AudioSource>();
		if (!(thisAudio.isPlaying))
		{
			thisAudio.Play();
		}
	}

	void Update(){

		Time.timeScale = 1;
		AudioListener.pause = false;
		if (!(thisAudio.isPlaying))
		{
			thisAudio.Play();
		}
	}

	public void PlayGame(){
		SceneManager.LoadScene ("TheGame1");	
	}

	public void PlayTut(){
		SceneManager.LoadScene ("test");	
	}

	public void PlaySC(){
		SceneManager.LoadScene ("cutMovie");
	}

	public void OpenOption(){
		SceneManager.LoadScene ("scene_2");
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public void BackToCutScene()
	{
		SceneManager.LoadScene ("");
	}
}
