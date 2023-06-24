using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour
{
    // Start is called before the first frame update

    string GooglePlay_ID = "3800849";
    bool TestMode =true;
    void Start()
    {
        Advertisement.Initialize(GooglePlay_ID, TestMode);
    }

  public void DisplayInterstitialAd()
    {
        Advertisement.Show();
    }
    
    public void StartAdTimer(float time)
    {
        Invoke("DisplayInterstitialAd", time);
    }
}
