using UnityEngine;
using System.Collections;

public class EchoPing : MonoBehaviour
{
    public GameObject pingVisualPrefab;
    public float maxRadius = 10f;
    public float pingSpeed = 15f;
    public LayerMask echoLayerMask; // ← Add this line

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            {
                if (pingVisualPrefab == null)
                {
                    Debug.LogError("❌ pingVisualPrefab is STILL null.");
                }
                else
                {
                    Debug.Log("✅ pingVisualPrefab is assigned. Spawning...");
                    StartCoroutine(PingRoutine());
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