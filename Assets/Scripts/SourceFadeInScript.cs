using UnityEngine;

public class SourceFadeInScript : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private float volumeStep;

    private void Awake()
    {
        source.volume = 0f;
    }

    void Update()
    {
        if (source.volume < 1f)
            source.volume += volumeStep * Time.deltaTime;
    }
}
