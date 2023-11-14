
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip volumeMusic;

    private AudioSource source;

    private AudioMixer mixer;

    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        mixer = Resources.Load("AudioMixer") as AudioMixer;

        print(mixer);
        // Assign Master to Audio Source
        var group = mixer.FindMatchingGroups("Master");
        source.outputAudioMixerGroup = group[0];
    }

    public void PlayNext() //TODO when has gameplay
    {
        Stop();
    }

    private void ChangeTrack(GameManager.GameState state)
    {
        Stop();

        switch(state)
        {
            case GameManager.GameState.MAIN_MENU:
                source.clip = mainMenuMusic;
                break;
        }
    }

    public void Play(GameManager.GameState state)
    {
        ChangeTrack(state);

        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    private static AudioManager instance;
    public static AudioManager Instance => instance;
}
