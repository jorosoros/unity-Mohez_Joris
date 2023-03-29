using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class buttonScript : MonoBehaviour
{
    public void playSong()
    {
        SceneManager.LoadScene(3);
    }

    public void playSong2()
    {
        SceneManager.LoadScene(4);
    }
    public void backButton()
    {
        SceneManager.LoadScene(0);
    }
    public void instructButton()
    {
        SceneManager.LoadScene(2);
    }
}
