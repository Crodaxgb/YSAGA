using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AIBrain : MonoBehaviour
{
    GameManager gmRef;
    NeuralNetwork neuralNetwork;
    Rigidbody2D rigidBodyRef;
    void Awake()
    {
        neuralNetwork = new NeuralNetwork();
        neuralNetwork.InitializeNetwork(new int[] { 4, 6, 8, 2});
        rigidBodyRef = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var reaction = neuralNetwork.CalculateOutput(new List<float>() { 0.75f, 0.9f, 0.4f, 0.05f });
        rigidBodyRef.velocity = new Vector2(reaction.ElementAt(0), reaction.ElementAt(1));       

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
                    NeuralWeightLength++;
                }

            }

            weights.Add(layerWeights);

        }
    }

    public List<float> GetWeights()
    {
        List<float> flattenedWeight = new List<float>();

        for (int layerIndex = 0; layerIndex < neuralLayers.Length - 1; layerIndex++)
        {
          
            for (int rowIndex = 0; rowIndex < neuralLayers[layerIndex + 1]; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < neuralLayers[layerIndex] + 1; columnIndex++)
                {
                    flattenedWeight.Add(weights.ElementAt(layerIndex)[rowIndex, columnIndex]);
                }

            }

        }

        return flattenedWeight;

    }

    public void PutWeights(List<float> flattenedWeights)
    {
        int flattenIndex = 0;
        for (int layerIndex = 0; layerIndex < neuralLayers.Length - 1; layerIndex++)
        {

            for (int rowIndex = 0; rowIndex < neuralLayers[layerIndex + 1]; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < neuralLayers[layerIndex] + 1; columnIndex++)
                {
                    weights.ElementAt(layerIndex)[rowIndex, columnIndex] = flattenedWeights[flattenIndex++];                    
                }
            }
        }

    }

    public List<float> CalculateOutput(List<float> inputs)
    {
        List<float> outputs = new List<float>();
        for (int layerIndex = 0; layerIndex < neuralLayers.Length - 1; layerIndex++)
        {

            for (int rowIndex = 0; rowIndex < neuralLayers[layerIndex + 1]; rowIndex++)
            {
                float weightedSum = 0;

                for (int columnIndex = 0; columnIndex < neuralLayers[layerIndex]; columnIndex++)
                {
                    weightedSum += weights.ElementAt(layerIndex)[rowIndex, columnIndex] * inputs.ElementAt(columnIndex); 
                }
                //bias
                weightedSum -= weights.ElementAt(layerIndex)[rowIndex, neuralLayers[layerIndex]];
                outputs.Add(Sigmoid(weightedSum));
            }

            inputs = outputs;

        }

        return outputs;
    }

    float Sigmoid(float netInput, float response = 1.0f)
    {
        return (1.0f / (1.0f + (float)System.Math.Exp(-netInput / response)));
    }

    public int NeuralWeightLength { get => neuralWeightLength; set => neuralWeightLength = value; }
}
