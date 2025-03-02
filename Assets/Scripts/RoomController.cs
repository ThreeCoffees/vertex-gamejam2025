using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] List<GameObject> devices;
    [SerializeField] List<GameObject> doors;
    [SerializeField] List<GameObject> lights;
    private Collider2D roomCollider;

    bool isDestroyed = false;

    void Awake()
    {
        roomCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isDestroyed == true)
        {
            return;
        }

        foreach (GameObject o in devices)
        {
            DeviceController d = o.GetComponent<DeviceController>();
            if (!d.isDestroyed())
            {
                return;
            }
        }

        DestroyRoom();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDestroyed)
        {
            Debug.Log("Player died");
            GameController.instance.GameOver();
        }
    }

    void DestroyRoom()
    {
        isDestroyed = true;
        foreach (GameObject o in doors)
        {
            o.GetComponent<DoorController>().locked = true;
        }
        foreach (GameObject o in lights)
        {
            o.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.red;
        }
    }
}
