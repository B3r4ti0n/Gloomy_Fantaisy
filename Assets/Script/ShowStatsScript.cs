using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowStatsScript : MonoBehaviour
{
    [SerializeField] private TMP_Text HpTMP;
    [SerializeField] private TMP_Text OvTMP;
    [SerializeField] private TMP_Text DvTMP;
    [SerializeField] private TMP_Text IvTMP;
    [SerializeField] private TMP_Text SvTMP;
    [SerializeField] private TMP_Text MvTMP;
    [SerializeField] private TMP_Text LvTMP;
    [SerializeField] private TMP_Text XpTMP;
    [SerializeField] private TMP_Text GsTMP;
    [SerializeField] private TMP_Text GpTMP;
    [SerializeField] private TMP_Text PlTMP;
    [SerializeField] private Button HpBtn;
    [SerializeField] private Button OvBtn;
    [SerializeField] private Button DvBtn;
    [SerializeField] private Button IvBtn;
    [SerializeField] private Button SvBtn;
    [SerializeField] private Button MvBtn;
    [SerializeField] private RawImage AvImg;
    [SerializeField] private RawImage AtImg;
   

    public static UserLogged userLogged = new UserLogged();

    // Start is called before the first frame update
    void Start()
    {
        userLogged = StatsMapController.userLogged;
        LvTMP.text = userLogged.stats.level.ToString();
        XpTMP.text = userLogged.stats.exp.ToString();
        GsTMP.text = userLogged.stats.gold.ToString();
        GpTMP.text = userLogged.stats.gold_premium.ToString();
        PlTMP.text = userLogged.stats.level_point.ToString();
        HpTMP.text = userLogged.stats.health_point.ToString();
        OvTMP.text = userLogged.stats.offensive_value.ToString();
        DvTMP.text = userLogged.stats.defensive_value.ToString();
        IvTMP.text = userLogged.stats.intelligence_value.ToString();
        SvTMP.text = userLogged.stats.speed_value.ToString();
        MvTMP.text = userLogged.stats.mana_value.ToString();
        AvImg.texture = AtImg.texture;

        HpBtn.onClick.AddListener(() => AddPointLevel("HpBtn"));
        OvBtn.onClick.AddListener(() => AddPointLevel("OvBtn"));
        DvBtn.onClick.AddListener(() => AddPointLevel("DvBtn"));
        IvBtn.onClick.AddListener(() => AddPointLevel("IvBtn"));
        SvBtn.onClick.AddListener(() => AddPointLevel("SvBtn"));
        MvBtn.onClick.AddListener(() => AddPointLevel("MvBtn"));
    }

    // Update is called once per frame
    void Update()
    {
        LvTMP.text = userLogged.stats.level.ToString();
        XpTMP.text = userLogged.stats.exp.ToString();
        GsTMP.text = userLogged.stats.gold.ToString();
        GpTMP.text = userLogged.stats.gold_premium.ToString();
        PlTMP.text = userLogged.stats.level_point.ToString();
        HpTMP.text = userLogged.stats.health_point.ToString();
        OvTMP.text = userLogged.stats.offensive_value.ToString();
        DvTMP.text = userLogged.stats.defensive_value.ToString();
        IvTMP.text = userLogged.stats.intelligence_value.ToString();
        SvTMP.text = userLogged.stats.speed_value.ToString();
        MvTMP.text = userLogged.stats.mana_value.ToString();
    }

    void AddPointLevel(string nameBtn){
        if(userLogged.stats.level_point > 0){
            switch (nameBtn) {
                case "HpBtn":
                    userLogged.stats.health_point++;
                    userLogged.stats.level_point--;
                    break;
                case "OvBtn":
                    userLogged.stats.offensive_value++;
                    userLogged.stats.level_point--;
                    break;
                case "DvBtn":
                    userLogged.stats.defensive_value++;
                    userLogged.stats.level_point--;
                    break;
                case "IvBtn":
                    userLogged.stats.intelligence_value++;
                    userLogged.stats.level_point--;
                    break;
                case "SvBtn":
                    userLogged.stats.speed_value++;
                    userLogged.stats.level_point--;
                    break;
                case "MvBtn":
                    userLogged.stats.mana_value++;
                    userLogged.stats.level_point--;
                    break;
                default :
                    
                    break;
            }
        }
    }

}
