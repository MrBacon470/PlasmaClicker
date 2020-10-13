using UnityEngine;
using UnityEngine.UI;
using static BreakInfinity.BigDouble;
using BreakInfinity;

public class AutomatorManager : MonoBehaviour
{
    public IdleGame game;
    public UpgradesManager upgrades;

    public Text[] costText = new Text[20];
    public Image[] costBars = new Image[20];
    public Image[] costBarsSmooth = new Image[20];

    public string[] costDesc;

    //future playerdata stuff
    public BigDouble[] costs;
    public int[] levels;
    public int[] levelsCap;
    public float[] intervals;
    public float[] timer;

    private BigDouble cost1 => 1e4 * Pow(1.5, game.data.autoLevel1);
    private BigDouble cost2 => 1e5 * Pow(1.5, game.data.autoLevel2);
    private BigDouble cost3 => 1e6 * Pow(1.5, game.data.autoLevel3);
    private BigDouble cost4 => 1e7 * Pow(1.5, game.data.autoLevel4);
    private BigDouble cost5 => 1e8 * Pow(1.5, game.data.autoLevel5);
    private BigDouble cost6 => 1e9 * Pow(1.5, game.data.autoLevel6);
    private BigDouble cost7 => 1e10 * Pow(1.5, game.data.autoLevel7);
    private BigDouble cost8 => 1e11 * Pow(1.5, game.data.autoLevel8);
    private BigDouble cost9 => 1e12 * Pow(1.5, game.data.autoLevel9);
    private BigDouble cost10 => 1e13 * Pow(1.5, game.data.autoLevel10);
    private BigDouble cost11 => 1e14 * Pow(1.5, game.data.autoLevel11);
    private BigDouble cost12 => 1e15 * Pow(1.5, game.data.autoLevel12);
    private BigDouble cost13 => 1e16 * Pow(1.5, game.data.autoLevel12);
    private BigDouble cost14 => 1e17 * Pow(1.5, game.data.autoLevel14);
    private BigDouble cost15 => 1e18 * Pow(1.5, game.data.autoLevel15);
    private BigDouble cost16 => 1e19 * Pow(1.5, game.data.autoLevel16);
    private BigDouble cost17 => 1e40 * Pow(1.5, game.data.autoLevel17);
    private BigDouble cost18 => 1e40 * Pow(1.5, game.data.autoLevel18);
    private BigDouble cost19 => 1e80 * Pow(1.5, game.data.autoLevel19);
    private BigDouble cost20 => 1e80 * Pow(1.5, game.data.autoLevel20);



    public void StartAutomator()
    {
        costs = new BigDouble[20];
        levels = new int[20];
        levelsCap = new[] { 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21 };
        intervals = new float[20];
        timer = new float[20];
        costDesc = new[] { "Click Upgrade 1 AutoBuyer", "Production Upgrade 1 AutoBuyer", "Click Upgrade 2 AutoBuyer", "Production Upgrade 2 AutoBuyer", "Click Upgrade 3 AutoBuyer", "Production Upgrade 3 AutoBuyer", "Click Upgrade 4 AutoBuyer", "Production Upgrade 4 AutoBuyer", "Click Upgrade 5 AutoBuyer", "Production Upgrade 5 AutoBuyer", "Click Upgrade 6 AutoBuyer", "Production Upgrade 6 AutoBuyer", "Click Upgrade 7 AutoBuyer", "Production Upgrade 7 AutoBuyer", "Click Upgrade 8 AutoBuyer", "Production Upgrade 8 AutoBuyer", "Click Upgrade 9 AutoBuyer", "Production Upgrade 9 AutoBuyer", "Click Upgrade X AutoBuyer", "Production Upgrade X AutoBuyer" };
    }

