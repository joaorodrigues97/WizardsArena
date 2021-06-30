using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class AudioSettingsPrefs : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    private float backgroundFloat, soundEffectsFloat;

    public AudioSource backgroundAudio;
    public AudioSource soundEffectsAudio;

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
    }

    public void LoadOnline()
    {

        //SceneManager.LoadScene("HomeMenu");
        StartCoroutine(WaitAndLoadOnline());
    }

    private IEnumerator WaitAndLoadOnline()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);
        SceneManager.LoadScene("HomeMenu");
    }

    public void LoadConf()
    {

        //SceneManager.LoadScene("HomeMenu");
        StartCoroutine(WaitAndLoadConf());
    }

    private IEnumerator WaitAndLoadConf()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);
        SceneManager.LoadScene("ConfigMenu");
    }

    public void firstMenu()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("FirstMenu");
    }

    public void CreateRoomSound()
    {
        soundEffectsAudio.Play();
    }

    public void FindRoomSound()
    {
        soundEffectsAudio.Play();
    }

    public void StartGame()
    {
        StartCoroutine(WaitAndLoadPlay());
    }

    private IEnumerator WaitAndLoadPlay()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel("WizardsArenaGame");
        }
    }

    public void LoadFirstMenu()
    {

        //SceneManager.LoadScene("HomeMenu");
        StartCoroutine(WaitAndLoadFirstMenu());
    }

    private IEnumerator WaitAndLoadFirstMenu()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);
        SceneManager.LoadScene("FirstMenu");
    }

    public void LoadHelpMenu()
    {

        //SceneManager.LoadScene("HomeMenu");
        StartCoroutine(WaitAndLoadHelpMenu());
    }

    private IEnumerator WaitAndLoadHelpMenu()
    {
        soundEffectsAudio.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(soundEffectsAudio.clip.length);
        SceneManager.LoadScene("HelpMenu");
    }
}
