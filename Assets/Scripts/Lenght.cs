using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lenght : MonoBehaviour {

	public Text Dalnost;
	float rasstoyanie = 0; // переменная для расстояния до цели
	public GameObject sharik;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hitInfo;

		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitInfo, 200))
		{
			rasstoyanie = hitInfo.distance;
			Dalnost.text = rasstoyanie.ToString ();

			if (Input.GetKeyDown (KeyCode.Mouse1)) 
			{
				GameObject GO = Instantiate (sharik, transform.position + Vector3.Normalize (hitInfo.point - transform.position), transform.rotation);
				Rigidbody rig = GO.GetComponent<Rigidbody> ();
				rig.velocity =  Vector3.Normalize (hitInfo.point - transform.position) * 10;
			}
		}

	}
}
