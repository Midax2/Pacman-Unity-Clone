using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform[] ghostSpawnPoints; // Array of ghost spawn points: Green - 0, Orange - 1, Pink - 2, Red - 3
    public enum Ghosts { Green, Orange, Pink, Red }

    /// <summary>
    /// Spawns the player at the specified spawn point.
    /// </summary>
    public void SpawnPlayer()
    {
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            playerPrefab.name = "Player";
        }
    }

    /// <summary>
    /// Spawns a ghost of the specified type at the corresponding spawn point.
    /// </summary>
    /// <param name="ghostType">The type of ghost to spawn.</param>
    public void SpawnGhost(Ghosts ghostType)
    {
        if (ghostPrefab != null && ghostSpawnPoints != null && ghostSpawnPoints.Length > (int)ghostType)
        {
            GameObject ghost = Instantiate(ghostPrefab, ghostSpawnPoints[(int)ghostType].position, ghostSpawnPoints[(int)ghostType].rotation);
            ghost.name = ghostType.ToString();
        }
    }
}
