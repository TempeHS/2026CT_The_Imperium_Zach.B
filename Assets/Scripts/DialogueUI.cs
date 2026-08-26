using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text bodyText;

    [Header("Input")]
    [SerializeField] private KeyCode advanceKey = KeyCode.E;

    private string[] lines;
    private int index;

    private PlayerMovement lockedPlayer;
    private Rigidbody2D lockedBody;
    private RigidbodyConstraints2D previousConstraints;

    public bool IsOpen => root != null && root.activeSelf;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        if (root != null) root.SetActive(false);
    }

    private void Update()
    {
        if (!IsOpen) return;

        if (Input.GetKeyDown(advanceKey) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            NextLine();
        }
    }

    public void Open(string speaker, string[] dialogueLines, PlayerMovement playerToLock = null)
    {
        if (dialogueLines == null || dialogueLines.Length == 0) return;

        lines = dialogueLines;
        index = 0;

        if (speakerText != null) speakerText.text = speaker;
        if (bodyText != null) bodyText.text = lines[index];
        if (root != null) root.SetActive(true);

        lockedPlayer = playerToLock;
        if (lockedPlayer != null) lockedPlayer.enabled = false;

        {
            lockedPlayer.enabled = false;

            lockedBody = lockedPlayer.GetComponent<Rigidbody2D>();
            if (lockedBody != null)
            {
                previousConstraints = lockedBody.constraints;
                lockedBody.linearVelocity = Vector2.zero;
                lockedBody.angularVelocity = 0f;
                lockedBody.constraints = previousConstraints
                    | RigidbodyConstraints2D.FreezePositionX
                    | RigidbodyConstraints2D.FreezePositionY
                    | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    public void NextLine()
    {
        index++;
        if (index >= lines.Length)
        {
            Close();
            return;
        }

        if (bodyText != null) bodyText.text = lines[index];
    }

    public void Close()
    {
        if (root != null) root.SetActive(false);

        if (lockedBody != null)
        {
            lockedBody.linearVelocity = Vector2.zero;
            lockedBody.angularVelocity = 0f;
            lockedBody.constraints = previousConstraints;
            lockedBody = null;
        }

        if (lockedPlayer != null) lockedPlayer.enabled = true;
        lockedPlayer = null;
    }
}