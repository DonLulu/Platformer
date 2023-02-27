using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer: MonoBehaviour
{
    public TMP_Text timerText;
    public float timeValue = 100;
    public PlayerMovement player;
 
    public void Start(){
        StartCoroutine(time());
    }

    private void Update()
    {
        if (timeValue < 1)
        {
            player.GameOver();
        }

        timerText.text = timeValue.ToString();
    }

    IEnumerator time(){
        while (true)
        {
            timeCount();
            yield return new WaitForSeconds(1);
        }
    }
    void timeCount(){
        timeValue -= 1;
    }
}