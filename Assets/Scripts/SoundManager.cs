using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    /*
        SHORT TUTORIAL:

    1. Add sound effect files to the Resources folder in the project.
    2. Add sound effect name as AudioClip variable in this script. (public static AudioClip menuHover, etc.)
    3. Load the sound effect in the Start() method using newSoundEffect = Resources.Load<AudioClip>("SoundEffectName");
    4. Add a case to switch/case in PlaySound(string clip) method for the new sound effect.
    */

    // Audio clips for various sound effects
    public static AudioClip menuHover, menuSelect, menuBack, menuClose, menuOpen, buttonClick, jump, interact, dash;    

    static AudioSource audioSrc;
    public AudioMixer audioMixer;

    public static SoundManager instance; // Singleton instance of SoundManager

    void Awake()
    {
        // Check if an instance of SoundManager already exists
        if (instance == null)
        {
            instance = this; // Assign this instance to the static instance
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
            audioSrc = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject
        }
        else
        {
            Destroy(gameObject); // Destroy this object if another instance already exists
        }
    }


    void Start()
    {
        // Load audio clips from Resources folder
        menuHover = Resources.Load<AudioClip>("MenuHover");
        menuSelect = Resources.Load<AudioClip>("MenuSelect");
        menuBack = Resources.Load<AudioClip>("MenuBack");
        menuOpen = Resources.Load<AudioClip>("MenuOpen");
        menuClose = Resources.Load<AudioClip>("MenuClose");
        buttonClick = Resources.Load<AudioClip>("ButtonClick");
        jump = Resources.Load<AudioClip>("Jump");
        interact = Resources.Load<AudioClip>("Interact");
        dash = Resources.Load<AudioClip>("Dash");

        // Set initial volume levels from PlayerPrefs
        audioMixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master")) * 20);
        audioMixer.SetFloat("Sound", Mathf.Log10(PlayerPrefs.GetFloat("Sound")) * 20);
        audioMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("Music")) * 20);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master")) * 20);
        audioMixer.SetFloat("Sound", Mathf.Log10(PlayerPrefs.GetFloat("Sound")) * 20);
        audioMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("Music")) * 20);
    }

    public void SetVolume(AudioMixerGroup targetGroup, float value)
    {
        // Set the volume for the specified AudioMixerGroup
        audioMixer.SetFloat(targetGroup.name, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(targetGroup.name, value);
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            // Play the appropriate sound based on the clip name
            case "menuHover":
                audioSrc.PlayOneShot(menuHover);
                break;
            case "menuSelect":
                audioSrc.PlayOneShot(menuSelect);
                break;
            case "menuBack":
                audioSrc.PlayOneShot(menuBack);
                break;
            case "menuOpen":
                audioSrc.PlayOneShot(menuOpen);
                break;
            case "menuClose":
                audioSrc.PlayOneShot(menuClose);
                break;
            case "buttonClick":
                audioSrc.PlayOneShot(buttonClick);
                break;
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "interact":
                audioSrc.PlayOneShot(interact);
                break;
            case "dash":
                audioSrc.PlayOneShot(dash);
                break;
            default:
                Debug.LogWarning("Sound not found: " + clip);
                break;
        }
    }

    /*
    Made by : https://github.com/Dragonath 
    */
}
