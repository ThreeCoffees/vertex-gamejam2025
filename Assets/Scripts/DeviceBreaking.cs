using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceBreaking : MonoBehaviour
{
    public List<DeviceController> devices;
    [SerializeField]float deviceBreakInterval = 5.0f;

    private ItemType[] itemTypes;
    private float timeSinceLastBreak = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
