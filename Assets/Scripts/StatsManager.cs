using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static int deadChildren = 0;
    public static int deadWomen = 0;
    public static int deadMen = 0;

    private static StatsManager original;
    private void Awake()
    {
        if (original != this)
        {
            if (original != null)
                Destroy(original.gameObject);
            DontDestroyOnLoad(gameObject);
            original = this;
        }
    }
}
