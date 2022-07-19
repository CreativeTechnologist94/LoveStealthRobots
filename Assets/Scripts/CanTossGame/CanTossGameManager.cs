using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanTossGameManager : MonoBehaviour
{
   
    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _successPanel;

    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _caughtMusic;
    [SerializeField] private AudioClip _successMusic;

    [SerializeField] private GameObject _ball01, _ball02, _ball03;
    [SerializeField] private GameObject _canGroup;

    //public int canCount = 0;
    private float delay = 5f;
    void Update()
    {
        if ((_ball01.GetComponent<Ball>().ballHit == true && _ball02.GetComponent<Ball>().ballHit == true &&
            _ball03.GetComponent<Ball>().ballHit == true) || AllFallen()) 
        {
            Debug.Log("All balls are hit");
            CanFallCheck();
            //Invoke(nameof(ResetGame), delay);
            Invoke(nameof(RestartScene), delay);
        }
    }

    private void CanFallCheck()
    {
        if (AllFallen())
        {
            _successPanel.SetActive(true);
            PlayBGM(_successMusic);
        }
        else
        {
            _failedPanel.SetActive(true);
            PlayBGM(_caughtMusic);
        }
    }
    
    private void PlayBGM(AudioClip newBgm)
    {
        if (_bgmSource.clip == newBgm) return;
        
        _bgmSource.clip = newBgm;
        _bgmSource.Play();
    }

    private bool AllFallen()
    {
        foreach (Transform child in _canGroup.transform)
        {
            if (child.GetComponent<Can>().hasFallen == false)
            {
                return false;
            }
        }
        Debug.Log("All cans fallen");
        return true;
    }
    
    /*private void ResetGame()
    {
        Debug.Log("Resetting Game");
        _ball01.GetComponent<Ball>().RepositionBall();
        _ball02.GetComponent<Ball>().RepositionBall();
        _ball03.GetComponent<Ball>().RepositionBall();
        foreach (Transform child in _canGroup.transform)
        {
            child.GetComponent<Can>().RepositionCan();
        }
        _successPanel.SetActive(false);
        _failedPanel.SetActive(false);
    }
    */
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
