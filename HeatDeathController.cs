using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class HeatDeathController : MonoBehaviour
{
    public IdleGame game;

    public Text realityShardsText;
    public Text realityShardsPerSecText;
    public Text realityCrystalsText;
    public Text realityCrystalPerSecText;

    public Text clickUpgrade1;
    public Text clickUpgrade2;
    public Text productionUpgrade1;
    public Text productionUpgrade2;

    public Canvas heatUpgradeScreen;
    public Canvas heatMainScreen;

    private void Update()
    {
        var data = game.data;
        if (data.isheatdeathactive == false) return;

        UI();
        void UI()
        {
            if(heatUpgradeScreen.gameObject.activeSelf)
            {
                clickUpgrade1.text = $"Click Upgrade 1\nCost:{Methods.NotationMethod(data.realityShards / 4, "F2")} Shards\nPower +1 Shard Per Click";
                clickUpgrade2.text = $"Click Upgrade 2\nCost:{Methods.NotationMethod(data.realityCrystals / 4, "F2")} Crystals\nPower +1 Crystal Per Click";
                productionUpgrade1.text = $"Shard Harvester\nCost:{data.realityShards / 2} Shards\n +1 Shard/s";
                productionUpgrade2.text = $"Crystal Forger\nCost:{data.realityCrystals / 2} Crystals"
            }

            realityCrystalsText.text = $"{Methods.NotationMethod(data.realityCrystals, "F2")} Reality Crystals";
            realityShardsText.text = $"{Methods.NotationMethod(data.realityShards, "F2")} Reality Shards";
        }

    }

    public void Click()
    {
        var data = game.data;
        if (data.isheatdeathactive == false) return;

        data.realityShards += 1 + (1 * data.clickUpgrade1LevelH);
        data.realityCrystals += 1 * data.clickUpgrade2LevelH;
    }

    public void BuyClickUpgrade1()
    {
        var data = game.data;
        if (data.isheatdeathactive == false) return;
    }

    public void BuyClickUpgrade2()
    {
        var data = game.data;
        if (data.isheatdeathactive == false) return;
    }

    public void BuyProUpgrade1()
    {
        var data = game.data;
        if (data.isheatdeathactive == false) return;
    }

    public void BuyProUpgrade2()
    {
        var data = game.data;
        if (data.isheatdeathactive == false) return;
    }

}