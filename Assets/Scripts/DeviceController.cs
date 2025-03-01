using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    [SerializeField] List<ItemType> requiredItems = new List<ItemType>();
    [SerializeField] GameObject repairIcons;

    [SerializeField] GameObject hammerIcon; 
    [SerializeField] GameObject screwdriverIcon; 
    [SerializeField] GameObject wrenchIcon; 
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
    }

    public void TryUsingItem(GameObject item){
        Debug.Log("Using " + item.name + " on " + this.name);
        ItemType type = item.GetComponent<ItemController>().type;

        if(!isBroken()){
            return;
        }
        if(requiredItems[0] == type){
            requiredItems.RemoveAt(0);
            updateRepairIcons();
        }
    }

    public bool isBroken(){
        return requiredItems.Count > 0;
    }

    public void increaseDamage(ItemType item){
        requiredItems.Add(item);
        updateRepairIcons();
    }
}
