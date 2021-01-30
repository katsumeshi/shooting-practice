using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	public GameObject bulletPrefab;
	public int life = 3;

	void Update () {
		
		JoystickController jc = GameObject.Find("Fixed Joystick").GetComponent<JoystickController>();

		Vector2 playerPos = transform.position;
		Vector2 size = GetComponent<SpriteRenderer>().bounds.size;
		Vector2 screenLeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
		Vector2 screenRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

		if (playerPos.x - size.x/2 > screenLeftBottom.x) {
			if (jc.move == -1 || Input.GetKey (KeyCode.LeftArrow)) {
				transform.Translate (-0.1f, 0, 0);
			}
		}
		if (playerPos.x + size.x/2 < screenRightTop.x) {
			if (jc.move == 1 || Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate ( 0.1f, 0, 0);
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (bulletPrefab, transform.position, Quaternion.identity);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (life > 0) {
			GameObject[] objects = GameObject.FindGameObjectsWithTag("Life");
			Destroy(objects[life-1]);
		}

		if (life <= 1) {
			GameObject.Find ("Canvas").GetComponent<UIController>().GameOver ();
		}
		life -= 1;
	}
}
