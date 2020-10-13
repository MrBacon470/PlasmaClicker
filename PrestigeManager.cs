using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;
using static BreakInfinity.BigDouble;


public class PrestigeManager : MonoBehaviour
{
    public IdleGame game;
    public RebirthManager rebirth;
    public Canvas prestige;

    public Text[] costText = new Text[3];
    public Image[] costBars = new Image[3];

    public BigDouble[] prestigeUCosts;
    public BigDouble[] prestigeULevels;

    public string[] costDesc;

    //future playerdata stuff

    private BigDouble Cost1 => 5 * Pow(1.5, game.data.prestigeUlevel1);
    private BigDouble Cost2 => 10 * Pow(1.5, game.data.prestigeUlevel2);
    private BigDouble Cost3 => 100 * Pow(2.5, game.data.prestigeUlevel3);

    public Text DarkMatterText;
    public Text DarkMatterBoostText;
    public Text DarkMatterToGetText;

    public void StartPrestige()
    {
        prestigeUCosts = new BigDouble[3];
        prestigeULevels = new BigDouble[3];
        costDesc = new[]{"Click is 50% more effective", "Harvesters are 10% more effective", "Dark Matter is +1.01x better"};
    }

    public void Run()
    {
        
        ArrayManager();
        UI();

        DarkMatterText.text = "Dark Matter: " + Methods.NotationMethod(game.data.darkMatter, "F0");
        DarkMatterBoostText.text = Methods.NotationMethod(TotalDarkMatterBoost(), "F2") + "x boost";

        if (game.mainMenuGroup.gameObject.activeSelf)
        {
            DarkMatterToGetText.text = "Prestige\n+" + Methods.NotationMethod(game.data.darkMatterToGet, "F0") + " Dark Matter";
        }

        void UI()
        {
            if (!prestige.gameObject.activeSelf) return;
            for (var i = 0; i < costText.Length; i++)
            {
                costText[i].text = $"Level {prestigeULevels[i]}\n{costDesc[i]}\nCost: {Methods.NotationMethod(prestigeUCosts[i], "F0")} Dark Matter";
                Methods.BigDoubleFill(game.data.darkMatter, prestigeUCosts[i], ref costBars[i]);
            }

        }
    }

    public void BuyUpgrade(int id)
    {
        

        switch (id)
        {
            case 0:
                Buy(ref game.data.prestigeUlevel1);
                break;

            case 1:
                Buy(ref game.data.prestigeUlevel2);
                break;

            case 2:
                Buy(ref game.data.prestigeUlevel3);
                break;
        }

        void Buy(ref int level)
        {
            
            if (game.data.darkMatter < prestigeUCosts[id]) return;
            game.data.darkMatter -= prestigeUCosts[id];
            level++;
        }

    }

    public void ArrayManager()
    {
        prestigeUCosts[0] = Cost1;
        prestigeUCosts[1] = Cost2;
        prestigeUCosts[2] = Cost3;

        prestigeULevels[0] = game.data.prestigeUlevel1;
        prestigeULevels[1] = game.data.prestigeUlevel2;
        prestigeULevels[2] = game.data.prestigeUlevel3;
    }

    //Prestige
    public void Prestige()
    {
        
        if (game.data.plasma > 1000)
        {
            game.data.darkMatter += game.data.darkMatterToGet;

            game.data.plasma = 0;
            game.data.plasmaClickValue = 1;
            game.data.plasmaCollected = 0;
            game.data.achLevel1 = 0;
            game.data.achLevel2 = 0;
            game.data.plasmaPerSecond = 0;

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
        }
    }

    public BigDouble TotalDarkMatterBoost()
    {
        
        var temp = game.data.darkMatter * 0.01;

        temp += prestigeULevels[2] * 0.01;
        temp *= rebirth.darkEnergyBoost;
        return temp + 1;
    }
}
