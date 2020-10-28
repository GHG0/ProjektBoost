using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    AudioSource audio;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] ParticleSystem mainEngine;
    [SerializeField] AudioClip deathsound;
    [SerializeField] AudioClip nextlevelsound;
    [SerializeField] ParticleSystem mainEngine;
    [SerializeField] ParticleSystem deathsound;
    [SerializeField] ParticleSystem nextlevelsound;
// TODO middle of 28


    enum State {Alive, Dying, Tran}
    State state = State.Alive;

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
    	if (state == State.Alive){
    		rotate();
    		thrust();
		}
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
    			audio.PlayOneShot(mainEngine);
    		}
    	}else{
    		audio.Stop();
    	}
	}
	void OnCollisionEnter(Collision collision){
		if (state != State.Alive){return;}
		switch (collision.gameObject.tag){
			case "Friendly":
				break;
			case "Finish":
				state = State.Tran;
				Invoke("LoadNextScene", 1f);
				audio.Stop();
				audio.PlayOneShot(nextlevelsound);
				break;
			default:
				state = State.Dying;
				Invoke("Death", 1f);
				audio.Stop();
				audio.PlayOneShot(deathsound);
				break;
		}
	}
	void Death(){
		SceneManager.LoadScene(0);
	}
	void LoadNextScene(){
		SceneManager.LoadScene(1); // allow for more than 2 levels
	}
}//serialize mainthrust