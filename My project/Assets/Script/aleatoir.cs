using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aleatoir : MonoBehaviour
{
    public GameObject groundTilePrefab;
    public GameObject player; // R�f�rence au GameObject du joueur
    public float tileLength = 10f; // Longueur d'une tuile
    public int numTilesOnScreen = 15; // Nombre de tuiles devant le joueur
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnX = 0.0f; // Position de d�part pour la g�n�ration des tuiles
    public GameObject[] housePrefabs; // Pr�fabriqu�s de maisons
    public float roadWidth = 60f; // Largeur de la route
    public float houseWidth = 10f; // Largeur approximative d'une maison
    public float maxDistanceFromRoad = 50f; // Distance maximale des maisons par rapport � la route

    void Start()
    {
        Debug.Log("spawnX: " + spawnX);
        Debug.Log("Player X: " + player.transform.position.x);
        for (int i = 0; i < numTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }


    void Update()
    {
        Debug.Log("spawnX: " + spawnX);
        Debug.Log("Player X: " + player.transform.position.x);

        if (player.transform.position.x > spawnX - (numTilesOnScreen * tileLength) / 2)
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile()
    {
        GameObject go = Instantiate(groundTilePrefab, new Vector3(spawnX, 0, 0), Quaternion.identity);
        activeTiles.Add(go);
        spawnX += tileLength;
        PlaceHousesNearRoad(go.transform.position.x);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void PlaceHousesNearRoad(float roadX)
    {
        float leftSideZ = -roadWidth / 2 - houseWidth; // Position Z pour le c�t� gauche
        float rightSideZ = roadWidth / 2 + houseWidth; // Position Z pour le c�t� droit

        PlaceHousesOnSide(roadX, leftSideZ, -1); // Place les maisons du c�t� gauche
        PlaceHousesOnSide(roadX, rightSideZ, 1); // Place les maisons du c�t� droit
    }

    private void PlaceHousesOnSide(float roadX, float sideZ, int direction)
    {
        for (float x = roadX - tileLength / 2; x < roadX + tileLength / 2; x += houseWidth)
        {
            GameObject housePrefab = housePrefabs[Random.Range(0, housePrefabs.Length)];

            Instantiate(housePrefab, new Vector3(x, 0, sideZ), Quaternion.identity);
        }
    }
}
