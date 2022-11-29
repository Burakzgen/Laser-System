using UnityEngine;

public class LaserColorChanger : MonoBehaviour
{
    [SerializeField] LineRenderer[] lineRenderers;
    [SerializeField] Material[] materials;

   public void ChangerColorOnClick(int value)
    {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            lineRenderers[i].material = materials[value];
        }
    }
}
