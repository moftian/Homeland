using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FModDemo : MonoBehaviour
{
    [Header("FMOD Audios")]

    [FMODUnity.EventRef]
    public string battleResultAudioEvent;

    [FMODUnity.EventRef]
    public string weatherAudioEvent;

    [FMODUnity.EventRef]
    public string battleAudioEvent;

    FMOD.Studio.EventInstance battleResultAudioInstance;
    FMOD.Studio.EventInstance weatherAudioInstance;
    FMOD.Studio.EventInstance battleAudioInstance;

    [Header("FMOD Parameters")]
    [Range(0.0f, 1.0f), SerializeField]
    public float weatherCondition;

    private void Awake()
    {
        // Chargement des audios fmod
        battleResultAudioInstance = FMODUnity.RuntimeManager.CreateInstance(battleResultAudioEvent);
        battleResultAudioInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));

        weatherAudioInstance = FMODUnity.RuntimeManager.CreateInstance(weatherAudioEvent);
        weatherAudioInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));

        battleAudioInstance = FMODUnity.RuntimeManager.CreateInstance(battleAudioEvent);
        battleAudioInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setWeatherCondition(weatherCondition);
    }

    public void startWeatherAudio()
    {
        weatherAudioInstance.start();
    }

    public void setIsVictory(bool value)
    {
        if (value)
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("isVictory", 1.0f);
        else
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("isVictory", 0.0f);
    }

    public void stopWeatherAudio()
    {
        weatherAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //weatherAudioInstance.release();
    }

    public void startBattleResultAudio()
    {
        battleResultAudioInstance.start();
    }

    public void stopBattleResultAudio()
    {
        battleResultAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void setWeatherCondition(float cond)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("weatherCondition", Mathf.Clamp(cond, 0.0f, 1.0f));
    }

    public void setZoomOut(float percentage)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("zoomOut", Mathf.Clamp(percentage, 0.0f, 1.0f));
    }

    public void startBattleMusic()
    {
        battleAudioInstance.start();
    }

    public void stopBattleMusic()
    {
        battleAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void setIsBattleBegin(bool value)
    {
        battleAudioInstance.setParameterByName("isBattleBegin", value ? 1.0f : 0.0f);
    }

    public void setDoRepeatFight(bool value)
    {
        battleAudioInstance.setParameterByName("doRepeatFight", value ? 1.0f : 0.0f);
    }

    public void setIsReEnter(bool value)
    {
        battleAudioInstance.setParameterByName("isReEnter", value ? 1.0f : 0.0f);
    }

    public void setIsNearlyEnd(bool value)
    {
        battleAudioInstance.setParameterByName("isNearlyEnd", value ? 1.0f : 0.0f);
    }

    public void setIsBattleEnd(bool value)
    {
        battleAudioInstance.setParameterByName("isBattleEnd", value ? 1.0f : 0.0f);
    }

    public void buttonClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Click");
    }

    public void buttonHover()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Hover");
    }

    public void enemy()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fighting/Enemy00");
                break;
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fighting/Enemy02");
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fighting/Enemy01");
                break;
            default:
                break;
        }
    }
}
