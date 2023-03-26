using UnityEngine;

public class VideoPlay : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public GameManager instance;

    void Start()
    {
        videoPlayer.playOnAwake = false;
    }

    void Update()
    {

        if (GameManager.instance.startPlaying)
        {
            if (GameManager.instance.theMusic.isPlaying)
            {
                videoPlayer.Play();
            } 
            
            else
            {
                videoPlayer.Stop();
            }
        }
    }
}