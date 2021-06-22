using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioSettingsScene : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private static readonly string SpellsPref = "SpellsPref";

    private float backgroundFloat, soundEffectsFloat, spellsFloat;

    public AudioSource backgroundAudio;
    public AudioSource soundEffectsAudio;
    public AudioSource[] spellsAudio;

    public GameObject inventory;
    public GameObject quitMenu;
    private bool quitOpen = false;

    public Sprite sound, mute;
    public Image soundImg;
    private bool isMuted = false;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        backgroundAudio.volume = backgroundFloat;
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
        soundEffectsAudio.volume = soundEffectsFloat;
        spellsFloat = PlayerPrefs.GetFloat(SpellsPref);
        for (int i = 0; i < spellsAudio.Length; i++)
        {
            spellsAudio[i].volume = spellsFloat;
        }
    }

    public void basicSound()
    {
        spellsAudio[0].Play();
    }

    public void skill1Sound()
    {
        spellsAudio[1].Play();
    }

    public void skill2Sound()
    {
        spellsAudio[2].Play();
    }

    public void skill3Sound()
    {
        spellsAudio[3].Play();
    }

    public void btnClickSound()
    {
        soundEffectsAudio.Play();
    }


    public void DisconnectPlayer()
    {
        soundEffectsAudio.Play();
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }
        SceneManager.LoadScene("FirstMenu");
    }

    private void openQuitMenu()
    {
        quitMenu.SetActive(true);
        inventory.SetActive(false);
        inventory.GetComponent<inventoryMain>().setInventoryOpen(false);
        quitOpen = true;
    }

    private void closeQuitMenu()
    {
        quitMenu.SetActive(false);
        quitOpen = false;
    }

    public void controllQuitMenu()
    {
        if (quitOpen)
        {
            closeQuitMenu();
        }
        else
        {
            openQuitMenu();
        }
    }

    public void setExitMenu(bool isOpen)
    {
        quitOpen = isOpen;
    }

    public void setSound()
    {
        if (isMuted)
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundAudio.volume = backgroundFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsAudio.volume = soundEffectsFloat;
            spellsFloat = PlayerPrefs.GetFloat(SpellsPref);
            for (int i = 0; i < spellsAudio.Length; i++)
            {
                spellsAudio[i].volume = spellsFloat;
            }
            soundImg.sprite = sound;
            isMuted = false;
        }
        else
        {
            backgroundAudio.volume = 0;
            soundEffectsAudio.volume = 0;
            for (int i = 0; i < spellsAudio.Length; i++)
            {
                spellsAudio[i].volume = 0;
            }
            soundImg.sprite = mute;
            isMuted = true;
        }
    }

}
