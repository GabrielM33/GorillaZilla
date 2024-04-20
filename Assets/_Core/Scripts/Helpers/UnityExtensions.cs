using UnityEngine;
using UnityEngine.Events;

public static class UnityExtensions
{

    /// <summary>
    /// Extension method to check if a layer is in a layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    public static void SetLayerRecursive(this GameObject gameObject, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        gameObject.layer = layer;
        foreach(Transform t in gameObject.transform)
        {
            t.gameObject.layer = layer;
        }
    }
    public static void DestroyChildren(this GameObject gameObject){
        foreach (Transform child in gameObject.transform)
        {
            if(child.gameObject != gameObject){
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}