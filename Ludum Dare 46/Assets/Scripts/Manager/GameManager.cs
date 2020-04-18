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
        [SerializeField] private List<GameObject> brokenStuffPrefabs;
        [SerializeField] private List<Transform> brokenStuffSpawnPoints;
        [SerializeField] private float breakStuffTime = 10f;

        private IEnumerator breakRoutine;
        private UIManager uiManager;
        private int brokenStuffCount;
        private int brokenStuffIndex;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            uiManager = GetComponent<UIManager>();

            EventManager.AddListener<PlayerIsDeadEvent>(GameOver);
            EventManager.AddListener<PlayerFixedStuffEvent>(OnPlayerFixed);

            CallGame();
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
            StartGame();
        }

        #endregion


        #region Game Lifecycle

        public void StartGame()
        {
            SpawnBrokenStuff();
            SpawnGhost();
        }

        public void OnPlayerFixed(PlayerFixedStuffEvent e)
        {
            brokenStuffCount--;
            if (brokenStuffCount < 0)
                brokenStuffCount = 0;

            if (breakRoutine != null)
                StopCoroutine(breakRoutine);

            SpawnBrokenStuff();
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
            if (brokenStuffCount > (brokenStuffPrefabs.Count - 1))
                return;

            Instantiate(
                brokenStuffPrefabs[brokenStuffIndex],
                brokenStuffSpawnPoints[UnityEngine.Random.Range(0, brokenStuffSpawnPoints.Count)].position,
                Quaternion.identity);

            brokenStuffCount++;

            if (brokenStuffIndex >= (brokenStuffPrefabs.Count - 1))
                brokenStuffIndex = 0;
            else
                brokenStuffIndex++;

            breakRoutine = BreakStuffTimer();
            StartCoroutine(breakRoutine);
        }

        #endregion

        public IEnumerator BreakStuffTimer()
        {
            yield return new WaitForSeconds(breakStuffTime);

            SpawnBrokenStuff();
        }

    }
}
