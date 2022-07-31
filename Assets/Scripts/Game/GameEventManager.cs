using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    public enum GameMode
    {
        FP,
        VR
    }

    [Header("Accessibility")] 
    public Handed handedness;
    public GameMode gameMode;
    
    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private float _canvasFadeTime = 2f;
    [SerializeField] private Material _skyboxMaterial;

    [Header("Audio")] 
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _caughtMusic;
    [SerializeField] private AudioClip _successMusic;

    private PlayerInput _playerInput;
    private FirstPersonController _fpController;
    private bool _isFadingIn = false;
    private float _fadeLevel = 0;
    private bool _isGoalReached = false;

    private float _initialSkyBoxAtmosphereThickness;
    private Color _initialSkyboxColor;
    private float _initialSkyBoxExposure;
    
    // Start is called before the first frame update
    void Start()
    {
        EnemeyController[] enemies = FindObjectsOfType<EnemeyController>(); //Very inefficient way
        foreach (EnemeyController enemy in enemies)
        {
            enemy.onInvestigate.AddListener(EnemyInvestigating);
            enemy.onPlayerFound.AddListener(PlayerFound);
            enemy.onReturnToPatrol.AddListener(EnemyReturntoPatrol);
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player)
        {
            _playerInput = player.GetComponent<PlayerInput>();
            _fpController = player.GetComponent<FirstPersonController>();
        }
        else
        {
            Debug.Log("There is no player in scene");
        }

        _canvasGroup.alpha = 0;
        _failedPanel.SetActive(false);
        _successPanel.SetActive(false);
        
        ResetShaderValues();
        _initialSkyBoxAtmosphereThickness = _skyboxMaterial.GetFloat("_AtmosphereThickness");
        _initialSkyboxColor = _skyboxMaterial.GetColor("_SkyTint");
        _initialSkyBoxExposure = _skyboxMaterial.GetFloat("_Exposure");
    }

    private void EnemyReturntoPatrol()
    {
        
    }

    private void PlayerFound(Transform enemyThatFoundPlayer)
    {
        if (_isGoalReached) return;
        
        _failedPanel.SetActive(true);

        if (gameMode == GameMode.FP)
        {
            _isFadingIn = true;

            _fpController.CinemachineCameraTarget.transform.LookAt(enemyThatFoundPlayer); 
            
            DeactivateInput();
        }
        else
        {
            StartCoroutine((GameOverFadeOutSaturation(1f)));
        }
        PlayBGM(_caughtMusic);
    }

    private IEnumerator GameOverFadeOutSaturation(float _startDelay = 0f)
    {
        yield return new WaitForSeconds(_startDelay);
        
        Time.timeScale = 0;
        
        float fade = 0f;

        while (fade < 1f)
        {
            fade += Time.unscaledDeltaTime / _canvasFadeTime;
            
            Shader.SetGlobalFloat("_AllWhite", fade);
            _skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(_initialSkyBoxAtmosphereThickness, 0.7f, fade));
            _skyboxMaterial.SetColor("_SkyTint", Color.Lerp(_initialSkyboxColor, Color.white, fade));
            _skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(_initialSkyBoxExposure, 8, fade));
            
            yield return null;
        }

        yield return new WaitForSecondsRealtime(2f);
        RestartScene();
    }

    private void OnDestroy()
    {
        ResetShaderValues();
    }

    private void ResetShaderValues()
    {
        Shader.SetGlobalFloat("_AllWhite", 0);
        _skyboxMaterial.SetFloat("_AtmosphereThickness", _initialSkyBoxAtmosphereThickness);
        _skyboxMaterial.SetColor("_SkyTint", _initialSkyboxColor);
        _skyboxMaterial.SetFloat("_Exposure", _initialSkyBoxExposure);
    }

    public void GoalReached()
    {
        _isFadingIn = true;
        _isGoalReached = true;

        _successPanel.SetActive(true);

        DeactivateInput();
        PlayBGM(_successMusic);
    }

    private void DeactivateInput()
    {
        _playerInput.DeactivateInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void PlayBGM(AudioClip newBgm)
    {
        if (_bgmSource.clip == newBgm) return;
        
        _bgmSource.clip = newBgm;
        _bgmSource.Play();
    }

    private void EnemyInvestigating()
    {
        
    }

    public void RestartScene()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        ResetShaderValues();
        Time.timeScale = 1;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_isFadingIn)
        {
            if(_fadeLevel < 1f)
            {
                _fadeLevel += Time.deltaTime / _canvasFadeTime;
            }
        }
        else
        {
            if(_fadeLevel > 0f)
            {
                _fadeLevel -= Time.deltaTime / _canvasFadeTime;
            }
        }
        _canvasGroup.alpha = _fadeLevel;
    }
}
