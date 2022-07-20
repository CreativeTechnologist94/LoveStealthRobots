using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;

    [SerializeField] private List<GameObject> _targetList;
    public float score = 0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score;
    }

    public void ResetGame()
    {
        Debug.Log("Resetting");
        score = 0f;

        for (int i = 0; i < _targetList.Count; i++)
        {
            _targetList[i].GetComponent<target>().StandBackUp();
        }
    }
}
