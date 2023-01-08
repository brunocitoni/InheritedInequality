using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject creditsPanel;

    public static bool firstTimePlaying = true;
    public static bool audioEnabled = true;
    private static MySceneManager original;
    private void Awake()
    {
        if (original != this)
        {
            if (original != null)
                Destroy(original.gameObject);
            DontDestroyOnLoad(gameObject);
            original = this;
        }

        playButton.SetActive(true);
        panel.SetActive(false);


        //loadprefs
        if (PlayerPrefs.HasKey("firstTimePlaying"))
        {
            firstTimePlaying = intToBool(PlayerPrefs.GetInt("firstTimePlaying"));
        } else
        {
            PlayerPrefs.SetInt("firstTimePlaying", boolToInt(firstTimePlaying));
            //PlayerPrefs.Save();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickShowCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
    }

    public void OnStartGameClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickPlay()
    {
        //make button not visible and panel visible
        playButton.SetActive(false);
        panel.SetActive(true);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(1);
    }

    public static int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    public static bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

}
