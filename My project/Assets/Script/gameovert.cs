using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("GameOverManager instance set");
        }
        else if (instance != this)
        {
            Debug.LogError("Multiple instances of GameOverManager detected!");
            Destroy(gameObject);
        }
    }


    public void OnPlayerDeath()
    {
        // Mettez ici la logique à exécuter lorsque le joueur meurt
        Debug.Log("Le joueur est mort");
    }
}
