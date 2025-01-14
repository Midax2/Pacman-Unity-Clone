using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnController.SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
