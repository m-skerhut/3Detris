using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance { get; private set; }

    //the time after a block drops by 1
    public float dropTime = 1.0f;
    public float quickDropTime = 0.1f;

    public int totalPoints;
    int clearRowPoints = 10;

    void Awake()
    {
        if (gmInstance == null)
            gmInstance = this; 
        else if (gmInstance != this) 
            Destroy(this); 

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {

    }


    void Update()
    {
        
    }

    public void AddPoints(int multiplicator)
    {
        totalPoints = totalPoints + clearRowPoints * multiplicator;

    }
}
