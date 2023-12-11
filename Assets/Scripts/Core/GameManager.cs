using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public GridManager GridManager { get; set; }
        [field: SerializeField] public OcclusionCullingManager OcclusionCullingManager { get; set; }
        
        private void Awake()
        {
            GridManager.SetupGrid();
            OcclusionCullingManager.SetupOcclusionCulling();
        }
    }
}