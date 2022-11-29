using UnityEngine;
using System.Collections;

public class BoxSound : MonoBehaviour
{
	public AudioClip OpenSound;


	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "HandColl")
		{
			gameObject.GetComponent<AudioSource>().PlayOneShot(OpenSound);
		}
	}
}
