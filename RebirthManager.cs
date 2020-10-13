using static BreakInfinity.BigDouble;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;

public class RebirthManager : MonoBehaviour
{
    public IdleGame game;

    public Text darkEnergyText;
    public Text darkEnergyBoostText;
    public Text darkEnergyToGetText;

    public BigDouble darkEnergyToGet => 125 * Sqrt(game.data.darkMatter / 1e10);
    public BigDouble darkEnergyBoost => game.data.darkEnergy * 0.01 + 1;

    public void Run()
    {
        UI();
        

        void UI()
        {
            var data = game.data;
            if (!game.singularityGroup.gameObject.activeSelf) return;
            darkEnergyText.text = $"Dark Energy: {Methods.NotationMethod(data.darkEnergy, "F2")}";
            darkEnergyToGetText.text = $"+{Methods.NotationMethod(darkEnergyToGet, "F2")} Dark Energy";
            darkEnergyBoostText.text = $"Dark Matter is: {Methods.NotationMethod(darkEnergyBoost, "F3")}x better";

        }
    }

    public void Rebirth()
    {
        var data = game.data;

        if (data.darkMatter >= 1000)
        {
            data.darkEnergy = darkEnergyToGet;

            data.plasma = 0;
            data.plasmaCollected = 0;
            data.plasmaClickValue = 1;
            data.darkMatter = 0;
            data.plasmaPerSecond = 0;
            #region click
            game.data.clickUpgrade1Level = 0;
            game.data.clickUpgrade2Level = 0;
            game.data.clickUpgrade3Level = 0;
            game.data.clickUpgrade4Level = 0;
            game.data.clickUpgrade5Level = 0;
            game.data.clickUpgrade6Level = 0;
            game.data.clickUpgrade7Level = 0;
            game.data.clickUpgrade8Level = 0;
            game.data.clickUpgrade9Level = 0;
            game.data.clickUpgrade10Level = 0;
            #endregion
            #region production
            game.data.productionUpgrade1Level = 0;
            game.data.productionUpgrade2Level = 0;
            game.data.productionUpgrade3Level = 0;
            game.data.productionUpgrade4Level = 0;
            game.data.productionUpgrade5Level = 0;
            game.data.productionUpgrade6Level = 0;
            game.data.productionUpgrade7Level = 0;
            game.data.productionUpgrade8Level = 0;
            game.data.productionUpgrade9Level = 0;
            game.data.productionUpgrade10Level = 0;
            #endregion
            data.prestigeUlevel1 = 0;
            data.prestigeUlevel2 = 0;
            data.prestigeUlevel3 = 0;
            data.achLevel1 = 0;
            data.achLevel2 = 0;
            data.achLevel3 = 0;
            data.achLevel4 = 0;
            data.achLevel5 = 0;
            data.achLevel6 = 0;
            #region autos
            game.data.autoLevel1 = 0;
            game.data.autoLevel2 = 0;
            game.data.autoLevel3 = 0;
            game.data.autoLevel4 = 0;
            game.data.autoLevel5 = 0;
            game.data.autoLevel6 = 0;
            game.data.autoLevel7 = 0;
            game.data.autoLevel8 = 0;
            game.data.autoLevel9 = 0;
            game.data.autoLevel10 = 0;
            game.data.autoLevel11 = 0;
            game.data.autoLevel12 = 0;
            game.data.autoLevel13 = 0;
            game.data.autoLevel14 = 0;
            game.data.autoLevel15 = 0;
            game.data.autoLevel16 = 0;
            game.data.autoLevel17 = 0;
            game.data.autoLevel18 = 0;
            game.data.autoLevel19 = 0;
            game.data.autoLevel20 = 0;
            #endregion
            data.quarks = 1;
            data.realmUpgradeLevel1 = 0;
            data.realmUpgradeLevel2 = 0;
            data.realmUpgradeLevel3 = 0;
            game.singularityGroup.gameObject.SetActive(false);
            game.mainMenuGroup.gameObject.SetActive(true);
        }
    }
}
