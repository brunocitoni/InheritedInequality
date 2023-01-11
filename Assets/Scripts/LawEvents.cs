using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawEvents : MonoBehaviour
{
    public static void TriggerFactoryAct1833()
    {
        TimeManager.PrintToEventText("In a historic decision, Parliament decreed the Factory Act of 1833, which states that children aged 9-13 years may not work more than nine hours per day, while children aged 13-18 may not work more than 12 hours per day. This legislation aims to protect the well-being and safety of young workers in factories across the nation.");
        MoneyManager.childWH.mainSlider.maxValue = 9;
    }

    public static void TriggerFactoryAct1844()
    {
        TimeManager.PrintToEventText("In a momentous decision, Parliament decreed that women may work a maximum of 12 hours per day, with no night work allowed. The passage of this ground-breaking is a significant win for proponents of labour reform and women's rights.");
        MoneyManager.womanWH.mainSlider.maxValue = 12;
    }
    public static void TriggerFactoryAct1847()
    {
        TimeManager.PrintToEventText("In a pivotal decision, Parliament decreed the Ten Hours Act of 1847, which restricts the working hours of women and young persons (aged 13-18) in textile mills to a maximum of 10 hours per day.");
        MoneyManager.womanWH.mainSlider.maxValue = 10;
    }

    public static void TriggerEducationAct() 
    {
        TimeManager.PrintToEventText("In a crucial decision, Parliament decreed the framework for compulsory schooling of all children between the ages of 5 and 12. This groundbreaking legislation, passed in the year of our Lord 1870, marks a significant victory for advocates of education and the rights of children.");
        if (WorkforceManager.childWorkers >= 1)
        {
            WorkforceManager.childWorkers--;
        }
    }
}
