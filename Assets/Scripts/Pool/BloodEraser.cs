using UnityEngine;

public class BloodEraser : MonoBehaviour
{
    public Renderer bloodPlaneRenderer;
    public Texture2D eraseBrush;
    public float eraseSizeUV = 0.05f;

    public Transform mopHead;       // Transform de la tête
    public string mopTag = "Mop";

    public int raysX = 3;           // Nombre de raycasts horizontal
    public int raysZ = 3;           // Nombre de raycasts vertical

    private RenderTexture maskRT;
    private MaterialPropertyBlock block;

    void Start()
    {
        block = new MaterialPropertyBlock();
        bloodPlaneRenderer.GetPropertyBlock(block);

        maskRT = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGB32);
        maskRT.Create();

        RenderTexture.active = maskRT;
        GL.Clear(true, true, Color.white);
        RenderTexture.active = null;

        block.SetTexture("_Mask", maskRT);
        bloodPlaneRenderer.SetPropertyBlock(block);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(mopTag)) return;

        // Taille approximative de la tête de la serpillière
        Vector3 headSize = mopHead.localScale;

        for (int i = 0; i < raysX; i++)
        {
            for (int j = 0; j < raysZ; j++)
            {
                float offsetX = Mathf.Lerp(-headSize.x / 2f, headSize.x / 2f, i / (float)(raysX - 1));
                float offsetZ = Mathf.Lerp(-headSize.z / 2f, headSize.z / 2f, j / (float)(raysZ - 1));

                Vector3 rayOrigin = mopHead.position + new Vector3(offsetX, 0, offsetZ);
                PaintAtWorldPosition(rayOrigin);
            }
        }
    }

    void PaintAtWorldPosition(Vector3 worldPos)
    {
        Ray ray = new Ray(worldPos + Vector3.up * 0.05f, Vector3.down);
        if (!Physics.Raycast(ray, out RaycastHit hit, 1f)) return;
        if (hit.collider.gameObject != bloodPlaneRenderer.gameObject) return;

        Vector2 uv = hit.textureCoord;

        int x = (int)((uv.x - eraseSizeUV) * maskRT.width);
        int y = (int)((uv.y - eraseSizeUV) * maskRT.height);
        int size = (int)(eraseSizeUV * 2f * maskRT.width);

        RenderTexture.active = maskRT;
        Graphics.DrawTexture(new Rect(x, y, size, size), eraseBrush);
        RenderTexture.active = null;

        // Update du shader immédiatement
        bloodPlaneRenderer.GetPropertyBlock(block);
        block.SetTexture("_Mask", maskRT);
        bloodPlaneRenderer.SetPropertyBlock(block);
    }
}
