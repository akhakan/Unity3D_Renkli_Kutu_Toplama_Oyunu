# 🎮 Unity3D_Renkli_Kutu_Toplama_Oyunu

## 🎯 Oyun Hakkında

Bu proje, Unity 3D kullanılarak geliştirilmiş basit ve eğlenceli bir kutu toplama oyunudur. Oyuncular karakterlerini hareket ettirerek sahada rastgele dağılmış renkli kutuları toplar ve puan kazanırlar. Her renk farklı puan değerine sahiptir.

## 🖼️ Oyun Görselleri

<p align="left">
  <img src="ReadMeImages/kt1.jpg" alt="Resim1"  width:100px;"/>
  <img src="ReadMeImages/kt2.jpg" alt="Resim2"  width:100px;"/>
  <img src="ReadMeImages/kt3.jpg" alt="Resim3"  width:100px;"/>
</p>

## ✨ Özellikler

- **3D Karakter Kontrolü**: WASD veya ok tuşları ile akıcı hareket
- **Dinamik Skor Sistemi**: Gerçek zamanlı puan takibi
- **Renkli Kutu Sistemi**: 5 farklı renk, 5 farklı puan değeri
- **Otomatik Kutu Oluşturma**: Oyun başlangıcında 20 adet rastgele kutu
- **Cinemachine Kamera**: Smooth third-person kamera takibi
- **TextMeshPro UI**: Modern ve şık kullanıcı arayüzü

### Puan Sistemi
| Renk | Puan Değeri |
|------|-------------|
| 🔴 Kırmızı | 10 puan |
| 🔵 Mavi | 20 puan |
| 🟢 Yeşil | 30 puan |
| 🟡 Sarı | 50 puan |
| 🟣 Mor | 100 puan |

### Oyun Akışı
1. Oyun başladığında sahada 20 adet rastgele renkli kutu oluşur
2. Karakterinizi kutulara doğru hareket ettirin
3. Kutulara temas ettiğinizde kutular kaybolur ve puan kazanırsınız
4. Sol üst köşedeki skor tabelasından puanınızı takip edin

## 🛠️ Kurulum

### Gereksinimler
- Unity 6.1 veya üzeri
- TextMeshPro paketi
- Cinemachine paketi (kamera kontrolü için)

### Adım Adım Kurulum

#### 1. Yeni Proje Oluşturma
```
Unity Hub → New Project → 3D Template → Create
```

#### 2. Oyuncu (Player) Kurulumu
- **Hierarchy** → Sağ tık → **3D Object** → **Capsule**
- İsim: `Player`
- Tag: `Player`
- Transform Position: `(0, 1, 0)`
- **Add Component** → **Rigidbody**
  - Constraints: Freeze Rotation **X, Y, Z** ✓
- `PlayerController.cs` scriptini ekle
- Cinemachine Third Person Aim Camera ayarları yap

#### 3. Toplanabilir Kutu Prefab
- **Hierarchy** → **3D Object** → **Cube**
- İsim: `CollectibleBox`
- Tag: `Collectible` (yeni tag oluştur)
- Scale: `(1, 1, 1)`
- **Add Component** → **Box Collider**
  - Is Trigger: ✓
- `CollectibleBox.cs` scriptini ekle
- Project penceresine sürükleyerek **Prefab** oluştur
- Hierarchy'den orijinali sil

#### 4. Zemin Oluşturma
- **3D Object** → **Plane**
- Scale: `(5, 1, 5)`
- Position: `(0, 0, 0)`

#### 5. UI (Skor Sistemi)
- **Hierarchy** → **UI** → **Canvas**
  - Canvas Scaler ayarları:
    - UI Scale Mode: **Scale With Screen Size**
    - Reference Resolution: **1920x1080**
    - Match: **0.5**
