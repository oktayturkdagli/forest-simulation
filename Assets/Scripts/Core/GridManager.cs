using UnityEngine;

namespace Core
{
    public class GridManager : MonoBehaviour
    {
        [field: SerializeField] public int Width { get; set; } = 100;
        [field: SerializeField] public int Height { get; set; } = 100;
        [field: SerializeField] public GameObject GroundPrefab { get; set; }
        [field: SerializeField] public GameObject[] TreePrefabs { get; set; }
        [field: SerializeField] public GameObject[] RockPrefabs { get; set; }
        [field: SerializeField] public GameObject[] AnimalPrefabs { get; set; }
        [field: SerializeField] public float CellSize { get; set; } = 1f;
        
        private void Start()
        {
            GenerateGrid();
        }
        
        private void GenerateGrid()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var z = 0; z < Height; z++)
                {
                    var position = new Vector3(x * CellSize, 0, z * CellSize);
                    
                    // Place floor for each cell
                    var groundPrefab = Instantiate(GroundPrefab, position, Quaternion.identity);
                    groundPrefab.transform.parent = transform;
                    
                    // Place objects
                    var placedObject = PlaceRandomObject(TreePrefabs, RockPrefabs, position);
                    if (placedObject != null)
                        placedObject.transform.parent = transform;
                }
            }
        }
        
        private GameObject PlaceRandomObject(GameObject[] trees, GameObject[] rocks, Vector3 position)
        {
            GameObject placedObject = null;
            var randomValue = Random.value;
            
            if (randomValue <= 0.005) // Place animal with 0.5% chance
            {
                var animal = AnimalPrefabs[Random.Range(0, AnimalPrefabs.Length)];
                placedObject = Instantiate(animal, position, Quaternion.identity);
            }
            
            else if (randomValue > 0.05 && randomValue <= 0.15) // Place tree with 10% chance
            {
                var tree = trees[Random.Range(0, trees.Length)];
                placedObject = Instantiate(tree, position, Quaternion.identity);
            }
            
            else if (randomValue > 0.15 && randomValue <= 0.25) // Place the rock with 10% chance
            {
                var rock = rocks[Random.Range(0, rocks.Length)];
                placedObject = Instantiate(rock, position, Quaternion.identity);
            }
            
            return placedObject;
        }
    }
}