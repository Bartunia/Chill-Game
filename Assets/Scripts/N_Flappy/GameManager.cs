using UnityEngine;

namespace N_Flappy
{
    public class GameManager : MonoBehaviour
    {
        private int _score;

        public void GameOver()
        {
            Debug.Log("game over");
        }
        public void IncreaseScore()
        {
            _score++;
        }
    }
}
