using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private string speakerName = "NPC";
    [TextArea(2, 5)]
    [SerializeField] private string[] lines;

    private void Reset()
    {
        var c = GetComponent<Collider2D>();
        c.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (DialogueUI.Instance == null || DialogueUI.Instance.IsOpen) return;

        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        DialogueUI.Instance.Open(speakerName, lines, pm);
    }
}