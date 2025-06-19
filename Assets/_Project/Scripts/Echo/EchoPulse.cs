using UnityEngine;
using System.Collections;

public class EchoPulse : MonoBehaviour
{
    public LayerMask echoLayerMask;
    private float maxRadius;
    private float pingSpeed;


    public void StartExpanding(float max, float speed)
    {
        maxRadius = max;
        pingSpeed = speed;
        StartCoroutine(ExpandAndFade());
    }

    IEnumerator ExpandAndFade()
    {
        float currentRadius = 0f;
        transform.localScale = Vector3.zero;

        while (currentRadius < maxRadius)
        {
            currentRadius += pingSpeed * Time.deltaTime;
            float scale = currentRadius * 2f;
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
{
    if (((1 << other.gameObject.layer) & echoLayerMask) != 0)
    {
        Renderer rend = other.GetComponent<Renderer>();
        if (rend != null)
        {
            // Instead of forcing a new material every time,
            // this grabs the renderer's instanced material safely
            Material instancedMat = rend.material;

            instancedMat.EnableKeyword("_EMISSION");
            instancedMat.SetColor("_EmissionColor", Color.cyan * 3f);

            StartCoroutine(FadeOutEmission(instancedMat));
        }
    }
}

   IEnumerator FadeOutEmission(Material mat)
    {
        Debug.Log("🌀 Starting FadeOutEmission");

        float duration = 1f;
        float t = 0f;

        Color startColor = mat.GetColor("_EmissionColor");
        Color endColor = Color.black;

        while (t < duration)
        {
            t += Time.deltaTime;
            Color lerped = Color.Lerp(startColor, endColor, t / duration);
            mat.SetColor("_EmissionColor", lerped);
            yield return null;
        }

        mat.SetColor("_EmissionColor", Color.black);
        mat.DisableKeyword("_EMISSION");

        Debug.Log("✅ Emission faded out");
    }
}