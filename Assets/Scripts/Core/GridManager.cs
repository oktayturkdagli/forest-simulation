using UnityEngine;

namespace Core
{
    public class GridManager : MonoBehaviour
    {
        public int width = 100;
        public int height = 100;
        public GameObject groundPrefab; // Zemin için prefab
        public GameObject[] treePrefabs; // Ağaçlar için prefablar (3 farklı ağaç)
        public GameObject[] rockPrefabs; // Kayalar için prefablar (3 farklı kaya)
        public float cellSize = 1f;

        private void Start()
        {
            GenerateGrid();
        }

        void GenerateGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    Vector3 position = new Vector3(x * cellSize, 0, z * cellSize);

                    // Her hücre için zemin yerleştirme
                    Instantiate(groundPrefab, position, Quaternion.identity);

                    // Rastgele nesne yerleştirme
                    if (Random.value > 0.8f) // %20 şans ile ağaç veya kaya yerleştir
                    {
                        if (Random.value > 0.5f) // %50 şans ile ağaç
                        {
                            GameObject tree = treePrefabs[Random.Range(0, treePrefabs.Length)];
                            Instantiate(tree, position, Quaternion.identity);
                        }
                        else // %50 şans ile kaya
                        {
                            GameObject rock = rockPrefabs[Random.Range(0, rockPrefabs.Length)];
                            Instantiate(rock, position, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

}