using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text pointsText; 
    void Start()
    {
        pointsText = transform.GetComponent<Text>();
    }


    void Update()
    {
        pointsText.text = "Score: " + GameManager.gmInstance.totalPoints.ToString();
    }
}
