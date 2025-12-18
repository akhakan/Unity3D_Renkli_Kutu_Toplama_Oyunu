using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int numberOfBoxes = 20;
    [SerializeField] private float spawnRange = 20f;

    // Renk ve puan deðerleri
    private Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };
    private int[] scoreValues = { 10, 20, 30, 50, 100 };

    void Start()
    {
        SpawnBoxes();
    }

    void SpawnBoxes()
    {
        for (int i = 0; i < numberOfBoxes; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0.5f,
                Random.Range(-spawnRange, spawnRange)
            );

            GameObject box = Instantiate(boxPrefab, randomPosition, Quaternion.identity);

            // Rastgele renk ve puan deðeri ata
            int randomIndex = Random.Range(0, colors.Length);
            CollectibleBox collectible = box.GetComponent<CollectibleBox>();

            if (collectible != null)
            {
                collectible.BoxColor = colors[randomIndex];
                collectible.ScoreValue = scoreValues[randomIndex];
            }
        }
    }
}
