using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class RealmManager : MonoBehaviour
{
    public IdleGame game;
    public NegativeManager negative;
    public Canvas Realm1;
    public Canvas Realm2;
    public Canvas Realm3;

    public Text realmPlasmaText;
    public Text realmQuarksText;
    public Text RRBoostText;
    public Text realmAntiText;
    public Text realmLeakText;

    public BigDouble RRBoost => Log(Sqrt(game.data.quarks) + 1, 20) + 1;

    public void Update()
    {
        var data = game.data;
        realmPlasmaText.text = $"{Methods.NotationMethod(data.plasma, "F2")} Plasma";
        realmQuarksText.text = $"{Methods.NotationMethod(data.quarks, "F2")} Quarks";
        realmAntiText.text = $"{Methods.NotationMethod(data.AntiParticles, "F2")} Anti Particles";
        RRBoostText.text = $"{Methods.NotationMethod(RRBoost, "F2")}x\nPlasma/s";
        realmLeakText.text = $"Leak Boost:{Methods.NotationMethod(negative.leakBoost, "F2")}x";
    }

    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            case "realm1":
                Realm1.gameObject.SetActive(true);
                game.mainMenuGroup.gameObject.SetActive(true);
                break;
            case "realm2":
                Realm2.gameObject.SetActive(true);
                break;
            case "realm3":
                Realm3.gameObject.SetActive(true);
                break;

        }
    }

    void DisableAll()
    {
        Realm1.gameObject.SetActive(false);
        Realm2.gameObject.SetActive(false);
        Realm3.gameObject.SetActive(false);
        game.realmsGroup.gameObject.SetActive(false);
    }
}
