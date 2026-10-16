using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _dir;

    [Header("Refs")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerInput _playerInput;

    private float _inputX => _playerInput.InputAxisX;
    private float _inputY => _playerInput.InputAxisY;
    private bool _inputMouse0 => _playerInput.InputMouse_0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = FixedMove(_inputX, _inputY);

        Rotation();
    }
    private void Rotation()
    {
        Vector3 direction = _dir;
        var rotationSpeed = _rotationSpeed;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer) && _inputMouse0)
        {
            Vector3 targetPoint = hit.point;
            targetPoint.y = transform.position.y;

            direction = (targetPoint - transform.position).normalized;
            rotationSpeed *= 4f;
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float maxDegreeDelta = rotationSpeed * Time.fixedDeltaTime;

            _rb.MoveRotation(Quaternion.RotateTowards(_rb.rotation, targetRotation, maxDegreeDelta));
        }
    }

    private Vector3 FixedMove(float inputX, float inputY)
    {
        _dir = new Vector3(inputX, 0f, inputY).normalized;
        return _dir * _speed;
    }
}
