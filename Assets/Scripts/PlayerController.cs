using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
                Destroy(other.gameObject);
            }
        }
    }

}
