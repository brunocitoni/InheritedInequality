using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButtonScript : MonoBehaviour
{
    GameObject soundManager;
    [SerializeField] Sprite AudioOnSprite;
    [SerializeField] Sprite AudioOffSprite;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        //soundManager.gameObject.SetActive(!this.gameObject.activeInHierarchy);
        if (!MySceneManager.audioEnabled)
        {
            this.gameObject.GetComponent<Image>().sprite = AudioOffSprite;
            AudioListener.volume = 0;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = AudioOnSprite;
            AudioListener.volume = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (audioOn)
        {
            //show relevant image in this button
            this.gameObject.GetComponent<Image>().sprite = AudioOnSprite;

        } else
        {
            //show muted image
            this.gameObject.GetComponent<Image>().sprite = AudioOffSprite;
        }*/
    }

    public void OnClickMuteButton()
    {
        //soundManager.gameObject.SetActive(!this.gameObject.activeInHierarchy);
        if (!MySceneManager.audioEnabled)
        {
            this.gameObject.GetComponent<Image>().sprite = AudioOnSprite;
            AudioListener.volume = 1;
            MySceneManager.audioEnabled = true;
        } else
        {
            this.gameObject.GetComponent<Image>().sprite = AudioOffSprite;
            AudioListener.volume = 0;
            MySceneManager.audioEnabled = false;
        }
    }
}
