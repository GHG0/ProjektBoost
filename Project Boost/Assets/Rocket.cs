using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    AudioSource audio;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
    	rotate();
    	thrust();

        print("update");
    }
    private void rotate(){
    	rigidbody.freezeRotation = true; //take manual controll
    	float rotationtf = rcsThrust * Time.deltaTime;
    	if(Input.GetKey(KeyCode.A)){
    		transform.Rotate(Vector3.forward*rotationtf);
    	}
    	else if(Input.GetKey(KeyCode.D)){
    		transform.Rotate(-Vector3.forward*rotationtf);

    	}
    	rigidbody.freezeRotation = false;
    } 
    private void thrust(){
    	if(Input.GetKey(KeyCode.Space)){
    		rigidbody.AddRelativeForce(Vector3.up*mainThrust);
    		if(!audio.isPlaying){
    			audio.Play();
    		}
    	}else{
    		audio.Stop();
    	}
	}
	void OnCollisionEnter(Collision collision){
		switch (collision.gameObject.tag){
			case "Friendly":
				//do nothing
				print("ok");
				break;
			default:
				print("Dead");
				//kill/reload
				break;
		}
	}
}//serialize mainthrust