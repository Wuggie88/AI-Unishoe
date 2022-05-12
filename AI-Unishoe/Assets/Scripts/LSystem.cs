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
            switch (c)
            {
                case "A":
                    newLsystem.Add("A");
                    newLsystem.Add("B");

                    break;
                case "B":
                    newLsystem.Add("A");
                    newLsystem.Add("C");
                    break;
                case "C":
                    /*if(Random.Range(1, 6) <= 5) {
                        newLsystem.Add("B");
                    }
                    else
                    {
                        newLsystem.Add("D");
                    }*/
                    newLsystem.Add("D");
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
        yield return new WaitForEndOfFrame();

        //Goes through list and calls spawn-function
        for (int i = 0; i < Lsystem.Length; i++)
        {

            switch (Lsystem[i])
            {
                case "A":
                    SpawnObject(Prefabs[0]);

                    break;
                case "B":
                    SpawnObject(Prefabs[1]);

                    break;
                case "C":
                    SpawnObject(Prefabs[2]);

                    break;
                case "D":
                    SpawnObject(Prefabs[3]);
                    break;
            }
        }
    }

    void SpawnObject(GameObject obj)
    {
        //Instantiates objects and places them
        posX += 7;
        spawnPoint.position = new Vector3(posX, Random.Range(0, 3), 0);
        GameObject A = Instantiate(obj);
        A.transform.position = spawnPoint.position;
    }
}