    public void Run()
    {
        ArrayManager();
        UI();
        RunAuto();

        void UI()
        {
            if (!game.autoGroup.gameObject.activeSelf) return;
            for (var i = 0; i < costText.Length; i++)
            {
                costText[i].text = $"{costDesc[i]}\nCost: {Methods.NotationMethod(costs[i], "F2")} Plasma\nInterval: {(levels[i] >= levelsCap[i] ? "Instant" : intervals[i].ToString("F1"))}";
                game.SmoothNumber(ref game.plasmaTemp, game.data.plasma);
                Methods.BigDoubleFill(game.data.plasma, costs[i], ref costBars[i]);
                Methods.BigDoubleFill(game.plasmaTemp, costs[i], ref costBarsSmooth[i]);
            }
        }

        void RunAuto()
        {
            // lvl, upgrade
            CAuto(0, 0);
            CAuto(1, 1);
            CAuto(2, 2);
            CAuto(2, 2);
            CAuto(3, 3);
            CAuto(4, 4);
            CAuto(5, 5);
            CAuto(6, 6);
            CAuto(7, 7);
            CAuto(8, 8);
            CAuto(9, 9);

            PAuto(0, 0);
            PAuto(1, 1);
            PAuto(2, 2);
            PAuto(3, 3);
            PAuto(4, 4);
            PAuto(5, 5);
            PAuto(6, 6);
            PAuto(7, 7);
            PAuto(8, 8);
            PAuto(9, 9);


            void CAuto(int id, int index)
            {
                if (levels[id] <= 0) return;
                if (levels[id] != levelsCap[id])
                {
                    Buy(id);
                }
                else
                {
                    if (upgrades.BuyClickUpgradeMaxCount(index) != 0) upgrades.BuyClickUpgradeMax(index);
                }
            }
            void PAuto(int id, int index)
            {
                if (levels[id] <= 0) return;
                if (levels[id] != levelsCap[id])
                {
                    Buy(id);
                }
                else
                {
                    if (upgrades.BuyProductionUpgradeMaxCount(index) != 0) upgrades.BuyProductionUpgradeMax(index);
                }
            }

            void Buy(int id)
            {
                timer[id] += Time.deltaTime;
                if (timer[id] >= intervals[id])
                {
                    BuyUpgrade(id);
                    timer[id] = 0;
                }
            }
        }

    }



