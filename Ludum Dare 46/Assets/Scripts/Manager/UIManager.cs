using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Mikabrytu.LD46
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject menuCanvas, tutorialCanvas, gameCanvas, retryCanvas;
        [SerializeField] private TextMeshProUGUI playerBPM;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            
        }

        public void ShowMenu()
        {
            menuCanvas.SetActive(true);
            tutorialCanvas.SetActive(false);
            gameCanvas.SetActive(false);
            retryCanvas.SetActive(false);
        }

        public void ShowTutorial()
        {
            menuCanvas.SetActive(false);
            tutorialCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            retryCanvas.SetActive(true);
        }

        public void ShowGame()
        {
            menuCanvas.SetActive(false);
            tutorialCanvas.SetActive(false);
            gameCanvas.SetActive(true);
            retryCanvas.SetActive(false);
        }

        public void ShowRetry()
        {
            menuCanvas.SetActive(false);
            tutorialCanvas.SetActive(false);
            gameCanvas.SetActive(false);
            retryCanvas.SetActive(true);
        }

        public void ShowIntroText()
        {

        }

        public void UpdatePlayerBPM(int bpm)
        {
            playerBPM.text = bpm.ToString();
        }
    }
}
