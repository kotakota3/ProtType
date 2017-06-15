using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject VirtualPad;
	public AudioClip gameOver_Clip;
	public AudioClip goalClip;
	public SceneController sceneCtrl;
	public GraphicRaycaster graphicRaycaster;

	public List<int> IdList = new List<int> ();

	public void GameOver ()
	{
		Invoke ("DisplayRetryPanel", 3.0f);
		SoundManager.instance.StopBGM ();
	}

	void DisplayRetryPanel ()
	{
		VirtualPad.SetActive (false);
		SoundManager.instance.PlayJingle (gameOver_Clip);
		SceneManager.LoadScene ("Stage");
	}

	//	void Goal ()
	//	{
	//		graphicRaycaster.enabled = false;
	//		VirtualPad.SetActive (false);
	//		SoundManager.instance.StopBGM ();
	//		SoundManager.instance.PlayJingle (goalClip);
	//		sceneCtrl.GoalFade ();
	//	}
}
