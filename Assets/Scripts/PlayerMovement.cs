using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] GameObject heldItem;
    [SerializeField] float itemReachRadius = 1.0f;
    [SerializeField] float itemUseRadius = 1.0f;
    [SerializeField] float itemUseTimer = 0.1f;

    [SerializeField] private AudioClip hammerUse;
    [SerializeField] private AudioClip screwdriverUse;
    [SerializeField] private AudioClip wrenchUse;
 
    Rigidbody2D rigidb;

    private Vector2 moveInput;
    private float lastUsedItem = 0;

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
        HandleTimers();
        HandleInput();
        rigidb.velocity = moveInput * speed;
        RotateToMouse();
    }

    void HandleTimers(){
        lastUsedItem = lastUsedItem - Time.deltaTime;
        if(lastUsedItem < 0) {
            lastUsedItem = 0;
        }
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
    
        // Items
        itemInput();
    }

    void itemInput(){
        if (Input.GetKeyDown(KeyCode.E)){
            if(lastUsedItem > 0){
                return;
            }
            if (heldItem == null) {
                ReachForItem();
            }
            else if (heldItem != null) {
                TryUseItem();
            }
            lastUsedItem = itemUseTimer;
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            if (heldItem != null) {
                DropItem();
            }
        }
    }

    void RotateToMouse(){
        float AngleRad = Mathf.Atan2 (cursorPosition.y - transform.position.y, cursorPosition.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0,0,AngleDeg);

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

    void TryUseItem(){
        Debug.Log("Using item...");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, itemUseRadius);
        foreach (Collider2D collider in colliders){
            DeviceController device = collider.gameObject.GetComponent<DeviceController>();
            if (device != null){
                bool success = device.TryUsingItem(heldItem);
                //Debug.Log("Using " + heldItem.name + " on " + collider.gameObject.name);
                if (success){
                    switch(heldItem.GetComponent<ItemController>().type){
                        case ItemType.Hammer:
                            GetComponent<AudioSource>().PlayOneShot(hammerUse);
                            break;
                        case ItemType.Screwdriver:
                            GetComponent<AudioSource>().PlayOneShot(screwdriverUse);
                            break;
                        case ItemType.Wrench:
                            GetComponent<AudioSource>().PlayOneShot(wrenchUse);
                            break;
                    }
                }
                break;
            }
        }
    }

    void DropItem(){
        Debug.Log("Dropping item...");
        heldItem.transform.SetParent(null, true);
        Rigidbody2D heldItemRB = heldItem.GetComponent<Rigidbody2D>();
        heldItemRB.velocity = Vector2.zero;
        heldItemRB.angularVelocity = 0.0f;
        heldItem.GetComponent<Collider2D>().enabled = true;
        // heldItem.GetComponent<Rigidbody2D>().velocity = rigidb.rotation;
        
        heldItem = null;
    }

    public void ResetPlayer(){
        heldItem = null;
        Transform spawn = transform.parent;
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
    }
}

