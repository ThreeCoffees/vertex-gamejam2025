using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceBreaking : MonoBehaviour
{
    [SerializeField]float deviceBreakInterval = 5.0f;
    [SerializeField] private AudioClip smallDamage;
    [SerializeField] private AudioClip bigDamage;

    public List<DeviceController> devices;
    private ItemType[] itemTypes;
    private float timeSinceLastBreak = 1.0f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        DeviceController[] deviceControllers = Resources.FindObjectsOfTypeAll<DeviceController>();
        foreach(DeviceController d in deviceControllers){
            if(d.gameObject.activeInHierarchy){
                devices.Add(d);
            }
        }

        itemTypes = (ItemType[])Enum.GetValues(typeof(ItemType));
    }

    // Update is called once per frame
    void Update()
    {
        if(devices.Count == 0){
            Debug.Log("GameOver");
            return;
        }
        timeSinceLastBreak -= Time.deltaTime;
        if(timeSinceLastBreak <= 0){
            BreakMachine();
            timeSinceLastBreak = deviceBreakInterval;
        }
    }

    void BreakMachine(){
        int deviceId = UnityEngine.Random.Range(0, devices.Count);
        DeviceController d = devices[deviceId];
        ItemType neededTool = itemTypes[UnityEngine.Random.Range(0, itemTypes.Length)];

        d.increaseDamage(neededTool);
        if(d.isDestroyed()){
            devices.RemoveAt(deviceId);
        }

        audioSource.PlayOneShot(smallDamage);
    }
}
