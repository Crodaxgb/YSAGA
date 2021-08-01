using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject aiPrefab, foodPrefab;
    public int aiCount, foodCount;

    private (float xRange, float yRange) mapData = (23f, 10f);

    private List<Transform> transformList;
    void Awake()
    {
        transformList = new List<Transform>();
        InstantiateSceneObjects();    
       
    }

    private void Start()
    {
        //StartCoroutine(CheckFoodCount());
    }

    /// <summary>
    /// 
    /// </summary>
    void InstantiateSceneObjects()
    {
        for (int aiIndex = 0; aiIndex < aiCount; aiIndex++)
        {            
            GameObject aiRef = InstantiateParameter(aiPrefab);
            transformList.Add(aiRef.transform);
            aiRef.GetComponent<AIBrain>().GmRef = this;
        }

        for (int foodIndex = 0; foodIndex < foodCount; foodIndex++)
        {
            GameObject foodRef = InstantiateParameter(foodPrefab);
            transformList.Add(foodRef.transform);
            foodRef.GetComponent<FoodScript>().GmRef = this;
        }
    }

    IEnumerator CheckFoodCount()
    {
        while(true)
        {

           
        }
    }

    GameObject InstantiateParameter(GameObject parameter)
    {
        GameObject tempObject = 
        Instantiate(parameter,
                    new Vector3
                    (Random.Range(-1 * mapData.xRange,mapData.xRange), 
                    Random.Range(-1 * mapData.yRange, mapData.yRange), 
                    0),
                    Quaternion.identity);
        tempObject.GetComponent<SpriteRenderer>().color 
            = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        return tempObject;
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

    //List<(List<float> weightList, float fitness)> GetPopulation()
    //{
       
    //}

    public void SpeedUpButton()
    {
        
    }

}
