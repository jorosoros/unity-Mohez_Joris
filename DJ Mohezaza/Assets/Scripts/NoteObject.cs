using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect, pressedEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                Instantiate(pressedEffect, transform.position, Quaternion.identity);

                if (Mathf.Abs(transform.position.y) > 0.60) 
                {
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, hitEffect.transform.position, Quaternion.identity);
                } 
                
                else if (Mathf.Abs(transform.position.y) > 0.30)
                {
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, goodEffect.transform.position, Quaternion.identity);
                }

                else
                {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, perfectEffect.transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            Instantiate(missEffect, missEffect.transform.position, Quaternion.identity);
        }
    }
}
