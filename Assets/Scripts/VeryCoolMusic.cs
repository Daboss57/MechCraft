using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;


public class VeryCoolMusic : MonoBehaviour
{

    
    public bool music = true;
    public List<int> startingNumbers = new List<int>() {0, 1, 2};
    public AudioClip marimba;
    public GameObject audioSource;
    List<AudioSource> audioSources;

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        audioSources = CreateAudioSources(10);

        if (music) 
        {
            StartCoroutine(CoolMusicThing());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    List<AudioSource> CreateAudioSources(int count) 
    {
        List<AudioSource> sources = new List<AudioSource>();
        for (int i = 0; i < count; i++) {
            GameObject thing = Instantiate(audioSource);
            thing.GetComponent<AudioSource>().pitch = (float)(i + 1) * 0.2f;

            sources.Add(thing.GetComponent<AudioSource>());
        }
        return sources;
    }

    IEnumerator CoolMusicThing()
    {
        List<int> numberz = startingNumbers;


        while(true) {
            yield return new WaitForSeconds(0.2f);
            
            int sum = (numberz[0] + numberz[1] + numberz[2]) % 10;

            numberz.Add(sum);
            numberz.RemoveAt(0);
            // audioSource.pitch = numberz[2];

            audioSources[numberz[2]].PlayOneShot(marimba, 0.7F);
            // Debug.Log("playing " + numberz[2]);

            // just some random code that hopefully sounds good
            if ((numberz[0] + numberz[1]) % 7 == 0) 
            {
                // Debug.Log("qwerty");
                StartCoroutine(idk((3 * numberz[1]) % 10));
            }

            // Debug.Log(numberz.Count);
        }
    }

    IEnumerator idk(int note)
    {
        yield return new WaitForSeconds(0.1f);
        audioSources[note].PlayOneShot(marimba, 0.6F);
        // Debug.Log("qwerty");
    }

}

