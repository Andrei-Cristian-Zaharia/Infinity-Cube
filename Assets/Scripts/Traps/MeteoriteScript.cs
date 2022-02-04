using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteScript : MonoBehaviour
{
    public GameObject meteoriteBody;

    public Transform[] startingPoints;
    public Transform[] endingPoints;

    private GameObject meteoriteObj;
    private Transform destination;

    private void Start()
    {
        SpawnMeteorite();
    }

    private void Update()
    {
        if (meteoriteObj == null) { destination.gameObject.SetActive(false); SpawnMeteorite(); }
    }

    private void SpawnMeteorite()
    {
        Transform spawnPositon = startingPoints[Random.Range(0, startingPoints.Length)];
        destination = endingPoints[Random.Range(0, endingPoints.Length)];

        destination.gameObject.SetActive(true);

        GameObject met = Instantiate(meteoriteBody, spawnPositon.position, Quaternion.identity);
        met.GetComponent<Meteorite>().destination = destination;
        meteoriteObj = met;
    }
}
