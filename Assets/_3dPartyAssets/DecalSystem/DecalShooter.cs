using UnityEngine;
using System.Collections;

public class DecalShooter : MonoBehaviour {
	public GameObject DecalPrefab;
	public AudioSource ShootSource;
	public Animator anim;
	public GameObject Muzzleflash;
	float time = 0;
	public float radius = 1.0F;
	public float power = 2.0F;

	[SerializeField] private GameController _gameController;

	// Update is called once per frame
	void Update () {
		Muzzleflash.SetActive(false);

		if (time>0)
		{
			time -= Time.deltaTime;
			return;
		}
			

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{

			time = 0.3f;
			ShootSource.Play();
			anim.Play("fire");
			Muzzleflash.SetActive(true);
			//Сама стрельба
			RaycastHit hitInfo;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(transform.position, fwd, out hitInfo, 100f))
			{
				GameObject go = Instantiate(DecalPrefab ,hitInfo.point, Quaternion.LookRotation(Vector3.Slerp( -hitInfo.normal, fwd, 0.8f) )) as GameObject;
				go.GetComponent<DecalUpdater>().UpdateDecalTo(hitInfo.collider.gameObject, true);

				Vector3 explosionPos = hitInfo.point;

				Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();


                Target trg = hitInfo.collider.GetComponent<Target>();
                if (trg) trg.shoot();

				if (rb != null){
					rb.AddForceAtPosition(fwd*power,hitInfo.point,ForceMode.Impulse);
					_gameController.ShootInTarget?.Invoke();
					Debug.Log("rb!");
				}



			}
			//Сама стрельба


		}
	}
}
