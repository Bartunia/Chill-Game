using TMPro;
using UnityEngine;

namespace N_Flappy
{
    public class GameManager : MonoBehaviour
    {
        public BirdMovement player;
        public TextMeshProUGUI scoreText;
        public GameObject playButton;
        public GameObject gameOver;
        
        private int _score;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            Pause();
        }

        public void Play()
        {
            _score = 0;
            scoreText.text = _score.ToString();
            
            playButton.SetActive(false);
            gameOver.SetActive(false);

            Time.timeScale = 1f;

            player.enabled = true;

            Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
            for (int i = 0; i < pipes.Length; i++)
            {
                Destroy(pipes[i].gameObject);
            }
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            player.enabled = false;
        }

        public void GameOver()
        {
           gameOver.SetActive(true);
           playButton.SetActive(true);
           
           Pause();
        }
        public void IncreaseScore()
        {
            _score++;
            scoreText.text = _score.ToString();
        }
    }
}
