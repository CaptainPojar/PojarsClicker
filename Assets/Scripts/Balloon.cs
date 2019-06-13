using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float _scoreScaler;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private Vector3 _minScale;
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Color[] _colors;

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
        transform.localScale = Vector3.Lerp(transform.localScale, _maxScale, _scaleSpeed * Time.deltaTime);
        if (transform.localScale.x > _maxScale.x * 0.9f)
        {
            var scores = -(_scoreScaler * transform.localScale.x);
            GameStarter.SetScore(scores);
            Destroy(gameObject);
        }
    }

    private void OnMouseUpAsButton()
    {
        var scores = _scoreScaler * transform.localScale.x;
        GameStarter.SetScore(scores);
        Destroy(gameObject);
    }
}