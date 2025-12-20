using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private Transform _orientationTransform;

    private Rigidbody _playerRigidbody;

    private float _verticalInput, _horizontalInput;

    private Vector3 _movementDirection;

    [SerializeField] private float _movementSpeed = 10f;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
    }


    private void FixedUpdate()
    {

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            CollectibleBox box = other.GetComponent<CollectibleBox>();
            if (box != null)
            {
                ScoreManager.instance.AddScore(box.ScoreValue);
                // Patlama efekti(isteðe baðlý)
                Explode(other);

                Destroy(other.gameObject);
            }
        }
    }

    void Explode(Collider other)
    {
        Renderer rend = other.GetComponent<Renderer>();
        Color boxColor = rend.material.color;
        boxColor *= Random.Range(0.85f, 1.15f);

        GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);

        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = boxColor;
    }

}
