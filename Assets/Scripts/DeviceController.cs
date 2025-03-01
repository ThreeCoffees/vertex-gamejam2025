using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    [SerializeField] List<ItemType> requiredItems = new List<ItemType>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryUsingItem(GameObject item){
        Debug.Log("Using " + item.name + " on " + this.name);
        ItemType type = item.GetComponent<ItemController>().type;

        int id = requiredItems.IndexOf(type);
        if(id != -1){
            Debug.Log(id);
            requiredItems.RemoveAt(id);
        }
    }

    public bool isBroken(){
        return requiredItems.Count > 0;
    }

    public void increaseDamage(ItemType item){
        requiredItems.Add(item);
    }
}
