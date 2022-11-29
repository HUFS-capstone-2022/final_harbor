using UnityEngine;
using System.Collections;

public class FakeDoor : MonoBehaviour
{
	public AudioClip OpenSound;
	public static bool touchedDoor;


	void OnTriggerEnter(Collider coll)
	{
		touchedDoor = false;
		if (coll.gameObject.tag == "HandColl")
		{
			Debug.Log("Hand collider trigger door");
			touchedDoor = true;
			gameObject.GetComponent<AudioSource>().PlayOneShot(OpenSound);
		}
	}
}
