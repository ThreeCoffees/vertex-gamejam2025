using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] GameObject heldItem;
    [SerializeField] float itemReachRadius = 1.0f;

    Rigidbody2D rigidb;

    private Vector2 moveInput;
    private Vector3 cursorPosition;

    void Awake(){
        rigidb = GetComponent<Rigidbody2D>();        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        rigidb.velocity = moveInput * speed;
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
    
        if (Input.GetKey(KeyCode.E)){
            if (heldItem == null) {
                ReachForItem();
            }
        }
    }

    void RotateToMouse(){
        float AngleRad = Mathf.Atan2 (cursorPosition.y - transform.position.y, cursorPosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        rigidb.rotation = AngleDeg;

        // Rotate held item
        if (heldItem != null){
            heldItem.transform.position = transform.position;
            heldItem.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
            heldItem.transform.position += heldItem.transform.right * 0.5f;
        }
    }

    void ReachForItem(){
        Debug.Log("Reaching for item...");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, itemReachRadius);
        foreach (Collider2D collider in colliders){
            if (collider.gameObject.CompareTag("Item")){
                heldItem = collider.gameObject;
                
                collider.enabled = false;
                // collider.offset = new Vector2(0.5f, 0.5f);
                
                heldItem.transform.SetParent(this.transform, true);

                Debug.Log("Picked up " + heldItem.name);
                break;
            }
        }
    }
}

