using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
	public AudioClip buttonClip;
	public AudioClip startClip;
	public AudioClip stageClip;
	public float loadSpeed;
	public float fadeTime;

	GraphicRaycaster graphicRaycaster;
	Image fadeObj;
	float startTime;

	Color alpha;
	string fadeStart;

	void Awake ()
	{
		fadeObj = GetComponent<Image> ();
		startTime = Time.time;
		fadeStart = "FadeIn";
		graphicRaycaster = transform.root.GetComponent<GraphicRaycaster> ();
	}

	void Update ()
	{
		switch (fadeStart)
		{
			case "FadeIn":
				alpha.a = 1.0f - (Time.time - startTime) / fadeTime;
				fadeObj.color = new Color (0, 0, 0, alpha.a);
				break;
			case "FadeOut":
				alpha.a = (Time.time - startTime) / fadeTime;
				fadeObj.color = new Color (0, 0, 0, alpha.a);
				break;
		}
	}

	void LoadStageScene ()
	{
		SceneManager.LoadScene ("Stage");
	}

	public void LoadStageSceneButton ()
	{
		fadeStart = "FadeOut";
		startTime = Time.time;
		graphicRaycaster.enabled = false;
		Invoke ("LoadStageScene", loadSpeed);
	}
}