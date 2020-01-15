using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ocean : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string oceanAudioEvent;

    FMOD.Studio.EventInstance oceanAudioInstance;

    // Start is called before the first frame update
    void Start()
    {
        oceanAudioInstance = FMODUnity.RuntimeManager.CreateInstance(oceanAudioEvent);
        oceanAudioInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        oceanAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
