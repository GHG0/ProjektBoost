using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour{
	[SerializeField] Vector3 moveV;
	[Range(0,1)][SerializeField]float movementFactor;


//29 -- 9:01


    // Start is called before the first frame update
    void Start(){
        Vector3 startingPos = transform.position;

    }

    // Update is called once per frame
    void Update(){
        Vector3 offset = moveV * movementFactor;
        transform.position = startingPos + offset;
    }
}
