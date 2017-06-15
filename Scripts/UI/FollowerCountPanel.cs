using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowerCountPanel: MonoBehaviour
{
	public Text followerCountText;

	void Start ()
	{
		UpdateFollowerCount ();
	}

	public void UpdateFollowerCount ()
	{
		followerCountText.text = " ×" + FollowerController.s_FollowerCount;
	}
}
