using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;
	public AudioSource seSource;
	public AudioSource musicSource;
	public AudioSource jingleSource;
	public AudioSource threeD_Source;
	public AudioClip titleClip;
	public AudioClip stageClip;

	public AudioClip resultClip;
	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
		{
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	void Start ()
	{
		SceneManager.sceneLoaded += SelectBGM;
	}

	void SelectBGM (Scene scene, LoadSceneMode sceneMode)
	{
		switch (scene.name)
		{
			case "Title":
				PlayJingle (titleClip);
				break;
			case "Stage":
				SelectBGM (stageClip);
				break;
			case "Result": 
				SelectBGM (resultClip);
				break;
		}
	}

	public void PlaySingle (AudioClip clip)
	{
		seSource.clip = clip;
		seSource.Play ();
	}

	public void PlayThreeD (AudioClip clip)
	{
		threeD_Source.clip = clip;
		threeD_Source.Play ();
	}

	public void PlayJingle (AudioClip clip)
	{
		jingleSource.clip = clip;
		jingleSource.Play ();
	}

	void SelectBGM (AudioClip clip)
	{
		musicSource.clip = clip;
		musicSource.Play ();
	}

	public void StopBGM ()
	{
		musicSource.Stop ();
		musicSource.clip = null;
	}

	public void RandomizeSfx (params AudioClip[] clips)
	{
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);
		seSource.pitch = randomPitch;
		seSource.clip = clips [randomIndex];
		seSource.Play ();
	}
}