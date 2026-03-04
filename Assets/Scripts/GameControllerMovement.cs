using UnityEngine;

public class GameControllerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private float moveSpeed;
    //Google Ai ofr walk and run speed
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce;
    public float turnSpeed;

    public bool isgrounded; 
    
    public Vector3 spawnPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()



    {
        rb = GetComponent<Rigidbody>();

        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 movementInput = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
       movementInput = Vector3.Normalize(movementInput);
        rb.MovePosition(transform.position + movementInput * Time.deltaTime * moveSpeed);

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 currentRotation = rb.rotation.eulerAngles;

            Vector3 targetRotation = currentRotation + Vector3.up * turnSpeed * Time.deltaTime;

            rb.MoveRotation(Quaternion.Euler(targetRotation));
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 currentRotation = rb.rotation.eulerAngles;

            Vector3 targetRotation = currentRotation + Vector3.down * turnSpeed * Time.deltaTime;

            rb.MoveRotation(Quaternion.Euler(targetRotation));
        }

        //Google Ai
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isgrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        //Respawn
        if (transform.position.y < 0)
        {
            rb.position = spawnPosition;
        }
       

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);

        if (collision.gameObject.tag == "Ground") 
        { 
            Debug.Log("Only jump Once!");
        }

        //Chec if the player has come into contact with the ground
        //If they have, they should be able to jump
        if (collision.gameObject.tag == "Ground") 
        {
            isgrounded = true;
        }
        
        
        if (collision.gameObject.tag == "Hazard") 
        {
            rb.position = spawnPosition;
            Debug.Log("hurt");
        }

        
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isgrounded = false;
        }


    }
   
    



    
}
