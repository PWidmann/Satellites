using UnityEngine;
using System.Collections;

public class MoonYRotation : MonoBehaviour {

	public bool spin;
	float speed;

	private void Start()
	{
		speed = GameManager.MoonSelfRotationSpeed;
	}

	void Update()
	{
		speed = GameManager.MoonSelfRotationSpeed;
		transform.Rotate(-Vector3.up, (speed) * Time.deltaTime);
	}
}