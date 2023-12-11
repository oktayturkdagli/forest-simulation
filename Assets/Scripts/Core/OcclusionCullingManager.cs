using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class OcclusionCullingManager : MonoBehaviour
    {
        [field: SerializeField] public float CheckInterval { get; set; } = 0.01f; // Control range
        
        private Camera _mainCamera;
        private List<Renderer> _staticRenderers; // List of static renderers
        
        public void SetupOcclusionCulling()
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;
            
            FindStaticRenderers();
            StartCoroutine(CheckVisibilityRoutine());
        }
        
        private void FindStaticRenderers()
        {
            _staticRenderers = new List<Renderer>();
            Renderer[] allRenderers = FindObjectsOfType<Renderer>();
            foreach (var renderer in allRenderers)
            {
                if (renderer.gameObject.isStatic)
                {
                    _staticRenderers.Add(renderer);
                }
            }
        }
        
        private IEnumerator CheckVisibilityRoutine()
        {
            while (true)
            {
                CheckVisibility();
                yield return new WaitForSeconds(CheckInterval);
            }
        }
        
        private void CheckVisibility()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);
            foreach (var rendererReference in _staticRenderers)
            {
                if (GeometryUtility.TestPlanesAABB(planes, rendererReference.bounds))
                    rendererReference.enabled = true; // Activate if the renderer is in the field of view
                else
                    rendererReference.enabled = false; // If not, disable
            }
        }
    }
}