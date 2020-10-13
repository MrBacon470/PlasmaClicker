using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class ShopManager : MonoBehaviour
{
    public IdleGame game;

    public GameObject hdButton;

    public Canvas GameSpeedScreen;
    public Canvas FabricatorScreen;
    public Canvas HeatDeathScreen;
    public Canvas Shop;
    public Canvas HeatDeath;

    public Text countdownTimerText;
    public Text countdownCostText;

    public Text plasmaFabText;
    public Text quarkFabText;

    public Text plasmaText;
    public Text quarkText;
    public Text antiText;
    public Text shardsText;

    private void Update()
    {
        var data = game.data;
        if (GameSpeedScreen.gameObject.activeSelf)
        {
            var gameSpeedTimerFormatted = TimeSpan.FromSeconds(data.gamespeedtimer);
            countdownTimerText.text = !data.gamespeedactive ? "2x Game Speed: <color=#00FF04> Paused</color>" : $"2x Game Speed: <color=#00FF04> {gameSpeedTimerFormatted:d\\:hh\\:mm\\:ss}</color>";
            countdownCostText.text = $"Cost: {Methods.NotationMethod(1e6 + (data.crystalShards / 2), "F2")} <color=#B22712> Crystal Shards</color>";
        }

        if(FabricatorScreen.gameObject.activeSelf)
        {
            plasmaFabText.text = $"{Methods.NotationMethod(1e4 + (data.quarks / 2), "F2")} Quarks -> {Methods.NotationMethod(1e4 + (data.plasma / 2), "F2")} Plasma";
            quarkFabText.text = $"{Methods.NotationMethod(1e4 + (data.AntiParticles / 2), "F2")} Anti Particles -> {Methods.NotationMethod(1e4 + (data.quarks / 2), "F2")} Quarks";
        }

        if(HeatDeathScreen.gameObject.activeSelf)
        {
            
        }

        plasmaText.text = $"{Methods.NotationMethod(data.plasma, "F2")} Plasma";
        quarkText.text = $"{Methods.NotationMethod(data.quarks, "F2")} Quarks";
        antiText.text = $"{Methods.NotationMethod(data.AntiParticles, "F2")} Anti Particles";
        shardsText.text = $"{Methods.NotationMethod(data.crystalShards, "F2")} Shards";

        if (data.gamespeedtimer > 0 & data.gamespeedactive)
        {
            data.gamespeedtimer -= Time.deltaTime * (1 / Time.timeScale);
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (data.plasma >= 1e308)
            hdButton.gameObject.SetActive(true);
        else
            hdButton.gameObject.SetActive(false);
    }

    public void Fabricate(int id)
    {
        var data = game.data;
        switch(id)
        {
            case 1:
                if (data.AntiParticles <= 1e4 + (data.AntiParticles / 2)) return;
                data.AntiParticles -= 1e4 + (data.AntiParticles / 2);
                data.quarks += 1e4 + (data.quarks / 2);
                break;
            case 2:
                if (data.quarks <= 1e4 + (data.quarks / 2)) return;
                data.quarks -= 1e4 + (data.quarks / 2);
                data.plasma += 1e4 + (data.plasma / 2);
                break;
        }
    }

    public void ToggleGameSpeed()
    {
        var data = game.data;
        if (data.gamespeedtimer > 0)
            data.gamespeedactive = !data.gamespeedactive;
    }

    public void Purchase2xSpeed()
    {
        var data = game.data;
        if(data.crystalShards >= 1e6 + (data.crystalShards / 2))
        {
            data.crystalShards -= 1e6 + (data.crystalShards / 2);
            data.gamespeedtimer += 3600;
        }
        
    }

    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            case "fabricators":
                FabricatorScreen.gameObject.SetActive(true);
                game.mainMenuGroup.gameObject.SetActive(true);
                break;
            case "gamespeed":
                GameSpeedScreen.gameObject.SetActive(true);
                break;
            case "prestige":
                HeatDeathScreen.gameObject.SetActive(true);
                break;
            case "heatdeath":
                Shop.gameObject.SetActive(false);
                HeatDeath.gameObject.SetActive(true);
                game.data.isheatdeathactive = true;
                break;
        }
    }

    void DisableAll()
    {
        FabricatorScreen.gameObject.SetActive(false);
        GameSpeedScreen.gameObject.SetActive(false);
        HeatDeathScreen.gameObject.SetActive(false);
        game.realmsGroup.gameObject.SetActive(false);
        HeatDeath.gameObject.SetActive(false);
    }
}
