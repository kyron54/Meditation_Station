using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gene
{
    public string chromo1, chromo2;

    public Gene(string s)
    {
        chromo1 = s.Substring(0, 1);
        chromo2 = s.Substring(1, 1);
    }

    public string toString()
    {
        return chromo1 + chromo2;
    }

    public void CycleGeneForward()
    {
        if(chromo1 == chromo1.ToUpper() && chromo2 == chromo2.ToUpper())
        {
            chromo1 = chromo1.ToLower();
            chromo2 = chromo2.ToLower();
        }
        else if (chromo1 == chromo1.ToUpper() && chromo2 == chromo2.ToLower())
        {
            chromo1 = chromo1.ToUpper();
            chromo2 = chromo2.ToUpper();
        }
        else
        {
            chromo1 = chromo1.ToUpper();
            chromo2 = chromo2.ToLower();
        }
    }

    public void CycleGeneBackward()
    {
        if (chromo1 == chromo1.ToLower() && chromo2 == chromo2.ToLower())
        {
            chromo1 = chromo1.ToUpper();
            chromo2 = chromo2.ToUpper();
        }
        else if (chromo1 == chromo1.ToUpper() && chromo2 == chromo2.ToLower())
        {
            chromo1 = chromo1.ToLower();
            chromo2 = chromo2.ToLower();
        }
        else
        {
            chromo1 = chromo1.ToUpper();
            chromo2 = chromo2.ToLower();
        }
    }
}

public class Genetics
{
    public static Gene CrossBreed(Gene gene1, Gene gene2)
    {
        Gene newGene = new Gene("ss");

        if (UnityEngine.Random.Range(0, 2) == 0)
            newGene.chromo1 = gene1.chromo1;
        else
            newGene.chromo1 = gene2.chromo1;

        if (UnityEngine.Random.Range(0, 2) == 0)
            newGene.chromo2 = gene1.chromo2;
        else
            newGene.chromo2 = gene2.chromo2;

        return newGene;
    }
}
