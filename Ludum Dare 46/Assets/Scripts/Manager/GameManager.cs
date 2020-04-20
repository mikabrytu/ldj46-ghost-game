using System;
using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Events;
using System.Collections.Generic;
using System.Collections;

namespace Mikabrytu.LD46
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private IPlayer player;
        [SerializeField] private IGhost ghost;
        [SerializeField] private float breakStuffTime = 10f;

        private IEnumerator breakRoutine;
        private UIManager uiManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            uiManager = GetComponent<UIManager>();

            EventManager.AddListener<PlayerIsDeadEvent>(GameOver);
            EventManager.AddListener<PlayerFixedStuffEvent>(OnPlayerFixed);
            EventManager.AddListener<PlayerReachBrokenStuffEvent>(OnPlayerSeeBrokenStuff);
            EventManager.AddListener<PlayerLeavingBrokenStuffEvent>(OnPlayerLeaveBrokenStuff);

            CallMenu();
        }

        private void Update()
        {
            uiManager.UpdatePlayerBPM(player.GetHeartBPM());
        }

        #region Game States

        public void CallMenu()
        {
            uiManager.ShowMenu();
        }

        public void CallTutorial()
        {
            uiManager.ShowTutorial();
        }

        public void CallGame()
        {
            uiManager.ShowGame();
            StartGame();
        }

        #endregion


        #region Game Lifecycle

        private void StartGame()
        {
            player.SetInitialPosition();
            SpawnBrokenStuff();
            SpawnGhost();
        }

        private void OnPlayerFixed(PlayerFixedStuffEvent e)
        {
            uiManager.HideWarning();

            if (breakRoutine != null)
                StopCoroutine(breakRoutine);

            SpawnBrokenStuff();
        }

        private void OnPlayerSeeBrokenStuff(PlayerReachBrokenStuffEvent e)
        {
            uiManager.ShowWarning(e.position);
        }

        private void OnPlayerLeaveBrokenStuff(PlayerLeavingBrokenStuffEvent e)
        {
            uiManager.HideWarning();
        }

        private void GameOver(PlayerIsDeadEvent e)
        {
            Debug.Log("Player is Dead");

            player.StopMovement();
            ghost.StopMovement();

            StartCoroutine(PlayGhostCutscene());
        }

        #endregion


        #region Spawn

        private void SpawnGhost()
        {
            ghost.Enable(true);
        }

        private void SpawnBrokenStuff()
        {

            breakRoutine = BreakStuffTimer();
            StartCoroutine(breakRoutine);
        }

        #endregion

        public IEnumerator BreakStuffTimer()
        {
            yield return new WaitForSeconds(breakStuffTime);

            SpawnBrokenStuff();
        }

        public IEnumerator PlayGhostCutscene()
        {
            yield return new WaitForSeconds(3f);

            ghost.Enable(false);
            uiManager.ShowRetry();
        }

    }
}
