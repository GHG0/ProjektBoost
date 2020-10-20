using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
    	ProcessInput();
        print("update");
    }
    private void ProcessInput(){
    	if(Input.GetKey(KeyCode.Space)){
    		print("Thrusting");
    		rigidbody.AddRelativeForce(Vector3.up);
    	}
    	if(Input.GetKey(KeyCode.A)){
    		print("rotate left");
    	}
    	else if(Input.GetKey(KeyCode.D)){
    		print("rotate right");
    	}
    } 

}
