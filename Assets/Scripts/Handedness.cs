using UnityEngine;

public enum Handed          //static enum
{
    Left,
    Right
}
public class Handedness : MonoBehaviour
{
    public Handed handed;
    [SerializeField] private GameEventManager _gameManager;
    [SerializeField] private GameObject[] leftHandedObjects;
    [SerializeField] private GameObject[] rightHandedObjects;

    private void Start()
    {
        handed = _gameManager.handedness;
        
        if (handed == Handed.Left)
        {
            foreach (var obj in leftHandedObjects)
            {
                obj.SetActive(true);
            }
            
            foreach (var obj in rightHandedObjects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (var obj in leftHandedObjects)
            {
                obj.SetActive(false);
            }
            
            foreach (var obj in rightHandedObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
