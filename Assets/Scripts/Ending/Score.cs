﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class Score : MonoBehaviour
    {
        const string privateCode = "Y_XRjsodwUqgMBDtD2UD_A2VySGhXYukCWNq8If9vX2Q";
        const string publicCode = "5cdea05f3ebb3918707c2648";
        const string webURL = "http://dreamlo.com/lb/";

        public Highscore[] highscoresList;

        //contient la valeur de score globale
        int globalScore = -1;


        void Awake()
        {
            //on sassure que la database est initialisée
            AddNewHighscore("world", 0);
            //on récupère la valeur de score
            DownloadHighscores();
        }

        public void AddNewHighscore(string username, int score)
        {
            //on récupère la valeur de score
            DownloadHighscores();
            score += globalScore;
            StartCoroutine(UploadNewHighscore(username, score));
        }

        IEnumerator UploadNewHighscore(string username, int score)
        {
            WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
            yield return www;

            if (string.IsNullOrEmpty(www.error))
                print("Upload Successful");
            else
            {
                print("Error uploading: " + www.error);
            }
        }

        public void DownloadHighscores()
        {
            StartCoroutine("DownloadHighscoresFromDatabase");
        }

        IEnumerator DownloadHighscoresFromDatabase()
        {
            WWW www = new WWW(webURL + publicCode + "/pipe/");
            yield return www;

            if (string.IsNullOrEmpty(www.error))
                FormatHighscores(www.text);
            else
            {
                print("Error Downloading: " + www.error);
            }
        }

        void FormatHighscores(string textStream)
        {
            string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            highscoresList = new Highscore[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                highscoresList[i] = new Highscore(username, score);
            }
            if (highscoresList.Length > 0)
                globalScore = highscoresList[0].score;
        }

        public int GetOnlineScore()
        {
            return globalScore;
        }

        public string GetOnlineScoreToString()
        {
            if (globalScore != -1)
                return globalScore.ToString();
            else
                return "error accessing online score";
        }

    }

    public struct Highscore
    {
        public string username;
        public int score;

        public Highscore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }
}
