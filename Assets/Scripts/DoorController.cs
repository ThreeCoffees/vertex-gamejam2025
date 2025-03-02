using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class DoorController : MonoBehaviour
{

    [SerializeField] public bool locked = false;
    [SerializeField] private float openingSequenceDuration = 1.0f;
    [SerializeField] private float openingRange = 2.0f;
    [SerializeField] private float openPercentage = 0.0f; // Debug

    private GameObject door;

    private bool opening, closing; 

    // Start is called before the first frame update
    void Start()
    {
        door = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked) LockDoor();
        else UnlockDoor();
    
        if(opening){
            openPercentage += Time.deltaTime / openingSequenceDuration;
            if(openPercentage >= 1){
                openPercentage = 1;
                opening = false;
            }
        }

        if(closing){
            openPercentage -= Time.deltaTime / openingSequenceDuration;
            if(openPercentage <= 0){
                openPercentage = 0;
                closing = false;
            }
        }
        
        door.transform.localPosition = new Vector3(Mathf.Lerp(0, openingRange, openPercentage), 0, 0);
    }

    void StartOpening(){
        Debug.Log("Opening door...");
        if(!locked){
            door.GetComponent<BoxCollider2D>().enabled = false;
            opening = true;
            closing = false;
        }
    }

    void StartClosing(){
        door.GetComponent<BoxCollider2D>().enabled = true;
        opening = false;
        closing = true;
    }

    public void LockDoor(){
        locked = true;
        GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.red;
        door.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void UnlockDoor(){
        locked = false;
        GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.green;
        door.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Trigger enter");
        if(other.tag == "Player" && !locked){
            StartOpening();
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            StartClosing();
        }
    }
}
