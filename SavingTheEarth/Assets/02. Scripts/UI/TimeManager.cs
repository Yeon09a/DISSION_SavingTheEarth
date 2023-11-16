using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; // 시간 text

    private int curTime; // 현재 시간(데이터에서 가져오기) // 임시(게임시각 오전 9시)
    private int uiMinutes; // UI에 표시된 분
    private int uiHours; // UI에 표시될 시간
    private string ampm; // 오전 오후 표시

    private int standTime = 300; // 기준 시간(초단위 / 게임에서 30분이 현실 5분)
    private int gameStandTime = 30; // 게임 시간(분단위)

   

    private void Awake()
    {
    }
    void Start()
    {
        curTime = DataManager.instance.nowPlayerData.gameTime;
        ampm = DataManager.instance.nowPlayerData.ampm;

        StartCoroutine(setTime());
    }

    void Update()
    {
        
    }

    private void calTime()
    {
        uiHours = curTime / 60;
        uiMinutes = curTime % 60;
        if(uiHours == 13 && uiMinutes == 0)
        {
            uiHours = 1;
            curTime = 60;
            if (ampm.Equals("오전  "))
            {
                ampm = "오후  ";
            } else if (ampm.Equals("오후  "))
            {
                ampm = "오전  ";
            }
        }
    }
    IEnumerator setTime()
    {
        while (true)
        {
            calTime();
            timeText.text = ampm + uiHours.ToString() + " : " + uiMinutes.ToString("D2");
            yield return new WaitForSeconds(standTime);
            curTime += gameStandTime;
        }
    }
}
