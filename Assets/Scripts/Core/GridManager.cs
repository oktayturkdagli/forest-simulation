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

                    // Floor placement for each cell
                    Instantiate(GroundPrefab, position, Quaternion.identity);

                    // Random object placement
                    if (Random.value > 0.8f) // Place tree or rock with 20% chance
                    {
                        if (Random.value > 0.5f) // Tree with 50% chance
                        {
                            var tree = TreePrefabs[Random.Range(0, TreePrefabs.Length)];
                            Instantiate(tree, position, Quaternion.identity);
                        }
                        else // Rock with 50% chance
                        {
                            var rock = RockPrefabs[Random.Range(0, RockPrefabs.Length)];
                            Instantiate(rock, position, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

}