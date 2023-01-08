using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (MySceneManager.firstTimePlaying)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetMouseButtonDown(0) && this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
        }
*/
    }

    public void OnClickToggleHelpPanel()
    {
        this.gameObject.SetActive(!this.gameObject.activeInHierarchy);
    }
}
