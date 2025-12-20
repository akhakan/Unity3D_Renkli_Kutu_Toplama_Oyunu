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
        StartCoroutine(AnimateScore(totalScore, totalScore + points));
        totalScore += points;
        UpdateScoreUI();
    }

    // UI'ı güncelle
    void UpdateScoreUI()
    {
        scoreText.text = "Skor: " + totalScore;
    }

    IEnumerator AnimateScore(int start, int end)
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            int current = (int)Mathf.Lerp(start, end, elapsed / duration);
            scoreText.text = "Skor: " + current;
            yield return null;
        }

        scoreText.text = "Skor: " + end;
    }

}
