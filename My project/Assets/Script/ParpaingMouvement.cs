using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParpaingMouvement : MonoBehaviour
{
    public float speed = 12f;
    public float sAvent = 0.1f;
    public float jumpHeight = 5f;
    private Rigidbody rb;
    int mortcompt = 50;
    private Animator animator;
    public int score = 0;
    public int scoreT = 0;
    public Text scoreText;
    public Text vieText;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Assurez-vous d'avoir un composant Animator attaché
    }

    public void Mort()
    {
        mortcompt--;
        if (mortcompt<=0)
        {
            SceneManager.LoadScene("Menu");
    
        }
        vieText.text = "Vie pour petit obstacle: " + mortcompt.ToString();
    }
    public void MortDirect()
    {
        SceneManager.LoadScene("Menu");

    }
    void Update()
    {
        scoreT++;
        if(scoreT==50)
        {
            scoreT = 0;
            score++;
        }
        UpdateScore();
        float Horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed*10;
        transform.Translate(sAvent, 0, -Horizontal);
      

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    private bool isGrounded;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Lethal"))
        {
            Mort(); // Appelle la méthode Mort
        }
        else if (collision.collider.CompareTag("SuperLethal"))
        {
            MortDirect();
        }
        else if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void UpdateScore()
    {
        // Mettre à jour le texte d'affichage du score
        scoreText.text = "Score : " + score.ToString();
    }

}




