using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePrefabsAndStart : MonoBehaviour
{
    [SerializeField] TMP_Dropdown name;
    [SerializeField] TMP_Dropdown surname;
    [SerializeField] TMP_Dropdown title;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        PlayerPrefs.SetString("name", title.options[title.value].text + " " + name.options[name.value].text + " " + surname.options[surname.value].text);
        print("player name save as " + title.options[title.value].text + " " + name.options[name.value].text + " " + surname.options[surname.value].text);
        SceneManager.LoadScene(1);
    }
}
