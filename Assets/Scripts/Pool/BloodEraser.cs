using UnityEngine;

public class BloodEraser : MonoBehaviour
{
    public Renderer bloodPlaneRenderer;      // Le plane du sang
    public Texture2D eraseBrush;             // brosse floue
    public float eraseSizeUV = 0.05f;        // rayon du pinceau en UV

    private RenderTexture maskRT;
    private MaterialPropertyBlock block;

    void Start()
    {
        block = new MaterialPropertyBlock();
        bloodPlaneRenderer.GetPropertyBlock(block);
        maskRT = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGB32);
        maskRT.Create();
        Graphics.SetRenderTarget(maskRT);
        GL.Clear(true, true, Color.white);   // Sang visible partout au début
        block.SetTexture("_Mask", maskRT);
        bloodPlaneRenderer.SetPropertyBlock(block);
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint cp in collision.contacts)
        {
            PaintAtWorldPosition(cp.point);
        }
    }

    void PaintAtWorldPosition(Vector3 worldPos)
    {
        // Ray vers le plane
        Ray ray = new Ray(worldPos + Vector3.up * 0.05f, Vector3.down);
        if (!Physics.Raycast(ray, out RaycastHit hit, 1f))
            return;

        if (hit.collider.gameObject != bloodPlaneRenderer.gameObject)
            return;

        Vector2 uv = hit.textureCoord;

        // zone à dessiner
        int x = (int)((uv.x - eraseSizeUV) * maskRT.width);
        int y = (int)((uv.y - eraseSizeUV) * maskRT.height);
        int size = (int)(eraseSizeUV * 2f * maskRT.width);

        // Dessine la brosse dans le RT
        RenderTexture.active = maskRT;
        Graphics.DrawTexture(
            new Rect(x, y, size, size),
            eraseBrush
        );
        RenderTexture.active = null;
    }
}
