using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    AudioSource audio;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoaddelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathsound;
    [SerializeField] AudioClip nextlevelsound;
    [SerializeField] ParticleSystem engineP;
    [SerializeField] ParticleSystem deathP;
    [SerializeField] ParticleSystem nextP;
// TODO middle of 28


    enum State {Alive, Dying, Tran}
    State state = State.Alive;
    bool collisionsenabled = true;
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
    	if (state == State.Alive){
    		rotate();
    		thrust();
    		debug();
		}
    }
    private void debug(){
    	if(Input.GetKeyDown(KeyCode.L)){
    		LoadNextScene();
    	}else if(Input.GetKeyDown(KeyCode.C)){
    		collisionsenabled = !collisionsenabled;
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
    		engineP.Play();
    	}else{
    		audio.Stop();
    		engineP.Stop();
    	}
	}
	void OnCollisionEnter(Collision collision){
		if (state != State.Alive || !collisionsenabled){return;}
		switch (collision.gameObject.tag){
			case "Friendly":
				break;
			case "Finish":
				state = State.Tran;
				Invoke("LoadNextScene", levelLoaddelay);
				audio.Stop();
				audio.PlayOneShot(nextlevelsound);
				nextP.Play();
				break;
			default:
				state = State.Dying;
				Invoke("Death", levelLoaddelay);
				audio.Stop();
				audio.PlayOneShot(deathsound);
				deathP.Play();
				break;
		}
	}
	void Death(){
		SceneManager.LoadScene(0);
		deathP.Stop();
	}
	void LoadNextScene(){
		SceneManager.LoadScene(1); // allow for more than 2 levels
		nextP.Stop();
	}
}//serialize mainthrust