using UnityEngine;
using System.Collections;

public class SetVolumes : MonoBehaviour
{
	[SerializeField]
	UnityEngine.Audio.AudioMixer mixer = null;

	public float BGMVolume
	{
		set{ mixer.SetFloat ("BGM_Volume", Mathf.Lerp (-80, -10, value)); }
	}

	public float JingleVolume
	{
		set{ mixer.SetFloat ("JingleVolume", Mathf.Lerp (-80, -3, value)); }
	}

	public float SEVolume
	{
		set{ mixer.SetFloat ("SE_Volume", Mathf.Lerp (-80, -3, value)); }
	}

	public float SE_LowVolume
	{
		set{ mixer.SetFloat ("SE_LowVolume", Mathf.Lerp (-80, 10, value)); }
	}
}