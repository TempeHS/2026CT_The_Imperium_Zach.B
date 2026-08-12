using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransition2D : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private string targetSpawnId;
    [SerializeField] private float triggerCooldown = 0.2f;

    private static float nextAllowedTriggerTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time < nextAllowedTriggerTime) return;

        nextAllowedTriggerTime = Time.time + triggerCooldown;

        SceneTransitionState.PendingSpawnId = targetSpawnId;

        if (!string.IsNullOrWhiteSpace(targetSceneName) &&
            targetSceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(targetSceneName);
            return;
        }

        // Same-scene fallback (no scene load): teleport by spawnId
        SpawnPoint2D[] points = FindObjectsByType<SpawnPoint2D>(FindObjectsSortMode.None);
        foreach (var p in points)
        {
            if (p.spawnId != targetSpawnId) continue;
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null) pm.Teleport(p.transform.position);
            break;
        }
    }
}