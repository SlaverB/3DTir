using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject light;
    public Rigidbody rig;
    public AudioSource src;

    bool enabled = true;
    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
        src = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void Shoot () {
        if (!enabled)
            return;

        rig.isKinematic = false;
        light.SetActive (false);
        src.Play();

        enabled = false;
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

}

