using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float _scoreScaler;
    [SerializeField] private float _scaleSpeedMin;
    [SerializeField] private float _scaleSpeedMax;
    [SerializeField] private Vector3 _minScale;
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Color[] _colors;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioData;

    void Start()
    {
        transform.localScale = _minScale;
        if (_colors.Length != 0)
        {
            Color color = _colors[Random.Range(0, _colors.Length)];
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _maxScale, Random.Range(_scaleSpeedMin, _scaleSpeedMax) * Time.deltaTime);
        if (transform.localScale.x > _maxScale.x * 0.9f)
        {
            var scores = -(_scoreScaler * transform.localScale.x);
            GameStarter.SetScore(scores);
            Destroy(gameObject);
        }
    }

    private void OnMouseUpAsButton()
    {
        _audioData.clip = _audioClip;
        var scores = _scoreScaler * transform.localScale.x * _scaleSpeedMax;
        GameStarter.SetScore(scores);
        _audioData.Play();
        Destroy(gameObject);
    }
}