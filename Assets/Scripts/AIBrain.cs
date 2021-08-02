using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AIBrain : MonoBehaviour
{
    GameManager gmRef;   

    void Awake()
    {
        

    }

    // Update is called once per frame
    void Update()
    {        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
    }


    //List<float> CreateInput()
    //{
        
    //}


    public void RestoreComponents()
    {
        
    }

    void GetClosestEnemy(List<Transform> objects)
    {       
        
    }


    private void DontLeaveScene(bool torus)
    {
        
    }

    public bool IsEnemy()
    {
        return true;
    }

    public GameManager GmRef { get => gmRef; set => gmRef = value; }
}

public class NeuralNetwork
{
    private List<float[,]> weights = new List<float[,]>();
    int[] neuralLayers;
    int neuralWeightLength = 0;
    //new int[] {3, 6, 3, 1};
    public void InitializeNetwork(int[] layers)
    {
        neuralLayers = layers;
        for (int layerIndex = 0; layerIndex < layers.Length - 1; layerIndex++)
        {
            var layerWeights = new float[layers[layerIndex + 1] , layers[layerIndex] + 1];

            for (int rowIndex = 0; rowIndex < layers[layerIndex + 1]; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < layers[layerIndex] + 1; columnIndex++)
                {
                    layerWeights[rowIndex, columnIndex] = UnityEngine.Random.Range(0f, 1f);
                    neuralWeightLength++;
                }

            }

            weights.Add(layerWeights);

        }



    }

    //public List<float> GetWeights()
    //{

    //}

    public void PutWeights(List<float> flattenedWeights)
    {
        
    }

    //public List<float> CalculateOutput(List<float> inputs)
    //{
        

    //}

    float Sigmoid(float netInput, float response = 1.0f)
    {
        return (1.0f / (1.0f + (float)System.Math.Exp(-netInput / response)));
    }

   
}
