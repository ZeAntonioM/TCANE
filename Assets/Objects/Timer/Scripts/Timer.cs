using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    [Header("Time Values")]
    [Range(0,60)]
    public int seconds;
    [Range(0, 60)]
    public int minutes;
    [Range(0, 60)]
    public int hours;

    public Color fontColor;

    public bool showMilliseconds;

    private float currentSeconds;
    private int timerDefault;

    void Start()
    {
        timerText.color = fontColor;
        timerDefault = 0;
        timerDefault += (seconds + (minutes * 60) + (hours * 60 * 60));
        currentSeconds = timerDefault;
    }

    void Update()
    {
        if((currentSeconds -= Time.deltaTime) <= 0)
        {
            TimeUp();
        }
        else
        {
            if(showMilliseconds)
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss\:fff");
            else
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss");
        }
    }

    private void TimeUp()
    {
        /** 
        * Explode the bomb
        * This works if the tree structure is maintained: Bomb (with BombGame script) -> Timer (Canvas) -> Timer (Image + This Script) -> TimerText (TextMeshPro)
        */
        transform.parent.transform.parent.GetComponent<BombGame>().Explode();
        
        if (showMilliseconds)
            timerText.text = "00:00:000";
        else
            timerText.text = "00:00";
    }
}
