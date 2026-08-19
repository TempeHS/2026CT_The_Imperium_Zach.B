using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnResolver : MonoBehaviour
{
    private void Start()
    {

        Debug.Log($"[PlayerSpawnResolver] Start in scene '{SceneManager.GetActiveScene().name}', pendingSpawnId='{SceneTransitionState.PendingSpawnId}'");

        if (string.IsNullOrWhiteSpace(SceneTransitionState.PendingSpawnId)) return;

        SpawnPoint2D[] points = FindObjectsByType<SpawnPoint2D>(FindObjectsSortMode.None);
        Debug.Log($"[PlayerSpawnResolver] Found {points.Length} spawn points in scene.");

        foreach (var p in points)
        {
            Debug.Log($"[PlayerSpawnResolver] Checking spawnId '{p.spawnId}' at {p.transform.position}");
            if (p.spawnId != SceneTransitionState.PendingSpawnId) continue;

            PlayerMovement pm = GetComponent<PlayerMovement>();
            if (pm != null) pm.Teleport(p.transform.position);
            else transform.position = p.transform.position;

            Debug.Log($"[PlayerSpawnResolver] Teleported player to spawn '{p.spawnId}'");
            SceneTransitionState.PendingSpawnId = null;
            return;
        }
        Debug.Log($"[PlayerSpawnResolver] No matching spawn point found for '{SceneTransitionState.PendingSpawnId}'!");
    }
}