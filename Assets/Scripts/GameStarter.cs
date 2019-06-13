using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _balloonPrefab;
    [SerializeField] private float _maxScores;
    private GameObject _startButton;
    private GameObject _resultBoard;
    private static float _totalScores;
    private static Text _scoreBoard;
    private Text _timerBoard;
    private float _timer;
    private float _startTime;

    [UsedImplicitly]
    public void StartGame()
    {
        Debug.Log("Game started!");
        _startButton = GameObject.Find("StartGameButton");
        _resultBoard = GameObject.Find("ResultBoard");
        _timerBoard = GameObject.Find("Time").GetComponent<Text>();
        _scoreBoard = GameObject.Find("Scores").GetComponent<Text>();
        _scoreBoard.text = "0";
        _startTime = Time.time;
        _startButton.SetActive(false);
        _resultBoard.SetActive(false);

        GamePlay();
    }

    public void ResultBoard(string timerBoardText)
    {
        _resultBoard.SetActive(true);
        _startButton.SetActive(true);
        _resultBoard.GetComponent<Text>().text = "You Win!\nTime: " + timerBoardText + " sec";
        var gameObjects = GameObject.FindGameObjectsWithTag("balloon");
        foreach (GameObject gObj in gameObjects)
        {
            Destroy(gObj);
        }

        StopCoroutine(SpawnBalls());
    }

    private void GamePlay()
    {
        _totalScores = 0;
        _timerBoard.text = "0";
        _timer = Time.time - _startTime;
        StartCoroutine(SpawnBalls());
    }

    void Update()
    {
        if (_startButton != null && !_startButton.activeSelf)
        {
            _timer += Time.deltaTime;
            int t = (int) (_timer % 60);
            _timerBoard.text = t.ToString();
        }

        if (_maxScores <= _totalScores)
            ResultBoard(_timerBoard.text);
    }

    private IEnumerator SpawnBalls()
    {
        while (_maxScores >= _totalScores)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 randomPosition = new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(-4f, 5f), 0);
            Quaternion randomRotation = transform.rotation * Quaternion.Euler(0, 0, Random.Range(0, 190));
            Instantiate(_balloonPrefab, randomPosition, randomRotation);
        }
    }

    public static void SetScore(float scores)
    {
        if (_scoreBoard != null)
        {
            _totalScores += scores;
            _scoreBoard.text = Math.Round(_totalScores).ToString();
        }
    }
}