- Canvas altına **UI** → **Text - TextMeshPro**
  - İsim: `ScoreText`
  - Text: `"Skor: 0"`
  - Font Size: **60**
  - Font Style: **Bold**
  - Color (HEX): **#8B008B** (Mor)
  - Anchor Preset: **Top Left**
  - PosX: **20**, PosY: **-15**
  - Width: **350**, Height: **60**
  - Auto Size: ✓
  - Font Asset: `Lost Tumbler SDF`

#### 6. Game Manager Kurulumu
- **Hierarchy** → Boş GameObject oluştur
- İsim: `GameManager`
- `ScoreManager.cs` scriptini ekle
- Inspector'da **Score Text** alanına `ScoreText` objesini sürükle

#### 7. Box Spawner Kurulumu
- **Hierarchy** → Boş GameObject oluştur
- İsim: `BoxSpawner`
- `BoxSpawner.cs` scriptini ekle
- Inspector'da **Box Prefab** alanına `CollectibleBox` prefab'ını sürükle

## 📁 Proje Yapısı
```
Assets/
├── Scripts/
│   ├── PlayerController.cs      # Oyuncu hareket kontrolü
│   ├── CollectibleBox.cs        # Kutu davranışları ve renk sistemi
│   ├── ScoreManager.cs          # Skor yönetimi ve UI güncellemesi
│   └── BoxSpawner.cs            # Kutu oluşturma sistemi
├── Prefabs/
│   └── CollectibleBox.prefab    # Toplanabilir kutu prefab'ı
└── Scenes/
    └── SampleScene.unity          # Ana oyun sahnesi
```

## 💻 Kod Yapısı ve Açıklamalar

### 📦 CollectibleBox.cs

- Trigger collision detection
- Random renk atama sistemi
- Renk bazlı puan değerleri
- ScoreManager ile entegrasyon

🎯 **Amaç:** Her kutunun kendine özgü renk ve puan değerini saklar. Kutu oluşturulduğunda atanan renge göre materyalini değiştirir.

🧩 **Kod:**
```csharp
using UnityEngine;

public class CollectibleBox : MonoBehaviour
{
    private int scoreValue;      
    private Color boxColor;

    public int ScoreValue { get; set; }      // Kutunun puan değeri
    public Color BoxColor { get; set; }      // Kutunun rengi
    
    void Start()
    {
        // Kutunun rengini ata
        GetComponent().material.color = BoxColor;
    }
}
```

**📌 Önemli Noktalar:**
- `ScoreValue`: Kutu toplandığında kazanılacak puan miktarı
- `BoxColor`: Kutunun görsel rengi
- `Start()` metodunda Renderer komponenti üzerinden malzeme rengini ayarlar

---

### 🎲 BoxSpawner.cs

- Start metodunda otomatik spawn
- Configurable spawn alanı (-20 ile 20 arası)
- Prefab instantiation

🎯 **Amaç:** Oyun başladığında belirlenen sayıda rastgele konumlarda, rastgele renk ve puan değerlerine sahip kutular oluşturur.

🧩 **Kod:**
```csharp
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;      // Kutu prefab referansı
    [SerializeField] private int numberOfBoxes = 20;    // Oluşturulacak kutu sayısı
    [SerializeField] private float spawnRange = 20f;    // Spawn alanı genişliği
    
    // Renk ve puan değerleri dizileri
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
            
            // Kutuyu oluştur
            GameObject box = Instantiate(boxPrefab, randomPosition, Quaternion.identity);
            
            // Rastgele renk ve puan değeri ata
            int randomIndex = Random.Range(0, colors.Length);
            CollectibleBox collectible = box.GetComponent();
            
            if (collectible != null)
            {
                collectible.BoxColor = colors[randomIndex];
                collectible.ScoreValue = scoreValues[randomIndex];
            }
        }
    }
}
```

**📌 Önemli Noktalar:**
- `numberOfBoxes`: Inspector'dan ayarlanabilir kutu sayısı (varsayılan: 20)
- `spawnRange`: Kutların oluşturulacağı alan büyüklüğü (-20 ile +20 arası)
- `colors` ve `scoreValues` dizileri paralel çalışır (aynı index aynı renk-puan eşleşmesi)
- Her kutu Y ekseninde 0.5 yükseklikte oluşturulur (zeminin hemen üstü)

