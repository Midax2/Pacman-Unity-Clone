using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform[] ghostSpawnPoints;

    public void SpawnPlayer()
    {
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            playerPrefab.name = "Player";
        }
    }

    public void SpawnGreenGhost()
    {
        if (ghostPrefab != null && ghostSpawnPoints != null && ghostSpawnPoints.Length > 0)
        {
            GameObject ghost = Instantiate(ghostPrefab, ghostSpawnPoints[0].position, ghostSpawnPoints[0].rotation);
            ghost.name = "Green";
        }
    }

    public void SpawnOrangeGhost()
    {
        if (ghostPrefab != null && ghostSpawnPoints != null && ghostSpawnPoints.Length > 1)
        {
            GameObject ghost = Instantiate(ghostPrefab, ghostSpawnPoints[1].position, ghostSpawnPoints[1].rotation);
            ghost.name = "Orange";
        }
    }

    public void SpawnPinkGhost()
    {
        if (ghostPrefab != null && ghostSpawnPoints != null && ghostSpawnPoints.Length > 2)
        {
            GameObject ghost = Instantiate(ghostPrefab, ghostSpawnPoints[2].position, ghostSpawnPoints[2].rotation);
            ghost.name = "Pink";
        }
    }

    public void SpawnRedGhost()
    {
        if (ghostPrefab != null && ghostSpawnPoints != null && ghostSpawnPoints.Length > 3)
        {
            GameObject ghost = Instantiate(ghostPrefab, ghostSpawnPoints[3].position, ghostSpawnPoints[3].rotation);
            ghost.name = "Red";
        }
    }
}