using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private int startYear = 1830;
    private int endYear = 1875;

    public static int currentYear;

    // text bits
    [SerializeField] TMP_Text yearText;
    [SerializeField] static TMP_Text eventText;

    public static float modifierYear;
    public static float labourCostModifierYear;

    public static bool needToSubmitScore = false;

    [SerializeField] AudioSource backgroundSound;

    private void Awake()
    {
        currentYear = startYear;
    }
    // Start is called before the first frame update
    void Start()
    {
        eventText = GameObject.Find("EventText").GetComponent<TMP_Text>();

        //start background audio
        backgroundSound.Play();

        // set initial text
        PrintToEventText("Boffins confirm the secret to achieve wealth is to start off already wealthy");
    }

    // Update is called once per frame
    void Update()
    {
        yearText.text = currentYear.ToString();
    }

    public void OnClickAdvanceTime()
    {
        // advance time 1 month 
        AdvanceOneYear();

        // calculate money earned
        MoneyManager.CalculateEarnings();

        // calculate happiness earned
        MoneyManager.CalculateHappiness();

        // here so it gets overrritten in case there's something else happening
        TimeManager.PrintToEventText("Another year goes by...");

        // calculate random event based on happines + employed people
        MoneyManager.CalculateEvent();

        if (currentYear > 1870 && WorkforceManager.childWorkers >= 1)
        {
            WorkforceManager.childWorkers--;
        }
    }

    void AdvanceOneYear()
    {
        currentYear++;
        if (currentYear == endYear + 1)
        {
            backgroundSound.Stop();
            TriggerGameOver();
        }
    }

    public static void TriggerGameOver()
    {
           if (MoneyManager.gameLostReason == "bankrupt") // if we lost because of bankrupcy
           {
              needToSubmitScore = false;
           } else // first time playing losing to bankrupt
           {
                if (MySceneManager.firstTimePlaying)
                {
                    needToSubmitScore = true;
                    MySceneManager.firstTimePlaying = false;
                }
                else
                {
                    needToSubmitScore = false;
                }
           }
        PlayerPrefs.SetInt("firstTimePlaying", MySceneManager.boolToInt(MySceneManager.firstTimePlaying));
    
        SceneManager.LoadScene(2); // load end game scene with recap
    }

    public static bool CheckForNewspaperEvents()
    {
        if (currentYear == 1833)
        {
            LawEvents.TriggerFactoryAct1833();
            return true;
        }
        if (currentYear == 1844)
        {
            LawEvents.TriggerFactoryAct1844();
            return true;
        }
        if (currentYear == 1847)
        {
            LawEvents.TriggerFactoryAct1847();
            return true;
        }
        if (currentYear == 1870)
        {
            LawEvents.TriggerEducationAct();
            return true;
        }
        return false;
    }

    void ComputeModifierByYear()
    {
        labourCostModifierYear = currentYear / startYear;
    }

    public static void PrintToEventText(string textToPrint)
    {
        eventText.text = textToPrint; 
    }
}
