using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AIBrain : MonoBehaviour
{
    
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

}

public class NeuralNetwork
{
    public void InitializeNetwork(int[] layers)
    {
        

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
