using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Blade blade;
    private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Image fadeimage;
    [SerializeField] private GameObject endGame;
    private int score;
    private int finalScore;
    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }
    private void Start()
    {
        newGame();
    }
    private void newGame()
    {
        finalScore = score;
        finalScoreText.text = finalScore.ToString();

        Time.timeScale = 1.0f;

        blade.enabled = true;
        spawner.enabled = true;

        ClearScene();

        score = 0;
        scoreText.text = score.ToString();
    }
    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach(Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }
    public void increseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(explodeSequence());
    }

    private IEnumerator explodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeimage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);

        newGame();
        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeimage.color = Color.Lerp(Color.white, Color.clear, t);

            Time.timeScale = 1f - t;

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.1f);
        endGame.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public  void QuitGame()
    {
        Application.Quit();
    }
    
}
