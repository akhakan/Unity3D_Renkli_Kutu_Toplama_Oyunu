using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;      // Kutu prefab referansı
    [SerializeField] private int numberOfBoxes = 20;    // Oluşturulacak kutu sayısı
    [SerializeField] private float spawnRange = 20f;    // Spawn alanı genişliği

    // Renk ve puan değerleri
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
            // Rastgele pozisyon hesapla
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0.5f,
                Random.Range(-spawnRange, spawnRange)
            );

            // Instantiate, Unity'de çalışma zamanında (runtime) yeni obje kopyaları oluşturmak için kullanılan temel bir metoddur.
            // Bir şablondan (Prefab veya mevcut GameObject) yeni bir kopya oluşturur ve sahneye ekler.
            // boxPrefab → Kopyalanacak şablon
            // randomPosition → Yeni objenin konumu(Vector3)
            // Quaternion.identity → Rotasyon(0, 0, 0 açıları = döndürme yok)
            // Açı verilmek istenirse : Quaternion.Euler(0, 45, 0), // 45° Y ekseninde dönük
            // transform: Yeni objelerin, bu script'in bağlı olduğu objenin child'ı olur.             
            // Özetle:
            // boxPrefab şablonundan yeni bir kutu kopyalar
            // randomPosition konumuna yerleştirir
            // Rotasyon vermez(düz durur)
            // Eklenen objeler BoxSpawner(parent) GameObject'in child'ı olur
            // box değişkenine referansı kaydeder
            // Sonraki satırlarda box.GetComponent<CollectibleBox>() ile erişim sağlar
            GameObject box = Instantiate(boxPrefab, randomPosition, Quaternion.identity,transform);

            

            // Rastgele renk ve puan değeri ata
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
