using UnityEngine;
using UnityEngine.UI;

public class PingCooldownUI : MonoBehaviour
{
    public Image fillImage;
    private float duration;
    private float timer;

    public void StartCooldown(float time)
    {
        duration = time;
        timer = time;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (fillImage != null)
            {
                fillImage.fillAmount = 1f - Mathf.Clamp01(timer / duration);
            }
        }
        else if (fillImage != null)
        {
            fillImage.fillAmount = 1f;
        }
    }
}
