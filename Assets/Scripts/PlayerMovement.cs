using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 _dir;
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        var dirX = Input.GetAxisRaw("Horizontal");
        var dirY = Input.GetAxisRaw("Vertical");

        _dir = new Vector3(dirX, 0f, dirY);
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = _dir * speed;
    }

}
