using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject VirtualPad;
	public AudioClip gameOver_Clip;
	public AudioClip goal_Clip;
	public SceneController sceneCtrl;
	public GraphicRaycaster graphicRaycaster;

	public List<int> IdList = new List<int> ();

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape))
		{
			ShutDown ();
		}
	}

	public void GameOver ()
	{
		VirtualPad.SetActive (false);
		SoundManager.instance.StopBGM ();
		SoundManager.instance.PlayJingle (gameOver_Clip);
		Invoke ("DisplayRetryPanel", 3.0f);
	}

	void DisplayRetryPanel ()
	{
		SceneManager.LoadScene ("Stage");
	}

	public void Goal ()
	{
		graphicRaycaster.enabled = false;
		SoundManager.instance.PlayJingle (goal_Clip);
		Invoke ("DisplayRetryPanel", 8.0f);
	}

	public void ShutDown ()
	{
		Application.Quit ();
	}
}
