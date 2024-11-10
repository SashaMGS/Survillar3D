using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPause : MonoBehaviour
{
    public void pauseVoid()
    {
        AudioListener.volume = 0;
    }

    public void unPauseVoid()
    {
        AudioListener.volume = 1;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
        if (GameObject.FindGameObjectWithTag("ScriptsObj") != null)
            GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>().Save();
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
        // Or / And
        AudioListener.volume = silence ? 0 : 1;
    }
}
