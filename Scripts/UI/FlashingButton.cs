using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashingButton : MonoBehaviour
{
	public float step = 0.01f;
	Image FlashImage;

	void Start ()
	{
		FlashImage = GetComponent<Image> ();
	}

	void Update ()
	{
		// Alphaが0 または 1になったら増減値を反転
		if (FlashImage.color.a < 0 || FlashImage.color.a > 1)
		{
			step *= -1f;
		}
		// Alpha値を増減させてセット
		FlashImage.color = new Color (255, 255, 255, FlashImage.color.a + step);
	}
}
