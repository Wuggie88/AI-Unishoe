using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinZone : MonoBehaviour
{

    public Text winningText;
    public GameObject[] enemies;

    private void Start()
    {
        winningText = GameObject.Find("WinningText").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8) {
            winningText.text = "VICTORY!!";

            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(GameObject Enemy in enemies) {
                Enemy.gameObject.SetActive(false);
            }
        }
    }   
}
