using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject aiPrefab, foodPrefab;
    public int aiCount, foodCount;
    public float foodCheckPeriod = 5f;
    public bool torus;

    private (float xRange, float yRange) mapData = (23f, 10f);
    private float camOrthSize, screenRatio, widthOrtho;
    private List<Transform> transformList;

    [Space]
    [Space]
    [Header("Genetic Algorithm")]
    public float crossOverRatio;
    public float mutationRatio;

    private GeneticAlgorithm geneticAlgorithm;

    
    void Awake()
    {
        TransformList = new List<Transform>();
        InstantiateSceneObjects();

        camOrthSize = Camera.main.orthographicSize;
        screenRatio = (float)Screen.width / (float)Screen.height;
        widthOrtho = camOrthSize * screenRatio;
        geneticAlgorithm = new GeneticAlgorithm(aiCount, mutationRatio, crossOverRatio, transformList[0].GetComponent<AIBrain>().NeuralNetwork.NeuralWeightLength);
    }

    private void Start()
    {
        StartCoroutine(CheckFoodCount());
    }
    
    /// <summary>
    /// 
    /// </summary>
    void InstantiateSceneObjects()
    {
        for (int aiIndex = 0; aiIndex < aiCount; aiIndex++)
        {            
            GameObject aiRef = InstantiateParameter(aiPrefab);
            TransformList.Add(aiRef.transform);
            aiRef.GetComponent<AIBrain>().GmRef = this;
        }

        for (int foodIndex = 0; foodIndex < foodCount; foodIndex++)
        {
            GameObject foodRef = InstantiateParameter(foodPrefab);
            TransformList.Add(foodRef.transform);
            foodRef.GetComponent<FoodScript>().GmRef = this;
        }
    }

    IEnumerator CheckFoodCount()
    {
        while(true)
        {
            if((TransformList.Count - aiCount) < foodCount)
            {
                for (int foodIndex = 0; foodIndex < foodCount - (TransformList.Count - aiCount); foodIndex++)
                {
                    GameObject foodRef = InstantiateParameter(foodPrefab);
                    TransformList.Add(foodRef.transform);
                    foodRef.GetComponent<FoodScript>().GmRef = this;
                }
            }


            yield return new WaitForSeconds(foodCheckPeriod);
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

    public List<Transform> TransformList { get => transformList; set => transformList = value; }
    public float CamOrthSize { get => camOrthSize; set => camOrthSize = value; }
    public float ScreenRatio { get => screenRatio; set => screenRatio = value; }
    public float WidthOrtho { get => widthOrtho; set => widthOrtho = value; }
}
