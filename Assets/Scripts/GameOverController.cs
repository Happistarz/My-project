using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    
    [SerializeField] private TMP_Text scoreText;
    
    private void Start()
    {
        scoreText.text = $"Score: {GameManager.Instance.Score}";
    }
    
    public void RestartGame()
    {
        Debug.Log("Restarting game");
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("Main");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
