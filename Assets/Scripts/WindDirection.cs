using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindDirection : MonoBehaviour
{
    public float spreadSpeed;
    public float pathMultiplier;
    int pathIndex = 0;

    public DNA dna;
    public bool hasFinished = false;
    bool hasBeenInitialized = false;

    public GameObject[] windOrientations;

    public Vector3 target;
    public Vector3 nextPoint;

    public TMPro.TMP_Dropdown windOutput;

    private void Start()
    {
        GetOutput();
    }

    public void GetOutput()
    {
        switch (windOutput.value)
        {
            case 0:
                target = windOrientations[0].transform.position;
                break;
            case 1:
                target = windOrientations[1].transform.position;
                break;
            case 2:
                target = windOrientations[2].transform.position;
                break;
            case 3:
                target = windOrientations[3].transform.position;
                break;
            default:
                break;
        }
    }

    public void InitFire(DNA newDna, Vector3 _target)
    {
        dna = newDna;
        target = _target;
        nextPoint = transform.position;
        hasBeenInitialized = true;
    }

    private void Update()
    {
        if (hasBeenInitialized && !hasFinished)
        {
            if(pathIndex == dna.genes.Count || Vector3.Distance(transform.position, target) < 0.5f)
            {
                hasFinished = true;
            }

            if((Vector3)transform.position == nextPoint)
            {
                nextPoint = (Vector3)transform.position + dna.genes[pathIndex];
                pathIndex++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPoint, spreadSpeed * Time.deltaTime);
            }
        }
    }

    public float fitness
    {
        get
        {
            float dist = Vector3.Distance(transform.position, target);

            if(dist == 0)
            {
                dist = 0.0001f;
            }

            return 60/dist;
        }
    }
}
