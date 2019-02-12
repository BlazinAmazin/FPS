using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public LayerMask ground;
	public Transform feet;

	private Vector3 direction;
	private Rigidbody rb;

	public float rotationSpeed = 1f;
	private float minY = -60f;
	private float maxY = 60f;
	private float rotationY = 0f;
	private float rotationX = 0f;
    private bool isGrounded;

	// Use this for initialization
	void Start () {
        isGrounded = false;
        speed = 5.0f;
		jumpHeight = 3.0f;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		direction = Vector3.zero;

		direction.z = Input.GetAxis("Horizontal");
		direction.x = Input.GetAxis("Vertical");
		direction = direction.normalized;

		if(direction.x != 0)
			rb.MovePosition(rb.position + transform.right * direction.x * speed * Time.deltaTime);
		if(direction.z != 0)
			rb.MovePosition(rb.position + transform.forward * direction.z * speed * Time.deltaTime);

        /* Player rotation */
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(0f, rotationX, rotationY);

        //isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
		if(Input.GetButtonDown("Jump") /*&& isGrounded*/) {
			rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
		}
        
	}
}
