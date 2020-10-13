using UnityEngine;
using System;
using UnityEngine.UI;
using BreakInfinity;
using System.Threading.Tasks;

public class OfflineManager : MonoBehaviour
{
    public IdleGame game;
    public DailyRewardManager daily;
    public GameObject offlinePopUp;
    public Text timeAwayText;
    public Text GainText;

    public DateTime currentTime;

    public async void LoadOfflineProduction()
    {
        var data = game.data;
        if(data.offlineProgressCheck)
        {
            var tempOfflineTime = Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
            var oldTime = DateTime.FromBinary(tempOfflineTime);
            var currentTime = await AwaitGetUTCTIme();
            var difference = currentTime.Subtract(oldTime);
            var rawTime = (float)difference.TotalSeconds;
            var offlineTime = rawTime / 10;

            offlinePopUp.gameObject.SetActive(true);
            TimeSpan timer = TimeSpan.FromSeconds(rawTime);
            timeAwayText.text = $"You were away for\n<color=#FF0000>{timer:dd\\:hh\\:mm\\:ss}</color>";

            BigDouble plasmaGains = game.TotalPlasmaPerSecond() * offlineTime;
            data.plasma += plasmaGains;
            data.plasmaCollected += plasmaGains;
            GainText.text = $"You Earned:\n<color=#00FF04>+{Methods.NotationMethod(plasmaGains, "F2")} Plasma</color>";
        }
    }

    public DateTime currentUTCDateTime;
    public async Task<DateTime> AwaitGetUTCTIme()
    {
        StartCoroutine(daily.GetUTCTime());
        await Task.Delay(1000);
        return daily.tempDateTime;
    }

    public void CloseOffline()
    {
        offlinePopUp.gameObject.SetActive(false);
    }

}
