using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : PooledObject
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float hideDelay = 2f;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 2f, 0);

    private Transform _target;
    private Coroutine _hideCoroutine;
    private Action _onReleasedCallback;

    public void Initialize(Transform target, float currentHealth, float maxHealth, Action onReleasedCallback)
    {
        _target = target;
        _onReleasedCallback = onReleasedCallback;
        Refresh(currentHealth, maxHealth);
    }
    private void LateUpdate()
    {
        if (_target == null)
        {
            ReleaseSelf();
            return;
        }

        transform.position = _target.position + healthBarOffset;
    }
    public void Refresh(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;

        if (_hideCoroutine != null) StopCoroutine(_hideCoroutine);
        _hideCoroutine = StartCoroutine(HideTimerRoutine());
    }

    private IEnumerator HideTimerRoutine()
    {
        yield return new WaitForSeconds(hideDelay);
        ReleaseSelf();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        if (_hideCoroutine != null) StopCoroutine(_hideCoroutine);
        _hideCoroutine = StartCoroutine(HideTimerRoutine());
    }
    public override void OnDespawn()
    {
        base.OnDespawn();

        _onReleasedCallback?.Invoke();
        _onReleasedCallback = null;
    }
}
