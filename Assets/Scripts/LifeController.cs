using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public GameObject lifePrefab;

	void Start () {
        RocketController rocket = GameObject.Find("rocket").GetComponent<RocketController>();
        Vector2 lb = Camera.main.ScreenToWorldPoint(Vector3.zero);
		Vector2 rt = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        for (int i = 0; i < rocket.life; i++) {
            Instantiate (lifePrefab, new Vector3 ((0.5f * i) + lb.x + 0.3f, rt.y - 0.3f, 0), Quaternion.identity);
        }
	}
}
