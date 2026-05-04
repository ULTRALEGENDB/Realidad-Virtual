using System.Collections;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource normalMusic;
    public AudioSource chaseMusic;

    private int chasingEnemies = 0;
    private Coroutine currentFade;

    void Start()
    {
        normalMusic.volume = 0.8f;
        chaseMusic.volume = 0f;
        normalMusic.Play();
    }

    public void SetChasing(bool chasing)
    {
        if (chasing)
            chasingEnemies++;
        else
            chasingEnemies--;

        chasingEnemies = Mathf.Max(0, chasingEnemies);

        if (chasingEnemies > 0)
        {
            StartFade(normalMusic, chaseMusic);
        }
        else
        {
            StartFade(chaseMusic, normalMusic);
        }
    }

    void StartFade(AudioSource from, AudioSource to)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeMusic(from, to));
    }

    IEnumerator FadeMusic(AudioSource from, AudioSource to)
    {
        float duration = 1.5f; 
        float time = 0f;

        if (!to.isPlaying)
            to.Play();

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            from.volume = 0.8f - t * 0.8f;
            to.volume = t * 0.8f;

            yield return null;
        }

        from.Stop();
        from.volume = 0.8f;
        to.volume = 0.8f;
    }
}