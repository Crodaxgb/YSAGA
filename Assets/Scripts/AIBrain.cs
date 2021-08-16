using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AIBrain : MonoBehaviour, Killable
{
    public float speed = 5f;

    List<float> inputList;

    GameManager gmRef;
    NeuralNetwork neuralNetwork;
    Rigidbody2D rigidBodyRef;
    Transform closestObject = null;

    [SerializeField]
    private float deathPenalty = 10f;

    private float growthScale = 0.15f, individualScore;
    private bool isDead = false, torus;
    private float yMAx, xMax, boundaryRadius = 0.5f;
    
    void Awake()
    {
        inputList = new List<float>();
        NeuralNetwork = new NeuralNetwork();
        NeuralNetwork.InitializeNetwork(new int[] { 3, 6, 5});
        rigidBodyRef = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        yMAx = GmRef.CamOrthSize;
        xMax = GmRef.WidthOrtho;
        Torus = GmRef.torus;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            rigidBodyRef.velocity = Vector3.zero;
        }
        else
        {
            var networkResponse = NeuralNetwork.CalculateOutput(CreateInput());
            Vector2 movementVector = new Vector2();
            movementVector.x = networkResponse.ElementAt(0) - networkResponse.ElementAt(1);
            movementVector.y = networkResponse.ElementAt(2) - networkResponse.ElementAt(3);

            Debug.DrawLine(transform.position, closestObject.transform.position, Color.red);

            rigidBodyRef.velocity = movementVector * speed * networkResponse.ElementAt(4) / transform.localScale.x;
            DontLeaveScene(torus:Torus);

        }    

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if(transform.localScale.x > collision.transform.localScale.x * 1.1f && collision.GetComponent<Killable>() != null)
        {
            transform.localScale += collision.transform.localScale * growthScale;
            individualScore += collision.transform.localScale.x;         
            collision.GetComponent<Killable>().KillMessage();

        }

    }


    List<float> CreateInput()
    {
        inputList.Clear();
        GetClosestEnemy(GmRef.TransformList);

        Vector3 direction = closestObject.position - transform.position;
        direction.z = 0;
        direction.Normalize();

        inputList.Add(direction.x);
        inputList.Add(direction.y);    
        inputList.Add(closestObject.GetComponent<Killable>().IsEnemy() ? 1f : 0f );


        return inputList;
    }

    void GetClosestEnemy(List<Transform> objects)
    {
        float minDistance = Mathf.Infinity;

        foreach (Transform eachTransform in objects)
        {
            if(eachTransform.Equals(transform))
            {
                continue;
            }

            float distance = Vector2.Distance(eachTransform.transform.position, transform.position);

            if(distance < minDistance)
            {
                minDistance = distance;
                closestObject = eachTransform;
            }
            
        }
        
    }



    private void DontLeaveScene(bool torus)
    {
        var calculatedPosition = transform.position;

        if(torus)
        {
            if(transform.position.y + boundaryRadius > yMAx)
            {
                calculatedPosition.y = -yMAx + boundaryRadius * 1.5f;
            }
            if(transform.position.y - boundaryRadius < - yMAx)
            {
                calculatedPosition.y = yMAx - boundaryRadius * 1.5f;
            }
            if(transform.position.x + boundaryRadius > xMax)
            {
                calculatedPosition.x = -xMax + boundaryRadius * 1.5f;
            }
            if (transform.position.x - boundaryRadius < -xMax)
            {
                calculatedPosition.x = xMax - boundaryRadius * 1.5f;
            }
        }
        else
        {
            if (transform.position.y + boundaryRadius > yMAx)
            {
                calculatedPosition.y = yMAx - boundaryRadius;
            }
            if (transform.position.y - boundaryRadius < -yMAx)
            {
                calculatedPosition.y = -yMAx + boundaryRadius;
            }
            if (transform.position.x + boundaryRadius > xMax)
            {
                calculatedPosition.x = xMax - boundaryRadius;
            }
            if (transform.position.x - boundaryRadius < -xMax)
            {
                calculatedPosition.x = -xMax + boundaryRadius;
            }

        }

        transform.position = calculatedPosition;

    }

    public bool IsEnemy()
    {
        return true;
    }

    public void RestoreComponents()
    {
        IsDead = false;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        transform.localScale = Vector3.one;
    }

    public void KillMessage()
    {
        IsDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        transform.position = new Vector3(-25f, -25f, 0);
        
    }

    public GameManager GmRef { get => gmRef; set => gmRef = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public float IndividualScore { get => individualScore; set => individualScore = value; }
    public bool Torus { get => torus; set => torus = value; }
    public NeuralNetwork NeuralNetwork { get => neuralNetwork; set => neuralNetwork = value; }
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
