using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class solProc : MonoBehaviour
{
    public GameObject groundTilePrefab;
    public GameObject player; // R�f�rence au GameObject du joueur
    public float tileLength = 10f; // Longueur d'une tuile
    public int numTilesOnScreen = 45; // Nombre de tuiles devant le joueur
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnX = 0.0f; // Position de d�part pour la g�n�ration des tuiles
    public float roadWidth = 60f; // Largeur de la route
    public float houseWidth = 10f; // Largeur approximative d'une maison
    public GameObject house;
    public float maxDistanceFromRoad = 50f; // Distance maximale des maisons par rapport � la route
    public List<GameObject> housePrefabs = new List<GameObject>(); // Pr�fabriqu�s de maisons
    public List<GameObject> spawned = new List<GameObject>(); 
    public GameObject jsp; //pour le randomrange
    public List<GameObject> obstacte = new List<GameObject>(); // Pr�fabriqu�s d'obsacte
    public List<GameObject> poped = new List<GameObject>(); // pour gerer ce qu'il sera supprimer
    private float someThreshold = 50f;



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
        PlaceObstacle(go.transform.position.x);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        Destroy(spawned[0]);
        spawned.RemoveAt(0);
        Destroy(spawned[0]);
        spawned.RemoveAt(0);
        DeleteObsoleteObstacles();
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
            GameObject housePrefab = housePrefabs[Random.Range(0, housePrefabs.Count)];

            GameObject go2 = Instantiate(housePrefab, new Vector3(x, 0, sideZ), Quaternion.identity);
            spawned.Add(go2);
        }
    }
    int compteur = 0;
    private void PlaceObstacle(float roadX)
    {
        compteur++;
        if (compteur >= 10)
        {
            for (int i = 0; i < 5; i++) // Essayer de placer jusqu'� 5 obstacles
            {
                if (Random.Range(0f, 1f) < 0.2f) // 20% de chance de placer un obstacle
                {
                    float positionX = roadX + Random.Range(-tileLength / 2, tileLength / 2);
                    float positionZ = Random.Range(-roadWidth / 2, roadWidth / 2);
                    zone(positionX, positionZ);
                }
            }
        }
    }
    

    [SerializeField] // Permet de d�finir les pr�fabriqu�s dans l'interface Unity
    private List<GameObject> obstaclePrefabs; // Liste de pr�fabriqu�s d'obstacles
    private List<GameObject> activeObstacles = new List<GameObject>();


    private void zone(float positionX, float positionZ)
    {
        if (obstaclePrefabs.Count == 0)
        {
            Debug.LogWarning("Aucun pr�fabriqu� d'obstacle n'est d�fini.");
            return;
        }

        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        GameObject obstacleInstance = Instantiate(obstaclePrefab, new Vector3(positionX, 0, positionZ), Quaternion.identity);
        activeObstacles.Add(obstacleInstance);

        Debug.Log("Obstacle cr�� � la position: " + obstacleInstance.transform.position);
    }

    private void DeleteObsoleteObstacles()
    {
        while (activeObstacles.Count > 0 && player.transform.position.x - activeObstacles[0].transform.position.x > someThreshold)
        {
            Destroy(activeObstacles[0]);
            activeObstacles.RemoveAt(0);
        }
    }

}