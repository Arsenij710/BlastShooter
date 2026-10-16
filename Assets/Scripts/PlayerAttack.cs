using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private int _maxBullets;
    [SerializeField] private int _currBullets;

    [Header("Refs")]
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInput _playerInput;

    [Header("Settings")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private LayerMask groundLayer;
    private bool _inputMouse0 => _playerInput.InputMouse_0;
    private bool _inputMouseDown0 => _playerInput.InputMouseDown_0;
    public int MaxBullets => _maxBullets;
    public int CurrentBullets { get; private set; }

    public static event Action<int, int> OnAmmoChanged;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        CurrentBullets = MaxBullets;
        OnAmmoChanged?.Invoke(CurrentBullets, MaxBullets);
    }

    private void Update()
    {
        if (_inputMouseDown0)
        {
            if (CurrentBullets > 0)
                ShootAtCursor();
        }
    }
    private void ShootAtCursor()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, groundLayer))
        {
            CurrentBullets--;
            OnAmmoChanged?.Invoke(CurrentBullets, MaxBullets);

            Vector3 targetPosition = hitInfo.point;
            targetPosition.y = firePoint.position.y;

            Vector3 direction = (targetPosition - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));

            if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
        }
    }
    public void RestoreBullet() 
    {
        CurrentBullets++;
        OnAmmoChanged?.Invoke(CurrentBullets, MaxBullets);
    }
}
