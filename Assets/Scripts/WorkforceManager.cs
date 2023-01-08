using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkforceManager : MonoBehaviour
{
    [SerializeField] public static int childWorkers;
    [SerializeField] public static int womenWorkers;
    [SerializeField] public static int menWorkers;

    [SerializeField] TMP_Text employedChild;
    [SerializeField] TMP_Text employedWomen;
    [SerializeField] TMP_Text employedMen;

    [SerializeField] TMP_Text costOfChildText;
    [SerializeField] TMP_Text costOfWomanText;
    [SerializeField] TMP_Text costOfManText;

    private int costOfChild = 50;
    private int costOfWoman = 200;
    private int costOfMan = 500;

    // Start is called before the first frame update
    void Start()
    {
        childWorkers = 3;
        womenWorkers = 1;
        menWorkers = 1;
    }

    // Update is called once per frame
    void Update()
    {
        employedChild.text = childWorkers.ToString();
        employedWomen.text = womenWorkers.ToString();
        employedMen.text = menWorkers.ToString();

        costOfChildText.text = "Hire a child for £" + costOfChild.ToString();
        costOfWomanText.text = "Hire a woman for £" + costOfWoman.ToString();
        costOfManText.text = "Hire a man for £" + costOfMan.ToString();
    }

    public void OnClickAddChild()
    {
        if (MoneyManager.currentMoney >= costOfChild)
        {
            childWorkers++;
            MoneyManager.currentMoney = MoneyManager.currentMoney - costOfChild;
            costOfChild = (int)Math.Round(1.05 * costOfChild);
        }
    }

    public void OnClickAddWoman()
    {
        if (MoneyManager.currentMoney >= costOfWoman)
        {
            womenWorkers++;
            MoneyManager.currentMoney = MoneyManager.currentMoney - costOfWoman;
            costOfWoman = (int)Math.Round(1.05 * costOfWoman);
        }
    }

    public void OnClickAddMan()
    {
        if (MoneyManager.currentMoney >= costOfMan)
        {
            menWorkers++;
            MoneyManager.currentMoney = MoneyManager.currentMoney - costOfMan;
            costOfMan = (int)Math.Round(1.05 * costOfMan);
        }
    }
}
