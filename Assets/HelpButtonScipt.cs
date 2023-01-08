using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpButtonScipt : MonoBehaviour
{
    [SerializeField] GameObject helpPanel;
    bool clickingButton = false;
    // Start is called before the first frame update
    void Start()
    {
        if (MySceneManager.firstTimePlaying)
        {
            helpPanel.SetActive(true);
        }
        else
        {
            helpPanel.SetActive(false);
        }

    }

    public void OnClickToggleHelpPanel()
    {
        helpPanel.SetActive(!helpPanel.activeInHierarchy);
    }

}
