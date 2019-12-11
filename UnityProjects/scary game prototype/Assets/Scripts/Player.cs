using UnityEngine;

public class Player : MonoBehaviour {
    private Transform Trans;
    private bool Walking;
    private bool Flashlight = true;
    public float Stamina;
    public float MaxStamina;
    public float RidStamina;
    public float GainStamina;
    public GameObject Beam;
    public AudioSource WalkSound;
    public AudioSource FlashlightOn;
    public AudioSource FlashlightOff;
    // Start is called before the first frame update
    void Start() {
        Trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        //basic movement
        Trans.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4, 0));
        Trans.position += Trans.forward * (Input.GetAxis("Vertical") / 18);
        Trans.position += Trans.right * (Input.GetAxis("Horizontal") / 18);
        //running
        if(Input.GetButton("Run")&&Input.GetAxis("Vertical")!=0&&Stamina>0) {
            Trans.position += Trans.forward * (Input.GetAxis("Vertical") / 18);
            Trans.position += Trans.forward * (Input.GetAxis("Vertical") / 18);
            Stamina -= RidStamina;
        } else if (Stamina<MaxStamina&&!Input.GetButton("Run")) {
            Stamina += GainStamina;
        }
        //make sound on move 
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) Walking = true; else Walking = false;
        if (Walking&&!WalkSound.isPlaying) WalkSound.Play(); else WalkSound.Pause();
        //flashlight
        if(Input.GetButtonDown("Fire1")) {
            //toggle flashlight
            if(Flashlight) {
                Flashlight = false;
                Beam.SetActive(false);
                FlashlightOff.Play();
                Time.timeScale = 0f;
            } else {
                Flashlight = true;
                Beam.SetActive(true);
                FlashlightOn.Play();
                Time.timeScale = 1f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //reset velocity
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}