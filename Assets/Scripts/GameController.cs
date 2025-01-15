using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController; // Control spawning of all game objects
    void Start()
    {
        spawnController.SpawnPlayer();
    }

}
