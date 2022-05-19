using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LSystem : MonoBehaviour
{
    public string[] Lsystem; //The L-system for generation
    string axiom = "A"; //THe strating point
    public List<string> newLsystem; //A reference list for adding new grammars to L-system

    public bool hasGenerated = false; //Checks for end of grammar
    public GameObject[] Prefabs; //Platforms that can be generated
    public Transform spawnPoint; //Spawnpoint for the spawn
    int posX; //Used to incremently increase the x-position of the spawnpoint
    public GameObject aStar;

    // Start is called before the first frame update
    void Start()
    {
        Lsystem[0] = axiom;
        StartCoroutine(Grammar());
    }

    IEnumerator Grammar()
    {
        // Clears the alternative list, which gets added to in each iteration
        newLsystem.Clear();

        yield return new WaitForEndOfFrame();

        //Goes through the Grammar list and adds for next iteration
        foreach (string c in Lsystem)
        {
            int rndm = Random.Range(1, 6);
            switch (c)
            {
                case "A":
                    newLsystem.Add("A");
                    newLsystem.Add("B");

                    break;
                case "B":
                    
                    if(rndm <= 3)
                    {
                        newLsystem.Add("A");
                        newLsystem.Add("E");
                    }
                    else
                    {
                        newLsystem.Add("A");
                        newLsystem.Add("C");
                    }
                    break;
                case "C":
                    if (rndm >= 3)
                    {
                        newLsystem.Add("B");
                    }
                    else
                    {
                        newLsystem.Add("D");
                        Lsystem = newLsystem.ToArray();
                        hasGenerated = true;
                        StartCoroutine(Generate());
                    }
                    //newLsystem.Add("D");
                    break;
                default:
                    //Is done with the L-system and begins to initiating the map
                    Lsystem = newLsystem.ToArray();
                    hasGenerated = true;
                    StartCoroutine(Generate());
                    break;
            }
        }
        if (hasGenerated == false)
        {
            //Begins next iteration, if not done with L-system
            Lsystem = newLsystem.ToArray();
            StartCoroutine(Grammar());
        }
    }

    IEnumerator Generate()
    {

        //Goes through list and calls spawn-function
        for (int i = 0; i < Lsystem.Length; i++)
        {

            switch (Lsystem[i])
            {
                case "A":
                    //Platform 1
                    SpawnObject(Prefabs[0], 0);

                    break;
                case "B":
                    //Platform 2
                    SpawnObject(Prefabs[1], 1);

                    break;
                case "C":
                    //Platform 3
                    SpawnObject(Prefabs[2], 2);

                    break;
                case "D":
                    //End Platform
                    SpawnObject(Prefabs[3], 3);
                    break;
                case "E":
                    //Enemy Spawnpoint
                    SpawnObject(Prefabs[4], 2);
                    break;
            }
        }
        aStar.SetActive(true);
        yield return new WaitForEndOfFrame();
    }

    void SpawnObject(GameObject obj, int posY)
    {
        //Instantiates objects and places them
        posX += 10;
        spawnPoint.position = new Vector3(posX, posY, 0);
        GameObject spawnObj = Instantiate(obj);
        spawnObj.transform.position = spawnPoint.position;
    }
}
