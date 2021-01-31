using UnityEngine;
using System.Collections;
using Cinemachine;
using System;
using UnityEngine.UI;

public class RocketController : MonoBehaviour {

	public GameObject bulletPrefab;
	public int life = 3;
	public GameObject explosionPrefab;
	public CinemachineVirtualCamera cinemachineVirtualCamera;
	public Button button;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
	private float timeLeft = 0.3f;
	public FixedJoystick joystick;
	private Vector2 lb;
	private Vector2 rt;

	void Start() {
		virtualCameraNoise = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        button.onClick.AddListener(delegate() { Shot(); });
		lb = Camera.main.ScreenToWorldPoint(Vector3.zero);
		rt = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
	}

	void Update () {

		float x = transform.position.x + joystick.Horizontal * 0.1f;
		x = Mathf.Clamp(x, lb.x+0.6f, rt.x-0.6f);
		float y = transform.position.y + joystick.Vertical * 0.1f;
		y = Mathf.Clamp(y, lb.y+0.6f, rt.y-0.6f);
 		transform.position = new Vector3(x,y,0);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shot();
		}

		timeLeft = Math.Max(timeLeft - Time.deltaTime, -1);
		if ( timeLeft < 0 ) {
			virtualCameraNoise.m_AmplitudeGain = 0;
		}
	}

	void Shot() {
		Vector3 position = transform.position;
		position.y += 0.8f;
		Instantiate(bulletPrefab, position, Quaternion.identity);
    }

	void OnTriggerEnter2D(Collider2D coll) {

		virtualCameraNoise.m_AmplitudeGain = 1;
		timeLeft = 0.3f;

		if (life > 0) {
			GameObject[] objects = GameObject.FindGameObjectsWithTag("Life");
			Destroy(objects[life-1]);
		}

		if (life <= 1) {
			GameObject.Find ("Canvas").GetComponent<UIController>().GameOver ();
			GameObject effect = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
			Destroy(effect, 1.0f);
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject,0.3f);
		}
		life -= 1;
	}
}
