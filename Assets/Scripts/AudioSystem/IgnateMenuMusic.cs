using UnityEngine;

public class IgnateMenuMusic : MonoBehaviour
{

    public AudioController AudioController => AudioController.Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioController.PlayAudio("MenuMusic");
    }

    public void TransitionToGameMusic()
    {
        AudioController.SoftAudioTransition("GameMusic",2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
