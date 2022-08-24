using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class CubeStack : MonoBehaviour
{
    [SerializeField] private spawnCube _spawnCube;
    
    [SerializeField] private GameObject _easyTriggerVolume;
    [SerializeField] private GameObject _mediumTriggerVolume;
    [SerializeField] private GameObject _hardTriggerVolume;

    public TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _timerText;
    
    [SerializeField] private TextMeshProUGUI _cowntownText;
    [SerializeField] private TextMeshProUGUI _wonLostText;
    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;
    [SerializeField] private TextMeshProUGUI _volumeText;

    public int levelNum = 0;
    public float _timer = 0f;
    public float _cowntdowntimer = 3f;
    
    private float _timeforeasy = 11f;
    private float _timeformedium = 9f;
    private float _timeforhard = 7f;

    private float _volumeforeasy = 1000f;
    private float _volumeformedium = 1000f;
    private float _volumeforhard = 1000f;
    private float _volume = 0f;

    public bool _heightreached = false;
    public bool _gamestarted = false;
    public bool _gameinprogress = false;
    public bool cowntowndowninprogress = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gamestarted) return;
        
        if (_gameinprogress) //gameinprogress
        {
            _volume = _spawnCube.totalVolume;

            if (levelNum == 1)
            {
                _volumeText.text = "VOLUME LEFT : " + (_volumeforeasy-_volume);
                if (_timer >= _timeforeasy || _volume >=_volumeforeasy)
                {
                    LostGame();
                }
                else if (_easyTriggerVolume.GetComponent<trigger>().triggered == true)
                {
                    _heightreached = true;
                    _gameinprogress = false;
                }
            }
            else if (levelNum == 2)
            {
                _volumeText.text = "VOLUME LEFT : " + (_volumeformedium-_volume);
                if (_timer >= _timeformedium || _volume >=_volumeformedium)
                {
                    LostGame();
                }
                else if (_mediumTriggerVolume.GetComponent<trigger>().triggered == true)
                {
                    _heightreached = true;
                    _gameinprogress = false;
                }
            }
            else if (levelNum == 3)
            {
                _volumeText.text = "VOLUME LEFT : " + (_volumeforhard-_volume);
                if (_timer >= _timeforhard || _volume >=_volumeforhard)
                {
                    LostGame();
                }
                else if (_hardTriggerVolume.GetComponent<trigger>().triggered == true)
                {
                    _heightreached = true;
                    _gameinprogress = false;
                }
            }
            _timer += Time.deltaTime;
            _timerText.text = "TIMER: " + _timer;
        }
        
        
        else if(!_gameinprogress && cowntowndowninprogress)
        {
            if (_heightreached == true && _cowntdowntimer>0.0f)
            {
                _cowntdowntimer -= Time.deltaTime;
                _cowntownText.text = "END COUNTDOWN : " + _cowntdowntimer;
            } 
            else if (_cowntdowntimer <= 0f)
            {
                cowntowndowninprogress = false;
                _cowntownText.text = "COUNTDOWN OVER";
                if (levelNum == 1 && _easyTriggerVolume.GetComponent<trigger>().triggered == false)
                {
                    LostGame();
                }
                else if (levelNum == 2 && _mediumTriggerVolume.GetComponent<trigger>().triggered == false)
                {
                    LostGame();
                }
                else if (levelNum == 3 && _hardTriggerVolume.GetComponent<trigger>().triggered == false)
                {
                    LostGame();
                }
                else
                {
                    WonGame();
                }
            }
            
        }
        
    }

    public void OnStart()
    {
        _gamestarted = true;
        _gameinprogress = true;
    }
    
    private void WonGame()
    {
        _wonLostText.text = "YOU WON!";
        SetStars();
    }

    private void SetStars()
    {
        if (levelNum == 1)
        {
            _star1.SetActive(true);
        }
        else if (levelNum == 2)
        {
            _star1.SetActive(true);
            _star2.SetActive(true);
        }
        else if (levelNum == 2)
        {
            _star1.SetActive(true);
            _star2.SetActive(true);
        }
        else if (levelNum == 3)
        {
            _star1.SetActive(true);
            _star2.SetActive(true);
            _star3.SetActive(true);
        }
    }

    private void LostGame()
    {
        _wonLostText.text = "YOU LOST!";
        _gameinprogress = false;
    }
    
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
