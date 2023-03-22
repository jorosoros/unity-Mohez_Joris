using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPController : MonoBehaviour, Interactable
{
    public string sceneName;


    public void Interact()
    {
        Globals.previousLocation = SceneManager.GetActiveScene().name;
        Debug.Log("You will go trough this door");
        SceneManager.LoadScene(sceneName);
    }
}
