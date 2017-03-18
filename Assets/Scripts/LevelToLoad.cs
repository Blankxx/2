using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToLoad : MonoBehaviour
{
	public string loadLevel;
	public string exitName;
	private PlayerController thePlayer;

	void Start ()
	{
		//thePlayer = GameObject.Find ("Player").GetComponent<PlayerController> ();
		thePlayer = FindObjectOfType<PlayerController> (); // new
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			thePlayer.sceneSwitched = true;
			thePlayer.startPoint = exitName;
			SceneManager.LoadScene (loadLevel);
		}
	}
}