using UnityEngine;
using System.Collections;

public class YRotation : MonoBehaviour {

	public bool spin;
	public float speed = 10f;
	public float direction = 1f;

	void Update() {

		transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
	}
}