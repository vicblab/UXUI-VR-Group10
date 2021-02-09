using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioSource audioData;
    bool hasBeenPlayed=false;

    // Start is called before the first frame update
    void Start()
    {
      audioData = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){

        if(!hasBeenPlayed){
		audioData.Play();

		hasBeenPlayed=true;
	}
    }
}
