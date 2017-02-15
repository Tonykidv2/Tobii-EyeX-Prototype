using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GazeAwareComponent))]
public class PatrolingEnemy : MonoBehaviour {

    public Transform[] waypoints;
    int index = 0;
    //private bool caught = false;
    //float caughtTime;
    Transform Player;
    NavMeshAgent nav;
    private GazeAwareComponent _gazeAwareComponent;

    // Use this for initialization
    void Start () {

        nav = GetComponent<NavMeshAgent>();
        _gazeAwareComponent = GetComponent<GazeAwareComponent>();
        Player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //caughtTime = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            index++;
            if (index > waypoints.Length - 1)
                index = 0;
        }
        nav.SetDestination(waypoints[index].position);


        if (_gazeAwareComponent.HasGaze)
        {
            ChasePlayer();
            return;
        }

        //Patrolling();
	}

    void ChasePlayer()
    {
        nav.SetDestination(Player.position);
    }

    void Patrolling()
    {
        Vector3 Direction = Player.position - transform.position;
        float angle = Vector3.Angle(Direction, transform.forward);
        Debug.DrawRay(transform.position, Player.position - transform.position);


        if (angle <= 90 * 0.5f)
        {
            RaycastHit Hitter;

            if (Physics.Raycast(transform.position + transform.up, Direction.normalized, out Hitter, 20 * .80f))
            {
                if (Hitter.collider.tag == "MainCamera")
                {
                    nav.SetDestination(Player.position);
                }
            }
        }
    }
}
