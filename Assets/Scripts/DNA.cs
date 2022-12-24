using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public List<Vector3> genes = new List<Vector3>();

    public DNA(int genomeLenght)
    {
        for(int i = 0; i < genomeLenght; i++)
        {
            genes.Add(new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)));
        }
    }

    public DNA(DNA parent, DNA partner, float mutationRate)
    {
        for (int i = 0; i < parent.genes.Count; i++)
        {
            float mutationChance = Random.Range(0.0f, 1.0f);

            if(mutationChance <= mutationRate)
            {
                genes.Add(new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)));
            }
            else
            {
                int chance = Random.Range(0, 2);

                if(chance == 0)
                {
                    genes.Add(parent.genes[i]);
                }
                else
                {
                    genes.Add(partner.genes[i]);
                }         
            }
        }
    }  
}
