using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    void Awake()
    {
        InstantiateSceneObjects();       
       
    }

    private void Start()
    {
        StartCoroutine(CheckFoodCount());
    }

    void InstantiateSceneObjects()
    {
    }

    IEnumerator CheckFoodCount()
    {
        while(true)
        {

           
        }
    }

    GameObject InstantiateParameter(GameObject parameter)
    {
        
    }

    void RefreshLevel()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void RefreshText()
    {
        
    }

    void DeployGeneticAlgorithm()
    {
        
    }

    List<(List<float> weightList, float fitness)> GetPopulation()
    {
       
    }

    public void SpeedUpButton()
    {
        
    }

}
