using UnityEditor;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public GridManager GridManager { get; set; }
        [field: SerializeField] public ThirdPersonCharacterController ThirdPersonCharacterController { get; set; }
    }
    
    
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameManager gameManager = (GameManager)target;
            
            GUILayout.Space(10);
            if (GUILayout.Button("Create Randomize Forest"))
            {
                gameManager.GridManager.SetupGrid();
                gameManager.ThirdPersonCharacterController.SetupCharacterController();
            }
        }
    }
}