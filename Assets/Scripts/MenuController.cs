using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int selectedId;
    private GameObject selected;
    [SerializeField] private GameObject[] selections;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip selectSound;

    // Start is called before the first frame update
    void Start()
    {
        selected = selections[selectedId];
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.W)) {
            selectedId = (selectedId - 1) % selections.Length;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            selectedId = (selectedId + 1) % selections.Length;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (selectedId == 0) {
                player.GetComponent<PlayerMovement>().enabled = true;
                this.gameObject.SetActive(false);
            }
            if (selectedId == 1) {
                Application.Quit();
            }
        }
        SelectOption();
    }

    void SelectOption() {
        selected.GetComponent<TMP_Text>().color = Color.white;

        selected = selections[selectedId];
        selected.GetComponent<TMP_Text>().color = Color.red;
    }
}
