using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaZilla
{
    /// <summary>
    ///     This component enables smoothly switching between OVRPassthrough Layers
    /// </summary>
    public class PassthroughLayerController : MonoBehaviour
    {
        public float layerTransitionSmoothing = 1f;
        private OVRPassthroughLayer activeLayer;
        private OVRPassthroughLayer[] passthroughLayers;
        private void Awake()
        {
            passthroughLayers = GameObject.FindObjectsByType<OVRPassthroughLayer>(FindObjectsSortMode.None);
        }
        public void SetActiveLayer(OVRPassthroughLayer layer)
        {
            activeLayer = layer;
        }
        public void SetActiveLayer(string layerName)
        {
            activeLayer = GetPassthroughLayer(layerName);
        }
        private OVRPassthroughLayer GetPassthroughLayer(string layerName)
        {
            OVRPassthroughLayer active = null;
            foreach (var layer in passthroughLayers)
            {
                if (layer.name.Contains(layerName))
                {
                    active = layer;
                }
            }
            return active;
        }

        //Fades out each layer to 0 opacity, and fades in active layer to full opacity
        private void FadeIn()
        {
            foreach (var layer in passthroughLayers)
            {
                if (layer == activeLayer)
                {
                    layer.textureOpacity = Mathf.Clamp01(layer.textureOpacity + (Time.deltaTime * layerTransitionSmoothing));
                }
                else
                {
                    layer.textureOpacity = Mathf.Clamp01(layer.textureOpacity - (Time.deltaTime * layerTransitionSmoothing));
                }
            }
        }
        private void Update()
        {
            if (activeLayer && activeLayer.textureOpacity < 1)
            {
                FadeIn();
            }
        }
    }
}

