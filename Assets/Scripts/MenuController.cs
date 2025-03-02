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
    [SerializeField] private GameObject tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        selected = selections[selectedId];
        player.GetComponent<PlayerMovement>().enabled = false;
        if(tutorialText != null){
            tutorialText.GetComponent<TMP_Text>().text = (PlayerPrefs.GetInt("TutorialCompleted") == 1) ? "Tutorial Disabled" : "Tutorial Enabled";
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void OnEnable() {
        player.GetComponent<PlayerMovement>().enabled = false;
        Time.timeScale = 0;
    }

    void OnDisable() {
        player.GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1;
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.W)) {
            selectedId = Mathf.Abs(selectedId - 1) % selections.Length;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            selectedId = Mathf.Abs(selectedId + 1) % selections.Length;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (selectedId == 0) {
                if(GameController.instance.isGameOver()){
                    GameController.instance.ResetScene();
                }
                else {
                    player.GetComponent<PlayerMovement>().enabled = true;
                    this.gameObject.SetActive(false);
                }
            }
            if (selectedId == 1) {
                GameController.instance.ExitGame();
            }
            if (selectedId == 2) {
                PlayerPrefs.SetInt("TutorialCompleted", (PlayerPrefs.GetInt("TutorialCompleted") + 1)%2);
                if(tutorialText != null){
                    tutorialText.GetComponent<TMP_Text>().text = (PlayerPrefs.GetInt("TutorialCompleted") == 1) ? "Tutorial Disabled" : "Tutorial Enabled";
                }
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
