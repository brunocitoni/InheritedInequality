using Michsky.MUIP;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float startingMoney = 1000f;
    [SerializeField] public static float currentMoney;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text yearlyProfitText;

    private float startingHappiness = 0f;
    [SerializeField] public static float currentHappiness;
    [SerializeField] TMP_Text happinessText;
    [SerializeField] TMP_Text happinessChangeText;

    [SerializeField] static SliderManager childPay;
    [SerializeField] public static SliderManager childWH;
    [SerializeField] static float childProfitPerHour = 5f;

    [SerializeField] static SliderManager womanPay;
    [SerializeField] public static SliderManager womanWH;
    [SerializeField] static float womanProfitPerHour = 16f;

    [SerializeField] static SliderManager manPay;
    [SerializeField] static SliderManager manWH;
    [SerializeField] static float manProfitPerHour = 50f;

    [SerializeField] static float childProfit;
    [SerializeField] static float womenProfit;
    [SerializeField] static float menProfit;

    [SerializeField] static TMP_Text childProfitText;
    [SerializeField] static TMP_Text womenProfitText;
    [SerializeField] static TMP_Text menProfitText;

    static float childHappinessGain;
    static float womenHappinessGain;
    static float menHappinessGain;

    [SerializeField] TMP_Text childPayText;
    [SerializeField] TMP_Text womanPayText;
    [SerializeField] TMP_Text manPayText;
    [SerializeField] TMP_Text childWHText;
    [SerializeField] TMP_Text womanWHText;
    [SerializeField] TMP_Text manWHText;

    public static string gameLostReason;

    // end game stats
    public static float maxHappinessReached = 0f;
    public static float minHappinessReached = 0f;
    public static float maxMoneyReached = 1000f;
    public static float minMoneyReached = 1000f;

    [SerializeField] AudioSource backgroundSound;

    public static float totalGameProfit = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentMoney = startingMoney;
        currentHappiness = startingHappiness;

        childPay = GameObject.Find("ChildPaySlider").GetComponent<SliderManager>();
        childWH = GameObject.Find("ChildWorkHoursSlider").GetComponent<SliderManager>();

        womanPay = GameObject.Find("WomanPaySlider").GetComponent<SliderManager>();
        womanWH = GameObject.Find("WomanWorkHoursSlider").GetComponent<SliderManager>();

        manPay = GameObject.Find("ManPaySlider").GetComponent<SliderManager>();
        manWH = GameObject.Find("ManWorkHoursSlider").GetComponent<SliderManager>();

        childProfitText = GameObject.Find("ChildProductionAmount").GetComponent<TMP_Text>();
        womenProfitText = GameObject.Find("WomenProductionAmount").GetComponent<TMP_Text>();
        menProfitText = GameObject.Find("MenProductionAmount").GetComponent<TMP_Text>();

        // reset gameLossReason in case of previous bankrupt
        gameLostReason = "";
    }

    // Update is called once per frame
    void Update()
    {
        // get money and happiness stats
        if (currentMoney > maxMoneyReached)
        {
            maxMoneyReached = currentMoney;
        }
        if (currentMoney < minMoneyReached)
        {
            minMoneyReached = currentMoney;
        }
        if (currentHappiness > maxHappinessReached)
        {
            maxHappinessReached= currentHappiness;
        }
        if (currentHappiness < minHappinessReached)
        {
            minHappinessReached= currentHappiness;
        }

        // check game over conditions
        if (currentMoney < 0)
        {
            gameLostReason = "bankrupt";
            backgroundSound.Stop();
            TimeManager.TriggerGameOver();
        }

        CalculateChildProfit();
        CalculateWomenProfit();
        CalculateMenProfit();

        CalculateChildHappinesGain();
        CalculateWomenHappinesGain();
        CalculateMenHappinesGain();

        moneyText.text = "Capital: £"+Math.Round(currentMoney, 2).ToString();
        yearlyProfitText.text = "£"+Math.Round(childProfit + womenProfit + menProfit, 2).ToString();

        if (!MySceneManager.firstTimePlaying)
        {
            happinessText.text = "Total Happiness: " + Math.Round(currentHappiness, 2).ToString();
            happinessChangeText.text = "Yearly happiness change: " + Math.Min(Math.Round(childHappinessGain + womenHappinessGain + menHappinessGain, 2), 10).ToString();
        }

        childPayText.text = "£" + Math.Round(childPay.mainSlider.value,2).ToString() +" yearly pay";
        womanPayText.text = "£" + Math.Round(womanPay.mainSlider.value,2).ToString() + " yearly pay";
        manPayText.text = "£" + Math.Round(manPay.mainSlider.value,2).ToString() + " yearly pay";

        childWHText.text = childWH.mainSlider.value.ToString() + " hours work days";
        womanWHText.text = womanWH.mainSlider.value.ToString() + " hours work days";
        manWHText.text = manWH.mainSlider.value.ToString() + " hours work days";

    }

    public static void CalculateEarnings()
    {
        currentMoney = currentMoney + childProfit+womenProfit+menProfit;
        totalGameProfit = totalGameProfit + childProfit + womenProfit + menProfit;
    }

    public static void CalculateChildProfit()
    {
        childProfit = (float)Math.Round(WorkforceManager.childWorkers*((childWH.mainSlider.value * childProfitPerHour) - childPay.mainSlider.value),2);
        childProfitText.text = "Produce £" + childProfit.ToString() + " per year";
    }

    public static void CalculateWomenProfit()
    {
        womenProfit = (float)Math.Round(WorkforceManager.womenWorkers * ((womanWH.mainSlider.value * womanProfitPerHour) - womanPay.mainSlider.value),2);
        womenProfitText.text = "Produce £" + womenProfit.ToString() + " per year";  
    }

    public static void CalculateMenProfit()
    {
        menProfit = (float)Math.Round(WorkforceManager.menWorkers * ((manWH.mainSlider.value * manProfitPerHour) - manPay.mainSlider.value),2);
        menProfitText.text = "Produce £" + menProfit.ToString() + " per year";  
    }

    public static void CalculateHappiness()
    {
        float toAdd = Math.Min(childHappinessGain + womenHappinessGain + menHappinessGain, 10);
        currentHappiness = currentHappiness + toAdd;

    }

    public static void CalculateChildHappinesGain()
    {
        if (WorkforceManager.childWorkers > 0) {
            childHappinessGain = (float)Math.Round(((childPay.mainSlider.normalizedValue + 0.5) - (childWH.mainSlider.normalizedValue + 0.25)) * 5, 2);
        } else
        {
            childHappinessGain = 0;
        }
    }

    public static void CalculateWomenHappinesGain()
    {
        if (WorkforceManager.womenWorkers > 0)
        {
            womenHappinessGain = (float)Math.Round(((womanPay.mainSlider.normalizedValue + 0.5) - (womanWH.mainSlider.normalizedValue + 0.25)) * 2, 2);
        } else
        {
            womenHappinessGain= 0;
        }
    }

    public static void CalculateMenHappinesGain()
    {
        if (WorkforceManager.menWorkers > 0)
        {
            menHappinessGain = (float)Math.Round(((manPay.mainSlider.normalizedValue + 0.5) - (manWH.mainSlider.normalizedValue + 0.25)) * 1, 2);
        } else
        {
            menHappinessGain = 0;
        }
    }

    public static void CalculateEvent()
    {
        // resolve any time based events
        if (!TimeManager.CheckForNewspaperEvents()) // if no big event occur
        {
            if (PercentageRoll(15 + (currentHappiness / 100) * (WorkforceManager.womenWorkers)))
            {
                //women gets pregnant and child comes to work for us
                TimeManager.PrintToEventText("A child was born into the job!");
                WorkforceManager.childWorkers++;
                if (PercentageRoll(5))
                {
                    TimeManager.PrintToEventText("Twins were born in the job but unfortunately the mother did not make it through childbirth");
                    WorkforceManager.childWorkers++;
                    WorkforceManager.womenWorkers--;
                    StatsManager.deadWomen++;
                }
            }
            else if (WorkforceManager.childWorkers >= 1 && PercentageRoll(5 - currentHappiness / 25))
            {
                if (PercentageRoll(50))
                {
                    TimeManager.PrintToEventText("A small child got crushed under one of the heavier machines!");
                }
                else
                {
                    TimeManager.PrintToEventText("A child collapsed after a long shift!");
                }
                WorkforceManager.childWorkers--;
                StatsManager.deadChildren++;
            }
            else if (WorkforceManager.womenWorkers + WorkforceManager.menWorkers >= 20 && PercentageRoll(50 - currentHappiness/100))
            {
                TimeManager.PrintToEventText("The workers strike all year and some of them get killed by the police");
                if (WorkforceManager.womenWorkers >= 1)
                {
                    WorkforceManager.womenWorkers--;
                    StatsManager.deadWomen++;
                }
                if (WorkforceManager.menWorkers >= 1)
                {
                    WorkforceManager.menWorkers--;
                    StatsManager.deadMen++;
                }
                if (WorkforceManager.menWorkers >= 1)
                {
                    WorkforceManager.menWorkers--;
                    StatsManager.deadMen++;
                }
            }
        }
    }

    public static bool PercentageRoll(float desideredProbability)
    {
        float rand = UnityEngine.Random.Range(0, 100);
        if (rand <= desideredProbability)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
