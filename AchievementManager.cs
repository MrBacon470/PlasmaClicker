using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using BreakInfinity;
using static BreakInfinity.BigDouble;


public class AchievementManager : MonoBehaviour
{
    public IdleGame game;

    public GameObject achievementScreen;
    public List<Achievement> achievementList = new List<Achievement>();

    private static string[] AchievementStrings => new string[] { "Current Plasma", "Plasma Harvested", "Current Dark Matter",
        "Current Dark Energy", "Shards Scavenged", "Quarks Stabilized", "Positrons Contained", "Anti Particles Converted"};
    private BigDouble[] AchievementNumbers => new BigDouble[] { game.data.plasma, game.data.plasmaCollected, game.data.darkMatter,
        game.data.darkEnergy, game.data.crystalShards, game.data.quarks, game.data.positrons, game.data.AntiParticles};

    private void Start()
    {
        foreach (var obj in achievementScreen.GetComponentsInChildren<Achievement>())
            achievementList.Add(obj);
    }

    public void RunAchievements()
    {
        UpdateAchievements(AchievementStrings[0], AchievementNumbers[0], ref game.data.achLevel1, ref achievementList[0].fill, ref achievementList[0].title, ref achievementList[0].progress);
        UpdateAchievements(AchievementStrings[1], AchievementNumbers[1], ref game.data.achLevel2, ref achievementList[1].fill, ref achievementList[1].title, ref achievementList[1].progress);
        UpdateAchievements(AchievementStrings[2], AchievementNumbers[2], ref game.data.achLevel3, ref achievementList[2].fill, ref achievementList[2].title, ref achievementList[2].progress);
        UpdateAchievements(AchievementStrings[3], AchievementNumbers[3], ref game.data.achLevel4, ref achievementList[3].fill, ref achievementList[3].title, ref achievementList[3].progress);
        UpdateAchievements(AchievementStrings[4], AchievementNumbers[4], ref game.data.achLevel5, ref achievementList[4].fill, ref achievementList[4].title, ref achievementList[4].progress);
        UpdateAchievements(AchievementStrings[5], AchievementNumbers[5], ref game.data.achLevel6, ref achievementList[5].fill, ref achievementList[5].title, ref achievementList[5].progress);
        UpdateAchievements(AchievementStrings[6], AchievementNumbers[6], ref game.data.achLevel7, ref achievementList[6].fill, ref achievementList[6].title, ref achievementList[6].progress);
        UpdateAchievements(AchievementStrings[7], AchievementNumbers[7], ref game.data.achLevel8, ref achievementList[7].fill, ref achievementList[7].title, ref achievementList[7].progress);
    }

    private void UpdateAchievements(string name, BigDouble number, ref BigDouble level, ref Image fill, ref Text title, ref Text progress)
    {
        var cap = Pow(10, level);

        if (game.achievementsGroup.gameObject.activeSelf)
            title.text = $"{name}\n({level})";
        progress.text = $"{Methods.NotationMethod(number, "F2")} / {Methods.NotationMethod(cap, "F2")}";

        Methods.BigDoubleFill(number, cap, ref fill);

        if (number < cap) return;
        BigDouble levels = 0;
        if (number / cap >= 1)
            levels = Floor(Log10(number / cap)) + 1;
        level += levels;
    }
}
