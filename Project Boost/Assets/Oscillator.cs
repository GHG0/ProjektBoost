using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour{
	Vector3 startingPos;
    float startingRotation;
    [SerializeField] Vector3 moveV = new Vector3(10f,10f, 10f);
	[Range(0,1)][SerializeField]float movementFactor;
    [SerializeField] float period = 2f;




    // Start is called before the first frame update
    void Start(){
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update(){
        if (period<=Mathf.Epsilon){return;}
        float cycles= Time.time / period;
        const float tau = Mathf.PI *2;
        float rawSinWave = Mathf.Sin(cycles*tau);
        
        movementFactor = rawSinWave/2f + 0.5f;



        Vector3 offset = moveV * movementFactor;
        transform.position = startingPos + offset;
        transform.Rotate(Vector3.forward*movementFactor);
    }
}
