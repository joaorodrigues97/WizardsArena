using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMenuController : MonoBehaviour
{
    public void LoadOnline()
    {
        SceneManager.LoadScene("HomeMenu");
    }
}
