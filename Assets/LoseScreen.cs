using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{

    public GameObject losescreenref;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ToggleLoseScreen", 2.0f);
    }

    void ToggleLoseScreen()
    {
        losescreenref.SetActive(true);
    }
}
