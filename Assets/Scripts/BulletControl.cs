using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl : MonoBehaviour
{

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
    	if(other.gameObject.CompareTag("Target")) {
            source.Play();
    		other.gameObject.SetActive(false);

            ScoreCounter.count = ScoreCounter.count + 1;

    	}
    }
}
