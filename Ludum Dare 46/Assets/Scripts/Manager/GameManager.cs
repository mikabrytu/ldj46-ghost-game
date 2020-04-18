﻿using System;
using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Events;

namespace Mikabrytu.LD46
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private IPlayer player;
        [SerializeField] private IGhost ghost;
        [SerializeField] private IBrokenStuff brokenStuff;

        private UIManager uiManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            uiManager = GetComponent<UIManager>();

            EventManager.AddListener<PlayerIsDeadEvent>(GameOver);
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

        public void GameOver(PlayerIsDeadEvent e)
        {
            Time.timeScale = 0;
            Debug.Log("Player is Dead");
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
