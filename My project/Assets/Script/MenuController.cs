using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Button btn;
    public void Start()
    {
        btn = GameObject.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(ButtonSelected);
    }

    void ButtonSelected()
    {
        Debug.Log("vous avez cliqué sur" + btn.name);
        SceneManager.LoadScene("jeu");
    }
}
