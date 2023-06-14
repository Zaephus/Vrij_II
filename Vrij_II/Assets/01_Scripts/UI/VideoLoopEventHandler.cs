using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoLoopEventHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public UnityEvent OnLoopPointReached;

    private void Start()
    {
        videoPlayer.loopPointReached += VideoLoopPointReached;
    }

    private void VideoLoopPointReached(VideoPlayer vp)
    {
        OnLoopPointReached.Invoke();
        gameObject.SetActive(false);
    }
}
