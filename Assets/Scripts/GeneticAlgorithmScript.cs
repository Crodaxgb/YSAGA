using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm
{
    List<(List<float> weightList, float genomeFitness)>  population;
    int populationSize;
    int chromoLength;
    int fittestGenome;
    int generCounter;
    int NumElite = 4;
    int NumOfCopiesElite = 2;
    float maxPerturbation = 0.3f;
    float totalFitness;
    float bestFitness;
    float worstFitness;
    float averageFitness;

    float mutationRate;
    float crossOverRate;


    public GeneticAlgorithm(int popSize, float mutationRate, float crossOverRate, int chromoLength)
    {
        this.populationSize = popSize;
        this.mutationRate = mutationRate;
        this.crossOverRate = crossOverRate;
        this.chromoLength = chromoLength;

        totalFitness = 0;
        generCounter = 0;
        fittestGenome = 0;
        bestFitness = 0;
        averageFitness = 0;
        worstFitness = Mathf.Infinity;

        population = new List<(List<float> weightList, float genomeFitness)>();
    }

    void Mutate(ref List<float> chromosome)
    {

    }
    //List<float> GetChromoRoulettte()
    //{        
        
    //}

    void CrossOver(ref List<float> mum, ref List<float> dad, ref List<float> baby1, ref List<float> baby2)
    {
        
    }


    //public List<(List<float> weightList, float genomeFitness)> Epoch(ref List<(List<float> weightList, float genomeFitness)> oldPopulation)
    //{


    //}

    //Elitisim için en iyi bireyin bulunmasý
    void GrabNBest(int nBest, int numberOfCopies, ref List<(List<float> weightList, float genomeFitness)> populationReference)
    {

    }

    void CalculateBestWorstAvTot()
    {
        

    }

    //void Reset()
    //{
    //    totalFitness = 0;
    //    bestFitness = 0;
    //    worstFitness = 9999999;
    //    averageFitness = 0;
    //}
  

}