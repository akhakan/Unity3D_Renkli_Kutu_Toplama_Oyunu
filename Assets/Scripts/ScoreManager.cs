using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // public → Herkes erişebilir
    // static → Sınıfa ait (objeye değil), bellekte tek bir kopya var
    // Her yerden ScoreManager.instance ile erişilebilir
    public static ScoreManager instance;    // Singleton instance

    [SerializeField] private TMP_Text scoreText;    // Skor text referansı

    private int totalScore = 0;                     // Toplam puan

    // Property kullanarak toplam skoru almak(get işlemi)
    public int TotalScore => totalScore;                 // Toplam puanı döndür


    void Awake()
    {
        // Singleton pattern implementasyonu
        if (instance == null)     // Henüz ScoreManager yoksa
        {
            instance = this;      // Bu objeyi instance yap
        }
        else                      // Zaten bir ScoreManager varsa
        {
            Destroy(gameObject);  // Bu kopya objeyi yok et!
        }
    }

    // Oyun başladığında 1 kez çalışır
    // UpdateScoreUI() çağırarak ekranda "Skor: 0" gösterir
    void Start()
    {
        UpdateScoreUI();
    }

    // Puan ekle
    // Başka Script'ten şu şekilde çağrılır : ScoreManager.instance.AddScore(scoreValue);
    public void AddScore(int points)
    {
        // 1. ADIM: Coroutine başlat
        StartCoroutine(AnimateScore(totalScore, totalScore + points));
        //                              ↓           ↓
        //                          başlangıç     hedef
        //                             100         150

        // 2. ADIM: Gerçek skoru hemen güncelle
        totalScore += points; // totalScore = 100 + 50 = 150
        UpdateScoreUI();
    }

    // UI'ı güncelle
    void UpdateScoreUI()
    {
        scoreText.text = "Skor: " + totalScore;
    }

    //                 Başlangıç skoru  Hedef skor
    //                             ↓         ↓
    IEnumerator AnimateScore(int start, int end)
    {
        Vector3 originalScale = scoreText.transform.localScale;

        float duration = 0.5f;          // Animasyon süresi (0.5 saniye)
        float elapsed = 0f;             // Geçen zaman
       

        while (elapsed < duration)      // 0.5 saniye dolana kadar döngü
        {
            // 1. Geçen zamanı güncelle
            elapsed += Time.deltaTime;  // Time.deltaTime : Bir önceki frame'den bu yana geçen süre (saniye cinsinden)
            // 2. Ara değeri hesapla
            int current = (int)Mathf.Lerp(start, end, elapsed / duration);
            // 3. Ekranı güncelle
            scoreText.text = "Skor: " + current;
            // 4. Bir sonraki frame'i bekle
            yield return null;
        }
        // Döngü bitti, son kontrol.
        // Sebebi matematiksel hesaplamalardan kaynaklı son değerin `end` değeri olduğundan emin olmak.
        scoreText.text = "Skor: " + end; // Kesin değer. Örnek: end->150 ise "Skor: 150" olur.
    }

}
