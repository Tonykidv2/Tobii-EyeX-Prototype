using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GhostScript : MonoBehaviour {

    public Transform waypoint;
    bool running;
    public float RunTime = 7.5f;
    
    //int index = 0;
    //private bool caught = false;
    //float caughtTime;
    Transform Player;
    NavMeshAgent nav;
    private GazeAwareComponent _gazeAwareComponent;
    AudioSource _Sound;
    public float playSoundin = 5.0f;
    private float timer;

    // Use this for initialization
    void Start () {

        nav = GetComponent<NavMeshAgent>();
        _gazeAwareComponent = GetComponent<GazeAwareComponent>();
        Player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _Sound = GetComponent<AudioSource>();
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {


        if (timer <= 0.0f)
        {
            _Sound.Play();
            timer = playSoundin;
        }
        else if (timer > 0.0f)
            timer -= Time.deltaTime;


        if (Vector3.Distance(transform.position, Player.position) <= 0.5f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (!running)
            ChasePlayer();

        else if (running)
        {
            RunAway();
            RunTime -= Time.deltaTime;

            if(RunTime <= 0.0f)
            {
                RunTime = 7.5f;
                running = false;
            }
        }
        
        if (_gazeAwareComponent.HasGaze)
        {
            running = true;
            RunTime = 7.5f;
            RunAway();
        }

    }

    void ChasePlayer()
    {
        nav.SetDestination(Player.position);
    }

    void RunAway()
    {
        nav.SetDestination(waypoint.position);
    }
}
