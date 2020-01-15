using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private FMOD.Studio.Bus master;
    private FMOD.Studio.Bus music;
    float masterVolume = 1.0f;
    float musicVolume = 0.5f;

    private void Awake()
    {
        master = FMODUnity.RuntimeManager.GetBus("Bus:/MasterBus");
    }

    public void SetMasterVolume(float newVolume)
    {
        Debug.Log("new volume");
        masterVolume = newVolume;
        master.setVolume(newVolume);
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        music.setVolume(newVolume);
    }

    public void MuteMasterVolume()
    {
        musicVolume = 0;
        music.setVolume(0);
    }
}
