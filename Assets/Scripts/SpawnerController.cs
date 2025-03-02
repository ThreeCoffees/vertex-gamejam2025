using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
   	private GameObject item;

	// Start is called before the first frame update
	void Start() {
		item = transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update() {

	}
	
	public void ResetItem() {
		item.transform.position = transform.position;
        item.transform.rotation = transform.rotation;
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        item.GetComponent<Rigidbody2D>().angularVelocity = 0;
	}
}
