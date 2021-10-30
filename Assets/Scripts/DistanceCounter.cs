using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour 
{
    public Text DistanceText;
    public GameObject Grenade;
	private float _distance = 0; // переменная для расстояния до цели

    // Update is called once per frame
	void Update () {

		RaycastHit hitInfo;

		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitInfo, 200))
		{
			_distance = hitInfo.distance;
			DistanceText.text = _distance.ToString ();

			if (Input.GetKeyDown (KeyCode.Mouse1)) 
			{
				GameObject GO = Instantiate (Grenade, transform.position + Vector3.Normalize (hitInfo.point - transform.position), transform.rotation);
				Rigidbody rig = GO.GetComponent<Rigidbody> ();
				rig.velocity =  Vector3.Normalize (hitInfo.point - transform.position) * 10;
			}
		}

	}
}
