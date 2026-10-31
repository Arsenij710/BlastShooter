using System.Collections;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.08f;

    private Material objectMaterial;
    private Color originalColor;
    private Coroutine flashCoroutine;
    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            objectMaterial = renderer.material;

            if (objectMaterial.HasProperty("_Color"))
            {
                originalColor = objectMaterial.color;
            }
        }
    }
    public void Flash()
    {
        if (objectMaterial == null) return;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        objectMaterial.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        objectMaterial.color = originalColor;

        flashCoroutine = null;
    }
    private void OnDisable()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }
        objectMaterial.color = originalColor;
    }
}
