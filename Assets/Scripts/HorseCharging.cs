using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseCharging : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHorseCharging(string path)
    {
        FMOD.Studio.EventInstance hc = FMODUnity.RuntimeManager.CreateInstance(path);
        hc.setParameterByName("mySpeed", 8f);
        hc.start();
        hc.release();
    }
}
