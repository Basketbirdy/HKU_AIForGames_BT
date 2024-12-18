using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float currentSpeed;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2.5f;

    private KeyCode sprintKey = KeyCode.LeftShift;

    private Vector3 direction;
    private Rigidbody rb;

    // states

    //[SerializeField]
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = baseSpeed;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
        CheckState();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void GetDirection()
    {
        direction.x = Input.GetAxisRaw("Horizontal");   // x direction
        direction.z = Input.GetAxisRaw("Vertical");     // z direction
        direction.y = 0;

        if(direction.x == 0 && direction.z == 0) { isMoving = false; }
        else { isMoving = true; }
    }

    private void CheckState()
    {
        if (Input.GetKeyDown(sprintKey)) { currentSpeed = baseSpeed * sprintMultiplier; }
        
        if(Input.GetKeyUp(sprintKey)) { currentSpeed = baseSpeed; }
    }

    private void Rotate()
    {
        if (!isMoving) { return; }

        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Move()
    {
        if(rb == null) { return; }

        rb.velocity = direction.normalized * currentSpeed;
    }
}