    private void ArrayManager()
    {
        var data = game.data;
        #region costs
        costs[0] = cost1;
        costs[1] = cost2;
        costs[2] = cost3;
        costs[3] = cost4;
        costs[4] = cost5;
        costs[5] = cost6;
        costs[6] = cost7;
        costs[7] = cost8;
        costs[8] = cost9;
        costs[9] = cost10;
        costs[10] = cost11;
        costs[11] = cost12;
        costs[12] = cost13;
        costs[13] = cost14;
        costs[14] = cost15;
        costs[15] = cost16;
        costs[16] = cost17;
        costs[17] = cost18;
        costs[18] = cost19;
        costs[19] = cost20;
        #endregion
        #region levels
        levels[0] = data.autoLevel1;
        levels[1] = data.autoLevel2;
        levels[2] = data.autoLevel3;
        levels[3] = data.autoLevel4;
        levels[4] = data.autoLevel5;
        levels[5] = data.autoLevel6;
        levels[6] = data.autoLevel7;
        levels[7] = data.autoLevel8;
        levels[8] = data.autoLevel9;
        levels[9] = data.autoLevel10;
        levels[10] = data.autoLevel11;
        levels[11] = data.autoLevel12;
        levels[12] = data.autoLevel13;
        levels[13] = data.autoLevel14;
        levels[14] = data.autoLevel15;
        levels[15] = data.autoLevel16;
        levels[16] = data.autoLevel17;
        levels[17] = data.autoLevel18;
        levels[18] = data.autoLevel19;
        levels[19] = data.autoLevel20;
        #endregion
        #region if/else
        if (data.autoLevel1 > 0)
            intervals[0] = 10 - (data.autoLevel1 - 1) * 0.5f;
        else intervals[0] = 0;

        if (data.autoLevel2 > 0)
            intervals[1] = 10 - (data.autoLevel2 - 1) * 0.5f;
        else intervals[1] = 0;

        if (data.autoLevel3 > 0)
            intervals[2] = 10 - (data.autoLevel3 - 1) * 0.5f;
        else intervals[2] = 0;

        if (data.autoLevel4 > 0)
            intervals[3] = 10 - (data.autoLevel4 - 1) * 0.5f;
        else intervals[3] = 0;

        if (data.autoLevel5 > 0)
            intervals[4] = 10 - (data.autoLevel5 - 1) * 0.5f;
        else intervals[4] = 0;

        if (data.autoLevel6 > 0)
            intervals[5] = 10 - (data.autoLevel6 - 1) * 0.5f;
        else intervals[5] = 0;

        if (data.autoLevel7 > 0)
            intervals[6] = 10 - (data.autoLevel7 - 1) * 0.5f;
        else intervals[6] = 0;

        if (data.autoLevel8 > 0)
            intervals[7] = 10 - (data.autoLevel8 - 1) * 0.5f;
        else intervals[7] = 0;

        if (data.autoLevel9 > 0)
            intervals[8] = 10 - (data.autoLevel9 - 1) * 0.5f;
        else intervals[8] = 0;

        if (data.autoLevel10 > 0)
            intervals[9] = 10 - (data.autoLevel10 - 1) * 0.5f;
        else intervals[9] = 0;

        if (data.autoLevel11 > 0)
            intervals[10] = 10 - (data.autoLevel11 - 1) * 0.5f;
        else intervals[10] = 0;

        if (data.autoLevel12 > 0)
            intervals[11] = 10 - (data.autoLevel12 - 1) * 0.5f;
        else intervals[11] = 0;

        if (data.autoLevel13 > 0)
            intervals[12] = 10 - (data.autoLevel13 - 1) * 0.5f;
        else intervals[12] = 0;

        if (data.autoLevel14 > 0)
            intervals[13] = 10 - (data.autoLevel14 - 1) * 0.5f;
        else intervals[13] = 0;

        if (data.autoLevel15 > 0)
            intervals[14] = 10 - (data.autoLevel15 - 1) * 0.5f;
        else intervals[14] = 0;

        if (data.autoLevel16 > 0)
            intervals[15] = 10 - (data.autoLevel16 - 1) * 0.5f;
        else intervals[15] = 0;

        if (data.autoLevel17 > 0)
            intervals[16] = 10 - (data.autoLevel17 - 1) * 0.5f;
        else intervals[16] = 0;

        if (data.autoLevel18 > 0)
            intervals[17] = 10 - (data.autoLevel18 - 1) * 0.5f;
        else intervals[17] = 0;

        if (data.autoLevel19 > 0)
            intervals[18] = 10 - (data.autoLevel19 - 1) * 0.5f;
        else intervals[18] = 0;

        if (data.autoLevel20 > 0)
            intervals[19] = 10 - (data.autoLevel20 - 1) * 0.5f;
        else intervals[19] = 0;
        #endregion
    }

    public void BuyUpgrade(int id)
    {
        var data = game.data;

        switch (id)
        {
            case 0:
                Buy(ref data.autoLevel1);
                break;

            case 1:
                Buy(ref data.autoLevel2);
                break;

            case 2:
                Buy(ref data.autoLevel3);
                break;
            case 3:
                Buy(ref data.autoLevel4);
                break;
            case 4:
                Buy(ref data.autoLevel5);
                break;
            case 5:
                Buy(ref data.autoLevel6);
                break;
            case 6:
                Buy(ref data.autoLevel7);
                break;
            case 7:
                Buy(ref data.autoLevel8);
                break;
            case 8:
                Buy(ref data.autoLevel9);
                break;
            case 9:
                Buy(ref data.autoLevel10);
                break;
            case 10:
                Buy(ref data.autoLevel11);
                break;
            case 11:
                Buy(ref data.autoLevel12);
                break;
            case 12:
                Buy(ref data.autoLevel13);
                break;
            case 13:
                Buy(ref data.autoLevel14);
                break;
            case 14:
                Buy(ref data.autoLevel15);
                break;
            case 15:
                Buy(ref data.autoLevel16);
                break;
            case 16:
                Buy(ref data.autoLevel17);
                break;
            case 17:
                Buy(ref data.autoLevel18);
                break;
            case 18:
                Buy(ref data.autoLevel19);
                break;
            case 19:
                Buy(ref data.autoLevel20);
                break;
        }

        void Buy(ref int level)
        {
            if (!(data.plasma >= costs[id] & level < levelsCap[id])) return;
            data.plasma -= costs[id];
            level++;
        }
    }
}