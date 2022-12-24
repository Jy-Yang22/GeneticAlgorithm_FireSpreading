using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulationController : MonoBehaviour
{
    private GridManager grid;

    List<WindDirection> population = new List<WindDirection>();

    public TMPro.TMP_InputField populationInput, genomeInput, mutationInput;

    public GameObject firePrefab;

    public int populationSize;
    public int genomeLenght;
    public float mutationRate;
    public float cutOff;

    public GameObject spawner;
    public Transform spawnPoint;
    public Transform end;
    
    void InitPopulation()
    {
        for(int i = 0; i < populationSize; i++)
        {
            GameObject go = Instantiate(firePrefab, spawnPoint.position, Quaternion.identity);
            go.GetComponent<WindDirection>().InitFire(new DNA(genomeLenght), end.position);
            population.Add(go.GetComponent<WindDirection>());
        }
    }

    void NextGeneration()
    {
        int survivorCut = Mathf.RoundToInt(populationSize * cutOff);
        List<WindDirection> survivors = new List<WindDirection>();
        
        for(int i = 0; i < survivorCut; i++)
        {
            survivors.Add(GetFittest());
        }

        for(int i = 0; i < population.Count; i++)
        {
            Destroy(population[i].gameObject);
        }

        population.Clear();

        while(population.Count < populationSize)
        {
            for(int i = 0; i < survivors.Count; i++)
            {
                GameObject go = Instantiate(firePrefab, spawnPoint.position, Quaternion.identity);
                go.GetComponent<WindDirection>().InitFire(new DNA(survivors[i].dna, survivors[Random.Range(0, 10)].dna, mutationRate), end.position);
                population.Add(go.GetComponent<WindDirection>());
                
                if(population.Count >= populationSize)
                {
                    break;
                }
            }
        }

        for(int i = 0; i < survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);
        }
    }

    private void Start()
    {
        //InitPopulation();
    }

    public void SetPopulation(string value) 
    {
        populationSize = int.Parse(value); 
    }

    public void SetGenome(string value)
    {
        genomeLenght = int.Parse(value);
    }

    public void SetMutation(string value)
    {
        mutationRate = float.Parse(value);
    }

    private void Update()
    {
        if (spawner.activeSelf)
        {
            InitPopulation();
            spawner.SetActive(false);
        }

        if (!HasActive())
        {
            NextGeneration();
        }
    }

    WindDirection GetFittest()
    {
        float maxFitness = float.MinValue;
        int index = 0;

        for(int i = 0; i < population.Count; i++)
        {
            if(population[i].fitness > maxFitness)
            {
                maxFitness = population[i].fitness;
                index = i;
            }
        }

        WindDirection fittest = population[index];
        population.Remove(fittest);

        return fittest;
    }

    bool HasActive()
    {
        for(int i = 0; i < population.Count; i++)
        {
            if (!population[i].hasFinished)
            {
                return true;
            }
        }

        return false;
    }
}
