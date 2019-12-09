//#define NERF
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Classes;
using System.Collections;

public class Enemy : MonoBehaviour {
    #region Vars
    private bool beingHandled = false;
    private NavMeshAgent Agent;
    public AudioSource ChaseMusic;
    public float RadiusToChase;
    public float RadiusToMarker;
    private Choice<string> States = new Choice<string>(new string[3] {"Idle","Patrol","Chase"},1);
    public GameObject Player;
    public GameObject[] Markers;
    private Choice<GameObject> Marker;
    #endregion Vars

    // Start is called before the first frame update
    void Start() {
        Marker = new Choice<GameObject>(Markers);
        Agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
#if NERF
        Debug.Log(Vector3.Distance(gameObject.transform.position,Player.transform.position));
#endif
        #region Handle States
        switch(States.Get) {
            case "Patrol":
                //if (!beingHandled) {
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
                    foreach (GameObject i in objs) {
                        if ((gameObject.transform.position - i.transform.position).magnitude < RadiusToChase) {
                            States.choice = 2;
                        }
                    }
                    GameObject[] objz = GameObject.FindGameObjectsWithTag("Marker");
                    foreach (GameObject i in objz) {
                        if (Vector3.Distance(gameObject.transform.position,i.transform.position) < RadiusToMarker && i == Marker.Get) {
                            Debug.Log("weeeeeee");
                            Marker.choice++;
                            //States.choice = 0;
                        }
                    }
                    this.SetDestination(Marker.Get.transform.position);
                //}
                break;
            case "Idle":
                if (ChaseMusic.isPlaying) { ChaseMusic.Stop(); }
                //if (!beingHandled) StartCoroutine(HandleIt());
                Agent.speed = 2;
                //States.choice = 1;
                break;
            case "Chase":
                if (!ChaseMusic.isPlaying) ChaseMusic.Play();
                this.SetDestination(Player.transform.position);
                Agent.speed = 3;
                GameObject[] objss = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject i in objss) {
                    if (!((gameObject.transform.position - i.transform.position).magnitude < RadiusToChase)) {
                        States.choice = 1;
                    }
                }
                break;
        }
        #endregion Handle States
    }

    private IEnumerator HandleIt() {
        beingHandled = true;
        // process pre-yield
        yield return new WaitForSeconds(5.0f);
        // process post-yield
        beingHandled = false;
        States.choice = 1;
    }

    private void SetDestination(Vector3 position) {
        Agent.SetDestination(position);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.name == Player.name) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
