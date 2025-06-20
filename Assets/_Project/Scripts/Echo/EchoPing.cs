using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EchoPing : MonoBehaviour
{
    public GameObject pingVisualPrefab;
    public float maxRadius = 10f;
    public float pingSpeed = 15f;
    public float cooldown = 2f;
    public LayerMask echoLayerMask;
    public PingCooldownUI cooldownUI;

    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (pingVisualPrefab == null)
            {
                Debug.LogError("❌ pingVisualPrefab is STILL null.");
            }
            else
            {
                Debug.Log("✅ pingVisualPrefab is assigned. Spawning...");
                StartCoroutine(PingRoutine());
                cooldownTimer = cooldown;
                if (cooldownUI != null)
                {
                    cooldownUI.StartCooldown(cooldown);
                }
            }
        }
    }

    IEnumerator PingRoutine()
    {
        if (pingVisualPrefab == null)
        {
            Debug.LogError("pingVisualPrefab is NULL at runtime.");
        }
        GameObject ping = Instantiate(pingVisualPrefab, transform.position, Quaternion.identity);

        EchoPulse pulse = ping.GetComponent<EchoPulse>();
        pulse.echoLayerMask = echoLayerMask;
        pulse.StartExpanding(maxRadius, pingSpeed);

        yield return null;
    }
}
