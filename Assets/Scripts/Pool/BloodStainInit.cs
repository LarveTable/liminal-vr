using UnityEngine;

public class BloodStainInit : MonoBehaviour
{
    [Tooltip("Résolution du masque. 512 est suffisant pour des petites taches, 1024 pour des grandes.")]
    public int textureResolution = 512;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend == null) return;

        // 1. On crée une nouvelle RenderTexture unique en mémoire pour CETTE tache
        RenderTexture specificRT = new RenderTexture(textureResolution, textureResolution, 0);
        specificRT.enableRandomWrite = true;
        specificRT.Create();

        // 2. On la remplit de BLANC (Visible) par défaut
        RenderTexture currentActive = RenderTexture.active; // On sauvegarde ce qu'unity regardait avant
        RenderTexture.active = specificRT;
        GL.Clear(false, true, Color.white); // On peint tout en blanc
        RenderTexture.active = currentActive; // On remet comme c'était

        // 3. On assigne cette nouvelle texture au Shader de cet objet précis
        // (L'utilisation de .material crée automatiquement une instance unique du matériau)
        rend.material.SetTexture("_Mask", specificRT);
    }
}