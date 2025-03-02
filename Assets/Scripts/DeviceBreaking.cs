using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceBreaking : MonoBehaviour
{
    [SerializeField] float deviceBreakInterval = 5.0f;
    [SerializeField] float deviceBreakIntervalTutorial = 10.0f;

    // TUTORIAL
    [SerializeField] DeviceController tutorialDevice;
    [SerializeField] bool tutorialMode;
    [SerializeField] GameObject timerText;
    [SerializeField] TimerController timerController;

    public List<DeviceController> devices;
    private int tutorialDeviceId;
    [SerializeField] private int maxBrokenDevices;
    private int deviceCount;
    private ItemType[] itemTypes;
    private float timeSinceLastBreak = 1.0f;

    void Awake(){
        if(!PlayerPrefs.HasKey("TutorialCompleted")){
            PlayerPrefs.SetInt("TutorialCompleted", 0);
            tutorialMode = true;
        } else {
            tutorialMode = (PlayerPrefs.GetInt("TutorialCompleted") == 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialMode) {
            timerText.SetActive(false);
        }

        DeviceController[] deviceControllers = Resources.FindObjectsOfTypeAll<DeviceController>();
        foreach(DeviceController d in deviceControllers){
            if(d.gameObject.activeInHierarchy){
                devices.Add(d);
                if (d == tutorialDevice) {tutorialDeviceId = devices.Count - 1;}
            }
        }
        deviceCount = devices.Count;

        itemTypes = (ItemType[])Enum.GetValues(typeof(ItemType));
    }

    // Update is called once per frame
    void Update()
    {
        if(devices.Count == 0){ return; }
        if(devices.Count <= deviceCount - maxBrokenDevices){
            Debug.Log("GameOver");
            GameController.instance.GameOver();
            return;
        }

        if (tutorialMode && tutorialDevice.isBroken() == false) {
            tutorialMode = false;
            PlayerPrefs.SetInt("TutorialCompleted", 1);

            tutorialDevice.tutorialDevice = false;

            timerText.SetActive(true);
            timerController.ResetTimer();

            Debug.Log("Tutorial completed");
        }

        if (tutorialMode){
            TutorialBreak();
        } else {
            InGameBreak();
        }
    }

    void TutorialBreak() {
        timeSinceLastBreak -= Time.deltaTime;
        if(timeSinceLastBreak <= 0){
            BreakMachine(tutorialDeviceId);
            timeSinceLastBreak = deviceBreakIntervalTutorial;
        }
    }

    void InGameBreak() {
        timeSinceLastBreak -= Time.deltaTime;
        if(timeSinceLastBreak <= 0){
            BreakMachine(UnityEngine.Random.Range(0, devices.Count));
            timeSinceLastBreak = deviceBreakInterval;
        }
    }

    void BreakMachine(int deviceId){
        DeviceController d = devices[deviceId];
        ItemType neededTool = itemTypes[UnityEngine.Random.Range(0, itemTypes.Length)];

        d.increaseDamage(neededTool);
        if(d.isDestroyed()){
            devices.RemoveAt(deviceId);
        }

        Debug.Log("Device " + d.name + " is broken. Needs " + neededTool);
    }

    public void ResetTutorial(){
        tutorialMode = true;
        PlayerPrefs.SetInt("TutorialCompleted", 0);
    } 
}
