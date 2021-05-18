using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public float currScore = 0;

    [SerializeField] Text scoreAmount;
    
    // Start is called before the first frame update
    private void Start()
    {
        currScore = 0;
        
    }

    public void AddScore(float amount)
    {
        currScore += amount;
    }

    // Update is called once per frame
    void Update()
    {
        scoreAmount.text = currScore.ToString("0");
    }
}
