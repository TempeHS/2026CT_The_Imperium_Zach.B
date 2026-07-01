using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button startButton; // assign your Button in Inspector

    void Start()
    {
        // Hide the button at the start
        startButton.gameObject.SetActive(false);

        // Subscribe to the event that fires when video finishes
        videoPlayer.loopPointReached += OnVideoEnd;

        videoPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        startButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        // Always unsubscribe to avoid memory leaks
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(0);
    }
}