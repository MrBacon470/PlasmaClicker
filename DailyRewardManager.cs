﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class DailyRewardManager : MonoBehaviour
{
    public IdleGame game;
    public DateTime tempDateTime;
    public float UTCtimer;

    public GameObject dailyReward;
    public GameObject dailyRewardClaimed;
    public Text[] rewardText = new Text[7];
    public Image[] rewardButton = new Image[7];
    public int[] rewardPercent;
    public Text claimText;

    public UTCTime utcTime;
    [Serializable]
    public class UTCTime
    {
        public string datetime;
    }

    public void Start()
    {
        StartCoroutine(GetUTCTime());
        rewardPercent = new[]{ 2, 4, 6, 8, 10, 20, 50};
    }

    public void Update()
    {
        var data = game.data;

        if (dailyReward.gameObject.activeSelf)
        {
            for (var i = 0; i < 7; i++)
            {
                rewardText[i].text = i < data.currentDay ? "CLAIMED" : $"+{Methods.NotationMethod((data.plasmaCollected + 100) * ((float)rewardPercent[i] / 100), "F2")} Plasma";
                rewardButton[i].color = i < data.currentDay ? Color.green : Color.white;
            }
        }
        
        UTCtimer += Time.deltaTime;
        if (UTCtimer < 60) return;
        UTCtimer = 0;
        StartCoroutine(GetUTCTime());

    }

    public void Claim(int id)
    {
        var data = game.data;
        if(data.dailyRewardReady & id <= data.currentDay)
        {

            data.plasma += (data.plasmaCollected + 100) * ((float)rewardPercent[id] / 100);
            data.currentDay++;
            data.dailyRewardReady = false;
            dailyRewardClaimed.gameObject.SetActive(true);
            data.UTCtime = tempDateTime;
            claimText.text = $"+{Methods.NotationMethod((data.plasmaCollected + 100) * ((float)rewardPercent[id] / 100), "F2")} Plasma";
        }
    }

    public void CloseRewards()
    {
        dailyRewardClaimed.gameObject.SetActive(false);
        dailyReward.gameObject.SetActive(false);
    }

    public void ToggleRewards()
    {
        dailyReward.gameObject.SetActive(!dailyReward.gameObject.activeSelf);
    }

    public IEnumerator GetUTCTime()
    {
        var data = game.data;
        var request = UnityWebRequest.Get("https://worldtimeapi.org/api/timezone/etc/UTC/");
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError) yield break;
        var json = request.downloadHandler.text;
        utcTime = JsonUtility.FromJson<UTCTime>(json);
        tempDateTime = Convert.ToDateTime(utcTime.datetime);

        if ((data.UTCtime.Day != tempDateTime.Day
        || data.UTCtime.Month != tempDateTime.Month
        || data.UTCtime.Year != tempDateTime.Year)
        && !data.dailyRewardReady)
        {
            data.dailyRewardReady = true;
            dailyReward.gameObject.SetActive(true);
            if (data.currentDay >= 7)
                data.currentDay = 0;
        }
    }
}
