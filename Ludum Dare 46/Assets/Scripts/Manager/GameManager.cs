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
        private List<GameObject> brokenStuffInstances;
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

            brokenStuffInstances = new List<GameObject>();

            CallMenu();
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
            //SpawnBrokenStuff();
            SpawnGhost();
        }

        private void OnPlayerFixed(PlayerFixedStuffEvent e)
        {
            brokenStuffCount--;
            if (brokenStuffCount < 0)
                brokenStuffCount = 0;

            if (breakRoutine != null)
                StopCoroutine(breakRoutine);

            SpawnBrokenStuff();
        }

        private void GameOver(PlayerIsDeadEvent e)
        {
            Debug.Log("Player is Dead");

            player.StopMovement();
            ghost.StopMovement();

            StartCoroutine(PlayGhostCutscene());

            foreach (GameObject item in brokenStuffInstances)
                Destroy(item);
            brokenStuffInstances.Clear();
            brokenStuffCount = 0;
            brokenStuffIndex = 0;
        }

        #endregion


        #region Spawn

        private void SpawnGhost()
        {
            ghost.Enable(true);
        }

        private void SpawnBrokenStuff()
        {
            if (brokenStuffCount > (brokenStuffPrefabs.Count - 1))
                return;

            brokenStuffInstances.Add(Instantiate(
                brokenStuffPrefabs[brokenStuffIndex],
                brokenStuffSpawnPoints[UnityEngine.Random.Range(0, brokenStuffSpawnPoints.Count)].position,
                Quaternion.identity));

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

        public IEnumerator PlayGhostCutscene()
        {
            yield return new WaitForSeconds(3f);

            ghost.Enable(false);
            uiManager.ShowRetry();
        }

    }
}
