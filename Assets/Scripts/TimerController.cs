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

        //timer.GetComponent<TMP_Text>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
        timer.GetComponent<TMP_Text>().text = timerToText((float)time);
    }

    public static string timerToText(float timer){
        string text = string.Format("{0:00}:{1:00}.{2:00}", 
                Mathf.Floor(timer / 60), Mathf.Floor(timer % 60), Mathf.Floor((timer * 100) % 100));
        return text;
    }

    public double getTime() {
        return time;
    }

    public void ResetTimer() {
        time = 0.0;
    }
}
