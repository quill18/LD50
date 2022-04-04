using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Play(AudioClip[] clip)
    {
        if (clip == null || clip.Length == 0)
            return;

        Play( clip[ Random.Range(0, clip.Length) ] );
    }

    public static void Play(AudioClip clip)
    {
        if (clip == null)
            return;

        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }
}
