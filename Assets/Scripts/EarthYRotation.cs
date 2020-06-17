using UnityEngine;
using System.Collections;

public class EarthYRotation : MonoBehaviour {

	public bool spin;
	float speed;

	private void Start()
	{
		speed = GameManager.EarthSelfRotationSpeed;
	}

	void Update()
	{
		speed = GameManager.EarthSelfRotationSpeed;
		transform.Rotate(-Vector3.up, (speed) * Time.deltaTime);
	}
}