---

### 📊 ScoreManager.cs

- Singleton pattern implementasyonu
- TextMeshPro ile UI güncellemesi
- Global skor yönetimi

🎯 **Amaç:** Singleton pattern kullanarak tüm oyun boyunca puan sistemini yönetir. Toplam puanı takip eder ve UI'ı günceller.

🧩 **Kod:**
```csharp
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;    // Singleton instance
    
    [SerializeField] private TMP_Text scoreText;    // Skor text referansı
    private int totalScore = 0;                     // Toplam puan
    
    void Awake()
    {
        // Singleton pattern implementasyonu
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        UpdateScoreUI();
    }
    
    // Puan ekle
    public void AddScore(int points)
    {
        totalScore += points;
        UpdateScoreUI();
    }
    
    // UI'ı güncelle
    void UpdateScoreUI()
    {
        scoreText.text = "Skor: " + totalScore;
    }
    
    // Toplam puanı döndür
    public int GetScore()
    {
        return totalScore;
    }
}
```

**📌 Önemli Noktalar:**
- **Singleton Pattern**: Oyunda tek bir ScoreManager instance'ı olmasını garanti eder
- `AddScore()`: Dışarıdan çağrılarak puan eklemek için kullanılır
- `UpdateScoreUI()`: Her puan değişiminde TextMeshPro text'ini günceller
- `GetScore()`: Diğer scriptlerin mevcut skoru okuması için kullanılır

---

### 🎮 PlayerController.cs

- Rigidbody tabanlı fizik kontrolü
- Input.GetAxis ile smooth hareket
- Configurable hareket hızı

🎯 **Amaç:** Oyuncu karakterinin hareket kontrolünü sağlar. Rigidbody fizik sistemi kullanarak WASD/Ok tuşları ile hareket imkanı verir. Ayrıca kutularla temas algılaması yapar.

🧩 **Kod:**
```csharp
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _orientationTransform;    // Kamera yönlendirme referansı
    
    private Rigidbody _playerRigidbody;     // Rigidbody referansı
    
    private float _verticalInput, _horizontalInput;     // Input değerleri
    
    private Vector3 _movementDirection;     // Hareket yönü
    
    [SerializeField] private float _movementSpeed = 10f;    // Hareket hızı
    
    private void Awake()
    {
        _playerRigidbody = GetComponent();
        _playerRigidbody.freezeRotation = true;     // Fizik kaynaklı dönmeyi engelle
    }
    
    private void Update()
    {
        // Input değerlerini al
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        
        // Hareket yönünü hesapla (kamera yönüne göre)
        _movementDirection = _orientationTransform.forward * _verticalInput + 
                           _orientationTransform.right * _horizontalInput;      
    }
    
    private void FixedUpdate()
    {
        // Fizik gücü uygula
        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force); 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Toplanabilir kutu ile temas kontrolü
        if (other.CompareTag("Collectible"))
        {
            CollectibleBox box = other.GetComponent();
            
            if (box != null)
            {
                // Puan ekle ve kutuyu yok et
                ScoreManager.instance.AddScore(box.ScoreValue);
                Destroy(other.gameObject);
            }
        }
    }
}
```

**📌 Önemli Noktalar:**
- **Rigidbody Kontrolü**: Fizik tabanlı hareket için `AddForce()` kullanır
- **Kamera Bazlı Hareket**: `_orientationTransform` sayesinde hareket kamera yönüne göre olur
- `Update()`: Input'ları her frame okur
- `FixedUpdate()`: Fizik hesaplamaları için sabit frame rate'te çalışır
- **Trigger Collision**: `OnTriggerEnter()` ile kutularla temas algılanır
- **Tag Kontrolü**: "Collectible" tag'ine sahip objeleri toplar
- Puan ekleme işlemi ScoreManager singleton üzerinden yapılır