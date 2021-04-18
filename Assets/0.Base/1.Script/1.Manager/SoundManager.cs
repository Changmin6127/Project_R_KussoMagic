using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour   //Data Field
{
    private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private AudioSource broadAudioSource;
}

public partial class SoundManager : MonoBehaviour   //Function Field
{
    public void SoundSignup(AudioClip _clip)
    {
        sounds.Add(_clip.name, _clip);
    }
    public void BroadAudioSourceSignup(AudioSource _audioSource)
    {
        broadAudioSource = _audioSource;
    }
    public AudioClip GetSound(string _name)
    {
        return sounds[_name];
    }
    public void BroadSound(string _name)
    {
        broadAudioSource.PlayOneShot(sounds[_name]);
    }
}
