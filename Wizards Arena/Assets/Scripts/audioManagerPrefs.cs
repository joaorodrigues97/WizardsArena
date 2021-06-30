using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class audioManagerPrefs : MonoBehaviour
{

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private static readonly string SpellsPref = "SpellsPref";

    private int firstPlayInt;

    public Slider backgroundSlider, soundEffectsSlider, spellsSlider;
    private float backgroundFloat, soundEffectsFloat, spellsFloat;

    public AudioSource backgroundAudio;
    public AudioSource soundEffectsAudio;


    void Start()
    {

        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            backgroundFloat = .125f;
            soundEffectsFloat = .75f;
            spellsFloat = .5f;
            backgroundSlider.value = backgroundFloat;
            backgroundAudio.volume = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            soundEffectsAudio.volume = soundEffectsFloat;
            spellsSlider.value = spellsFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetFloat(SpellsPref, spellsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            backgroundAudio.volume = backgroundFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;
            soundEffectsAudio.volume = soundEffectsFloat;
            spellsFloat = PlayerPrefs.GetFloat(SpellsPref);
            spellsSlider.value = spellsFloat;

        }
        
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
        PlayerPrefs.SetFloat(SpellsPref, spellsSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundSlider.value;
        soundEffectsAudio.volume = soundEffectsSlider.value;
    }


    public void btnClickSound()
    {
        SaveSoundSettings();
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);

        SceneManager.LoadScene("FirstMenu");
    
    }

}
