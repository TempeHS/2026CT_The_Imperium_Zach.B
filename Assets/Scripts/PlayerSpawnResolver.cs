using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnResolver : MonoBehaviour
{
    private void Start()
    {
        if (string.IsNullOrWhiteSpace(SceneTransitionState.PendingSpawnId)) return;

        SpawnPoint2D[] points = FindObjectsByType<SpawnPoint2D>(FindObjectsSortMode.None);
        foreach (var p in points)
        {
            if (p.spawnId != SceneTransitionState.PendingSpawnId) continue;

            PlayerMovement pm = GetComponent<PlayerMovement>();
            if (pm != null) pm.Teleport(p.transform.position);
            else transform.position = p.transform.position;

            SceneTransitionState.PendingSpawnId = null;
            return;
        }
    }
}