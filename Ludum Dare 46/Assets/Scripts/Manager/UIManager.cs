using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mikabrytu.LD46
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject menuCanvas, tutorialCanvas, gameCanvas, retryCanvas;
        [SerializeField] private GameObject warningIcon;
        [SerializeField] private GameObject messageChair, messageBed, messagePlumbing, messageWindow;
        [SerializeField] private TextMeshProUGUI playerBPM;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            ShowWarning(false, Vector2.zero);
            ResetMessages();
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

        public void ShowWarning(bool show, Vector2 position)
        {
            if (show)
                warningIcon.transform.position = position;

            warningIcon.SetActive(show);
        }

        public void ShowMessage(bool show, BrokenStuffTypes type)
        {
            switch (type)
            {
                case BrokenStuffTypes.Chair:
                    messageChair.SetActive(show);
                    break;
                case BrokenStuffTypes.Bed:
                    messageBed.SetActive(show);
                    break;
                case BrokenStuffTypes.Plumbing:
                    messagePlumbing.SetActive(show);
                    break;
                case BrokenStuffTypes.Window:
                    messageWindow.SetActive(show);
                    break;
            }
        }

        public void ResetMessages()
        {
            messageChair.SetActive(false);
            messageBed.SetActive(false);
            messagePlumbing.SetActive(false);
            messageWindow.SetActive(false);
        }
    }
}
