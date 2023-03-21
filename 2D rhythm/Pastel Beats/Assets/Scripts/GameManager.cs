using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;
    public AudioSource beatSfx;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 2000;
    public int scorePerGoodNote = 2500;
    public int scorePerPerfectNote = 3000;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float NormalHits;
    public float GoodHits;
    public float PerfectHits;
    public float MissedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Press Any Key to Continue";
        
        currentMultiplier = 1;
        totalNotes = ((FindObjectsOfType<NoteObject>().Length) - 168);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                scoreText.text = "0";
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();
            }
        }
        else
        {
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                normalsText.text = "" + NormalHits;
                goodsText.text = "" + GoodHits;
                perfectsText.text = "" + PerfectHits;
                missesText.text = "" + MissedHits;

                float totalHits = NormalHits + GoodHits + PerfectHits;
                float percentHit = (totalHits / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit >= 30)
                {
                    rankVal = "D";
                    if (currentScore >= 550000)
                    {
                        rankVal = "C";
                        if (currentScore >= 750000)
                        {
                            rankVal = "B";
                            if (currentScore >= 900000)
                            {
                                rankVal = "A";
                                if (currentScore >= 950000)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }
                rankText.text = rankVal;
                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        beatSfx.Play();
        multiplierTracker++;
        if ((currentMultiplier - 1) < multiplierThresholds.Length) 
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        scoreText.text = "" + currentScore;
        multiText.text = "Score x" + currentMultiplier;  
    }

    public void NormalHit()
    {
        UnityEngine.Debug.Log("Normal Hit");
        currentScore += scorePerNote * currentMultiplier;
        NormalHits++;
        NoteHit();
    }

    public void GoodHit()
    {
        UnityEngine.Debug.Log("Good Hit");
        currentScore += scorePerGoodNote * currentMultiplier;
        GoodHits++;
        NoteHit();
    }

    public void PerfectHit()
    {
        UnityEngine.Debug.Log("Perfect Hit");
        currentScore += scorePerPerfectNote * currentMultiplier;
        PerfectHits++;
        NoteHit();
    }

    public void NoteMissed()
    {
        UnityEngine.Debug.Log("missed");
        MissedHits++;
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Score x" + currentMultiplier;

    }


}
    