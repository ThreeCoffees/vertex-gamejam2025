using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] GameObject timer;
    private double time = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        timer.GetComponent<TMP_Text>().text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        timer.GetComponent<TMP_Text>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ResetTimer() {
        time = 0.0;
    }
}
