using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    Rigidbody2D rb;  

    private Vector2 moveInput;
    private Vector3 cursorPosition;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        rb.velocity = moveInput * speed;
        RotateToMouse();
    }

    void HandleInput(){
        // Movement
        moveInput = Vector2.zero;
        if(Input.GetKey(KeyCode.A)){
            moveInput.x -= 1;
        }
        if(Input.GetKey(KeyCode.D)){
            moveInput.x += 1;
        }
        if(Input.GetKey(KeyCode.S)){
            moveInput.y -= 1;
        }
        if(Input.GetKey(KeyCode.W)){
            moveInput.y += 1;
        }
        moveInput.Normalize();

        // Cursor Position
        cursorPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - transform.position.y));
    }

    void RotateToMouse(){
        float AngleRad = Mathf.Atan2 (cursorPosition.y - transform.position.y, cursorPosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        rb.rotation = AngleDeg;
    }
}
