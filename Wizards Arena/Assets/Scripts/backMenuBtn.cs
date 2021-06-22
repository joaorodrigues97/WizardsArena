using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class backMenuBtn : MonoBehaviour
{
    public AudioSource btnQuit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMenu()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        btnQuit.Play();

        //Wait until clip finish playing
        yield return new WaitForSeconds(btnQuit.clip.length);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("FirstMenu");
    }
}
