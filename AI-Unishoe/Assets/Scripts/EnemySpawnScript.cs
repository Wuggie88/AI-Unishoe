using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    } 

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(enemy, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
    }
}
