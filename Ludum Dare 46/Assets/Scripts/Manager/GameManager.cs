using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mikabrytu.LD46
{
    public class GameManager : Singleton<GameManager>
    {
        private UIManager uiManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            uiManager = GetComponent<UIManager>();
        }

        #region Game States

        private void CallMenu()
        {

        }

        private void CallTutorial()
        {

        }

        private void CallGame()
        {

        }

        #endregion


        #region Game Lifecycle

        public void StartGame()
        {

        }

        public void GameOver()
        {

        }

        #endregion


        #region Player

        public void IncreaseFear()
        {

        }

        public void DecreaseFear()
        {

        }

        #endregion


        #region Spawn

        public void SpawnGhost()
        {

        }

        public void SpawnBrokenStuff()
        {

        }

        #endregion


        #region Zones

        public void InsideFearZone()
        {

        }

        public void OutsideFearZone()
        {

        }

        public void InsideCaptureZone()
        {

        }

        public void OutsideCaptureZone()
        {

        }

        public void InsideFixZone()
        {

        }

        public void OutsideFixZone()
        {

        }

        #endregion

    }
}
