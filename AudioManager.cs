using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoManager<AudioManager> {

    [SerializeField]
    private bool m_dontDestroyOnLoad = true;

    [SerializeField]
    private AudioSource[] m_sources;

    [SerializeField]
    private AudioSource m_gameMusic;

    [SerializeField]
    private AudioSource m_menuMusic;

    [SerializeField]
    private float m_crossfadeSpeed = 0.02f;

    Coroutine c_crossFadeMusicGameToMenuCoroutine = null;
    Coroutine c_crossFadeMusicMenuToGameCoroutine = null;

    private void Awake()
    {
        if (m_dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource tempSource = GetNonPlayingSource();

        if (tempSource != null)
        {
            tempSource.clip = clip;
            tempSource.Play();
        }
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        AudioSource tempSource = GetNonPlayingSource();

        if (tempSource != null)
        {
            tempSource.clip = clip;
            tempSource.volume = volume;
            tempSource.Play();
        }
    }

    public void StopAll()
    {
        for(int i = 0; i < m_sources.Length; i++)
        {
            m_sources[i].Stop();
        }
    }

    public void PauseAll()
    {
        for (int i = 0; i < m_sources.Length; i++)
        {
            m_sources[i].Pause();
        }
    }

    public void UnPauseAll()
    {
        for (int i = 0; i < m_sources.Length; i++)
        {
            m_sources[i].UnPause();
        }
    }

    public void PlayGameMusic()
    {
        m_gameMusic.volume = 1;
        m_gameMusic.pitch = 1;
        m_gameMusic.Play();
    }

    public void SetGameMusicPitch(float pitch)
    {
        m_gameMusic.pitch = pitch;
    }

    public void CrossFadeGameMusicToMenuMusic()
    {
        if (c_crossFadeMusicGameToMenuCoroutine == null)
            c_crossFadeMusicGameToMenuCoroutine = StartCoroutine(CCrossFadeGameMusicToMenuMusic());
    }

    public void CrossFadeMenuMusicToGameMusic()
    {
        if (c_crossFadeMusicMenuToGameCoroutine == null)
            c_crossFadeMusicMenuToGameCoroutine = StartCoroutine(CCrossFadeMenuMusicToGameMusic());
    }

    IEnumerator CCrossFadeGameMusicToMenuMusic()
    {
        m_menuMusic.Play();
        while(m_gameMusic.volume > 0)
        {
            m_gameMusic.volume -= m_crossfadeSpeed;
            m_menuMusic.volume += m_crossfadeSpeed;
            yield return new WaitForEndOfFrame();
        }

        m_gameMusic.volume = 0;
        m_menuMusic.volume = 1;
        m_gameMusic.Stop();

        c_crossFadeMusicGameToMenuCoroutine = null;
    }

    IEnumerator CCrossFadeMenuMusicToGameMusic()
    {
        m_gameMusic.pitch = 1;
        m_gameMusic.Play();
        while (m_menuMusic.volume > 0)
        {
            m_gameMusic.volume += m_crossfadeSpeed;
            m_menuMusic.volume -= m_crossfadeSpeed;
            yield return new WaitForEndOfFrame();
        }

        m_gameMusic.volume = 1;
        m_menuMusic.volume = 0;
        m_menuMusic.Stop();

        c_crossFadeMusicMenuToGameCoroutine = null;
    }

    private AudioSource GetNonPlayingSource()
    {
        int index = 0;
        while (m_sources[index].isPlaying)
        {
            index++;
        }

        return m_sources[index];
    }
}
