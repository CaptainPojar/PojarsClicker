using UnityEngine;

public class Chpok : MonoBehaviour
{
    private ParticleSystem _particle;

    void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        var audioLength = GetComponentInChildren<AudioSource>().clip.length;
        Destroy(gameObject, audioLength);
    }
}
