using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TiledBackground : MonoBehaviour
{
    public Camera cam;
    public float zInFrontOfCamera = 10f; // with default camera z=-10, result z=0
    public float tileScale = 1f;         // bigger = bigger repeated tiles
    public float offsetX = 0f;
    public float offsetY = 0f;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (cam == null) cam = Camera.main;
    }

    void LateUpdate()
    {
        if (cam == null || !cam.orthographic) return;

        float h = cam.orthographicSize * 2f;
        float w = h * cam.aspect;

        sr.size = new Vector2(w / Mathf.Max(0.0001f, tileScale), h / Mathf.Max(0.0001f, tileScale));

        Vector3 cp = cam.transform.position;
        transform.position = new Vector3(cp.x + offsetX, cp.y + offsetY, cp.z + zInFrontOfCamera);

        transform.localScale = new Vector3(tileScale, tileScale, 1f);
    }
}