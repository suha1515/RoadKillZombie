using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Main")]
    public TweenAlpha start;
    public TweenAlpha main;
    public GameObject panel;
    public UILabel scoreText;
    public ParticleSystem[] ps;
    int score = 0;
    public bool isStart = false, isEnd = false;

    void Update()
    {
        if (!isStart && !isEnd && Input.GetKeyDown(KeyCode.Space))
            GameStart();
        else if (!isStart && isEnd && Input.GetKeyDown(KeyCode.Space))
            Restart();
    }

    public void GameStart()
    {
        if (!isStart)
        {
            LKZ_GameManager.Instance.isStart = true;
            isStart = true;
            start.PlayForward();
            main.PlayForward();
        }
    }

    public void GameOver()
    {
        if (isStart)
        {
            LKZ_GameManager.Instance.isStart = false;
            isStart = false;
            isEnd = true;

            for (int i = 0; i < ps.Length; i++)
                ps[i].Play();

            panel.SetActive(true);
        }
    }

    public void GetPoint()
    {
        score += 100;
        scoreText.text = score.ToString();
    }



    public void Restart()
    {
        if (!isStart)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void GoToEquip()
    {

    }

    public void GoToWorid()
    {
        SceneManager.LoadScene("WorldMap");
    }

    public void GoToShop()
    {

    }

    public void GoToZombies()
    {

    }

    public void GoToBack()
    {
        Scene m_Scene = SceneManager.GetActiveScene();

        if (m_Scene.name.Equals("WorldMap"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
