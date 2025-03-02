using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceController : MonoBehaviour
{
    [SerializeField] List<ItemType> requiredItems = new List<ItemType>();
    [SerializeField] GameObject repairIcons;
    RectTransform iconsSize;

    [SerializeField] GameObject hammerIcon; 
    [SerializeField] GameObject screwdriverIcon; 
    [SerializeField] GameObject wrenchIcon; 
    [SerializeField] GameObject sparks;
    [SerializeField] bool tutorialDevice;
    bool destroyed = false;

    void Awake(){
        iconsSize = repairIcons.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        updateRepairIcons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateRepairIcons(){
        if(destroyed){
            requiredItems.Clear();
            sparks.SetActive(false);
            GetComponent<SpriteRenderer>().color = Color.grey;
        }

        if(requiredItems.Count == 0){
            iconsSize.sizeDelta = new Vector2(0,0);
        }else{
            iconsSize.sizeDelta = new Vector2(42 + (32 * (requiredItems.Count - 1)), 42);
        }


        foreach(Transform child in repairIcons.transform){
            GameObject.Destroy(child.gameObject);
        }
        foreach(ItemType i in requiredItems){
            switch(i){
                case ItemType.Hammer:
                    Instantiate(hammerIcon, repairIcons.transform);
                    break;
                case ItemType.Screwdriver:
                    Instantiate(screwdriverIcon, repairIcons.transform);
                    break;
                case ItemType.Wrench:
                    Instantiate(wrenchIcon, repairIcons.transform);
                    break;
            }
        }

        if(isBroken() && !isDestroyed()){
            sparks.SetActive(true);
        }else{
            sparks.SetActive(false);
        }
    }

    public bool TryUsingItem(GameObject item){
        Debug.Log("Using " + item.name + " on " + this.name);
        ItemType type = item.GetComponent<ItemController>().type;

        if(!isBroken()|| destroyed){
            return false;
        }
        if(requiredItems[0] == type){
            requiredItems.RemoveAt(0);
            updateRepairIcons();
            return true;
        }
        return false;
    }

    public bool isBroken(){
        return requiredItems.Count > 0;
    }

    public bool isDestroyed(){
        return destroyed;
    }

    public void increaseDamage(ItemType item){
        if(requiredItems.Count >= 4) {
            if (tutorialDevice) { /* don't destroy */ } 
            else { destroyed = true; }
            return;
        }

        requiredItems.Add(item);
        
        updateRepairIcons();
    }

    public void ResetDevice() {
        requiredItems.Clear();
        updateRepairIcons();
    }
}
