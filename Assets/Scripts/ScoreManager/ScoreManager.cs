using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class ScoreManager : MonoBehaviour
    {

        private static bool m_ShuttingDown = false;
        private static object m_Lock = new object();
        private static ScoreManager m_Instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static ScoreManager Instance
        {
            get
            {
                if (m_ShuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(ScoreManager) +
                        "' already destroyed. Returning null.");
                    return null;
                }

                lock (m_Lock)
                {
                    if (m_Instance == null)
                    {
                        // Search for existing instance.
                        m_Instance = (ScoreManager)FindObjectOfType(typeof(ScoreManager));

                        // Create new instance if one doesn't already exist.
                        if (m_Instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            m_Instance = singletonObject.AddComponent<ScoreManager>();
                            singletonObject.name = typeof(ScoreManager).ToString() + " (Singleton)";

                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return m_Instance;
                }
            }
        }


        private void OnApplicationQuit()
        {
            m_ShuttingDown = true;
        }


        private void OnDestroy()
        {
            m_ShuttingDown = true;
        }

        protected ScoreManager() { }


        public static ScoreManager instance = null;

        private Score score;

        private int playerScore = 0;
        private int onlineScore = 0;
        

        // Start is called before the first frame update
        private void Awake()
        {
            score = GetComponent<Score>();
            if (score == null)
            {
                score = this.gameObject.AddComponent<Score>();
            }

            onlineScore = score.GetOnlineScore();
        }

        public void increaseScore(int x)
        {
            playerScore += x;
        }

        public int getPlayerScore()
        {
            return playerScore;
        }

        public int getOnlineScore()
        {
            return onlineScore;
        }

        public void resetScore()
        {
            playerScore = 0;
        }

        public void postScore()
        {
            score.AddNewHighscore("world", playerScore + onlineScore);
        }
    }
}
