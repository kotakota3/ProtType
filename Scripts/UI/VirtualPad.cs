using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VirtualPad : MonoBehaviour
{
	public GameObject barJoyStickSprite, baseJoyStickSprite;
	public float maxRadius;
	public Camera virtualPad_Cam;

	Vector2 _position;

	public Vector2 Position
	{
		get { return _position; }
	}

	void Start ()
	{
		_position = Vector2.zero;
	}

	void Update ()
	{
		Display ();
		Move ();
	}

	void Display ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			baseJoyStickSprite.SetActive (true);
			baseJoyStickSprite.transform.position = virtualPad_Cam.ScreenToWorldPoint (Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp (0))
		{
			baseJoyStickSprite.SetActive (false);
			_position = Vector2.zero;
		}
	}

	void Move ()
	{
		//表示されていなければ移動しない
		if (!baseJoyStickSprite.activeSelf)
			return;

		Vector3 touchPosition = virtualPad_Cam.ScreenToWorldPoint (Input.mousePosition);
		barJoyStickSprite.transform.position = touchPosition;
		
		float radius = Vector3.Distance (Vector3.zero, barJoyStickSprite.transform.localPosition);

		if (radius < maxRadius)
		{
			//角度
			float radian = CalcRadian (
				               Vector3.zero,
				               barJoyStickSprite.transform.localPosition
			               );

			Vector3 setVec = Vector3.zero;
			setVec.x = radius * Mathf.Cos (radian);
			setVec.y = radius * Mathf.Sin (radian);

			barJoyStickSprite.transform.localPosition = setVec;

			float deg = radian * Mathf.Rad2Deg - 90f;
			barJoyStickSprite.transform.rotation = Quaternion.Euler (0, 0, deg);

			//-1〜1に正規化
			float threshold = maxRadius * 0.1f;
			_position = threshold < radius ? new Vector2 (barJoyStickSprite.transform.localPosition.x / maxRadius, barJoyStickSprite.transform.localPosition.y / maxRadius) : Vector2.zero;
		}
		else
		{
			//角度
			float radian = CalcRadian (
				               Vector3.zero,
				               barJoyStickSprite.transform.localPosition
			               );

			Vector3 setVec = Vector3.zero;
			setVec.x = radius * Mathf.Cos (radian);
			setVec.y = radius * Mathf.Sin (radian);

			barJoyStickSprite.transform.localPosition = setVec;

			float deg = radian * Mathf.Rad2Deg - 90f;
			barJoyStickSprite.transform.rotation = Quaternion.Euler (0, 0, deg);

			//-1〜1に正規化
			_position = new Vector2 (
				barJoyStickSprite.transform.localPosition.x / radius,
				barJoyStickSprite.transform.localPosition.y / radius
			);
		}
	}

	//2点間の角度を求める
	float CalcRadian (Vector3 from, Vector3 to)
	{
		float dx = to.x - from.x;
		float dy = to.y - from.y;
		float radian = Mathf.Atan2 (dy, dx);
		return radian;
	}
}