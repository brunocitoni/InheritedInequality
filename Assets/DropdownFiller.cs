using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownFiller : MonoBehaviour
{
    [SerializeField] TMP_Dropdown titleDropdown;
    [SerializeField] TMP_Dropdown nameDropdown;
    [SerializeField] TMP_Dropdown surnameDropdown;

    //Create a List of new Dropdown options
    List<string> m_titleOptions = new List<string> { "Mr.", "Miss", "Sir", "Lady", "Ms.", "Dr.", "Lord"};
    List<string> m_nameOptions = new List<string> { "Algernon", "Victoria", "Harold", "Felidia" , "Winslow", "Percival", "Rupert"};
    List<string> m_surnameOptions = new List<string> { "Caketown", "Doohickey", "Featherbottom", "Wigglesworth", "Fiddlesticks", "Snickerdoodle", "Fluffernutter"};

    void Start()
    {
        //Clear the old options of the Dropdown menu
        titleDropdown.ClearOptions();
        //Add the options created in the List above
        titleDropdown.AddOptions(m_titleOptions);

        //Clear the old options of the Dropdown menu
        nameDropdown.ClearOptions();
        //Add the options created in the List above
        nameDropdown.AddOptions(m_nameOptions);

        surnameDropdown.ClearOptions();
        surnameDropdown.AddOptions(m_surnameOptions);
    }

}
