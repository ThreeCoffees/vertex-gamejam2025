using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] bool reset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            ResetScene();
        }
        if (reset) {
            ResetScene();
            reset = false;
        }
    }

    void ResetScene() {
        /*GameObject[] devices = GameObject.FindGameObjectsWithTag("Device");
        foreach (GameObject device in devices) {
            device.GetComponent<DeviceController>().ResetDevice();
        }

        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in doors) {
            door.GetComponent<DoorController>().UnlockDoor();
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().ResetPlayer();

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item Spawner");
        foreach (GameObject item in items) {
            item.GetComponent<SpawnerController>().ResetItem();
        }

        GetComponent<TimerController>().ResetTimer();
        */
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
