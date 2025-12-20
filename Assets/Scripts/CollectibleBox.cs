using UnityEngine;

public class CollectibleBox : MonoBehaviour
{
    public int ScoreValue { get; set; }
    public Color BoxColor { get; set; }

    void Start()
    {
        GetComponent<Renderer>().material.color = BoxColor;
    }
}
