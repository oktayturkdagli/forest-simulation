using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        public void SetupGrid()
        {
            ClearGrid();
            ClearNavMesh();
            GenerateGrid();
            GenerateStaticObjects();
            BuildNavMesh();
            GenerateDynamicObjects();
        }
        
        private void ClearGrid()
        {
            // Destroy child objects one by one
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i).gameObject;
                if (Application.isEditor)
                    DestroyImmediate(child);
                else
                    Destroy(child);
            }
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
                }
            }
        }
        
        private void GenerateStaticObjects()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var z = 0; z < Height; z++)
                {
                    var position = new Vector3(x * CellSize, 0, z * CellSize);
                    
                    // Place rocks and trees
                    PlaceRocksAndTreesObject(position);
                }
            }
        }
        
        private void GenerateDynamicObjects()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var z = 0; z < Height; z++)
                {
                    var position = new Vector3(x * CellSize, 0, z * CellSize);
                    
                    // Place animals
                    PlaceAnimalObject(position);
                }
            }
        }
        
        private void PlaceRocksAndTreesObject(Vector3 position)
        {
            var randomValue = Random.value;
            
            GameObject placedObject = null;
            if (randomValue > 0.05 && randomValue <= 0.12) // Place tree with 11.5% chance
            {
                var tree = TreePrefabs[Random.Range(0, TreePrefabs.Length)];
                placedObject = Instantiate(tree, position, Quaternion.identity);
                placedObject.transform.parent = transform;
            }
            
            else if (randomValue > 0.12 && randomValue <= 0.17) // Place the rock with 5% chance
            {
                var rock = RockPrefabs[Random.Range(0, RockPrefabs.Length)];
                placedObject = Instantiate(rock, position, Quaternion.identity);
                placedObject.transform.parent = transform;
            }
        }
        
        private void PlaceAnimalObject(Vector3 position)
        {
            var randomValue = Random.value;
            
            if (randomValue <= 0.005) // Place animal with 0.5% chance
            {
                var animal = AnimalPrefabs[Random.Range(0, AnimalPrefabs.Length)];
                var placedObject = Instantiate(animal, position, Quaternion.identity);
                placedObject.transform.parent = transform;
            }
        }
        
        private void BuildNavMesh()
        {
            var navMeshSurface = GetComponent<NavMeshSurface>();
            if (navMeshSurface)
                navMeshSurface.BuildNavMesh();
        }
        
        private void ClearNavMesh()
        {
            var navMeshSurface = GetComponent<NavMeshSurface>();
            if (navMeshSurface)
                navMeshSurface.RemoveData();
        }
    }
}