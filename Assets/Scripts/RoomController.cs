using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]List<GameObject> devices;
    [SerializeField]List<GameObject> doors;
    private Collider2D roomCollider;

    void Awake(){
        roomCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        foreach(GameObject d in devices){
            if((DeviceController))
        }
    }
}
