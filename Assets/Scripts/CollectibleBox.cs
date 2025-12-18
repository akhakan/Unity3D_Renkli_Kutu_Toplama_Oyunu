using UnityEngine;

public class CollectibleBox : MonoBehaviour
{
     private int scoreValue;
     private Color boxColor;

    public int ScoreValue { get; set; }
    public Color BoxColor { get; set; }

    void Start()
    {
        GetComponent<Renderer>().material.color = BoxColor;
    }
}
