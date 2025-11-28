using UnityEngine;

public class BloodEraser : MonoBehaviour
{
    public Shader drawShader;
    public Texture2D brushTexture;
    
    [Header("Forme de la Serpillière")]
    [Range(1, 500)] public float brushWidth = 120f; // Plus large (le côté long de la tête)
    [Range(1, 500)] public float brushHeight = 30f; // Plus fin (l'épaisseur de la tête)
    
    [Header("Réglages Techniques")]
    public LayerMask bloodLayer;
    public float rayDistance = 1.0f;
    
    // Si la trace se dessine à 90° de la réalité, change cette valeur (ex: 90 ou -90)
    public float rotationOffset = 0f; 

    private Material drawMaterial;
    private RaycastHit hit;

    void Start()
    {
        if (drawShader != null)
        {
            drawMaterial = new Material(drawShader);
            drawMaterial.SetTexture("_MainTex", brushTexture);
        }
    }

    void Update()
    {
        Debug.DrawRay(transform.position, -transform.up * rayDistance, Color.red);

        if (Physics.Raycast(transform.position, -transform.up, out hit, rayDistance, bloodLayer)) 
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend == null || rend.sharedMaterial == null) return;
            if (!rend.material.HasProperty("_Mask")) return;

            Texture maskTexture = rend.material.GetTexture("_Mask");
            
            if (maskTexture is RenderTexture rt)
            {
                // On récupère la rotation Y du balai (son orientation horizontale)
                // Le "-" sert souvent à corriger le sens de rotation en 2D vs 3D
                float mopAngle = transform.eulerAngles.y; 

                DrawOnTexture(rt, hit.textureCoord, mopAngle);
            }
        }
    }

    void DrawOnTexture(RenderTexture rt, Vector2 uv, float angle)
    {
        RenderTexture.active = rt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, rt.width, rt.height, 0);

        // Conversion UV (0 à 1) vers Pixels
        float x = uv.x * rt.width;
        float y = (1 - uv.y) * rt.height; 

        // --- GESTION DE LA ROTATION (CORRIGÉE) ---
        
        // 1. On prépare la position et la rotation
        Vector3 position = new Vector3(x, y, 0);
        Quaternion rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
        Vector3 scale = Vector3.one;

        // 2. On crée une matrice qui combine tout ça
        Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);

        // 3. On applique la matrice au moteur de rendu GL
        GL.MultMatrix(matrix);

        // -----------------------------------------

        // IMPORTANT : Comme on a DÉJÀ déplacé le "curseur" à la position (x,y) via la matrice,
        // on dessine maintenant le rectangle en (0,0) relatif !
        // On centre juste le rectangle en faisant -width/2 et -height/2.
        
        Graphics.DrawTexture(
            new Rect(-brushWidth / 2, -brushHeight / 2, brushWidth, brushHeight), 
            brushTexture, 
            drawMaterial
        );

        GL.PopMatrix();
        RenderTexture.active = null;
    }
}