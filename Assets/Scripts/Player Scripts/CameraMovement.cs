using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    // Use this for initialization
    public float Speeder = 2.0f;

    CharacterController Mover;

	void Start () {
        Mover = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Vector3 newVelocity = new Vector3(Speed * h, 0, Speed * v);

        //gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        Vector3 MoveDirection = new Vector3(h, 0, v);
        MoveDirection = transform.TransformDirection(MoveDirection);
        MoveDirection *= Speeder;

        Mover.Move(MoveDirection);

    }
}
