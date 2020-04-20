using System;
using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Events;
using System.Collections.Generic;
using System.Collections;

namespace Mikabrytu.LD46
{
    public enum BrokenStuffTypes { Chair, Bed, Plumbing, Window } 

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private IPlayer player;
        [SerializeField] private IGhost ghost;
        [SerializeField] private List<IBrokenStuff> chairs;
        [SerializeField] private List<IBrokenStuff> beds;
        [SerializeField] private List<IBrokenStuff> plumbings;
        [SerializeField] private List<IBrokenStuff> windows;
        [SerializeField] private float breakStuffTime = 10f;

        private IEnumerator breakRoutine;
        private List<BrokenStuffTypes> availableTypes;
        private UIManager uiManager;
        private AudioManager audioManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            uiManager = GetComponent<UIManager>();
            audioManager = GetComponent<AudioManager>();

            EventManager.AddListener<PlayerIsDeadEvent>(GameOver);
            EventManager.AddListener<PlayerFixedStuffEvent>(OnPlayerFixed);
            EventManager.AddListener<PlayerReachBrokenStuffEvent>(OnPlayerSeeBrokenStuff);
            EventManager.AddListener<PlayerLeavingBrokenStuffEvent>(OnPlayerLeaveBrokenStuff);
            EventManager.AddListener<PlayerFixingEvent>(OnPlayerFixing);
            EventManager.AddListener<PlayerChangeBPMEvent>(OnPLayerBPMChange);

            CallMenu();
        }

        private void OnPLayerBPMChange(PlayerChangeBPMEvent e)
        {
            uiManager.ChangeHeartBeat(e.isIncreasing);
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
            ResetAvailableTypes();
            SpawnBrokenStuff();
            SpawnGhost();
        }

        private void OnPlayerFixing(PlayerFixingEvent e)
        {
            player.SetFixing(e.isFixing);
        }

        private void OnPlayerFixed(PlayerFixedStuffEvent e)
        {
            player.SetFixing(false);
            uiManager.ShowWarning(false, Vector2.zero);
            uiManager.ShowMessage(false, e.type);
            availableTypes.Add(e.type);

            if (breakRoutine != null)
                StopCoroutine(breakRoutine);

            SpawnBrokenStuff();
        }

        private void OnPlayerSeeBrokenStuff(PlayerReachBrokenStuffEvent e)
        {
            uiManager.ShowWarning(true, e.position);
        }

        private void OnPlayerLeaveBrokenStuff(PlayerLeavingBrokenStuffEvent e)
        {
            uiManager.ShowWarning(false, Vector2.zero);
        }

        private void GameOver(PlayerIsDeadEvent e)
        {
            player.StopMovement();
            ghost.StopMovement();

            foreach (IBrokenStuff item in chairs)
                item.Enable(false);

            foreach (IBrokenStuff item in beds)
                item.Enable(false);

            foreach (IBrokenStuff item in plumbings)
                item.Enable(false);

            foreach (IBrokenStuff item in windows)
                item.Enable(false);

            uiManager.ResetMessages();
            audioManager.PlayFootstep(false);

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
            if (availableTypes.Count == 0)
            {
                return;
            }

            BrokenStuffTypes category = availableTypes[UnityEngine.Random.Range(0, availableTypes.Count)];
            availableTypes.Remove(category);
            uiManager.ShowMessage(true, category);
            audioManager.PlayBroke();

            switch(category)
            {
                case BrokenStuffTypes.Chair:
                    chairs[UnityEngine.Random.Range(0, chairs.Count)].Enable(true);
                    break;
                case BrokenStuffTypes.Bed:
                    beds[UnityEngine.Random.Range(0, beds.Count)].Enable(true);
                    break;
                case BrokenStuffTypes.Plumbing:
                    plumbings[UnityEngine.Random.Range(0, plumbings.Count)].Enable(true);
                    break;
                case BrokenStuffTypes.Window:
                    windows[UnityEngine.Random.Range(0, windows.Count)].Enable(true);
                    break;
            }

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
            yield return new WaitForSeconds(2f);

            ghost.Enable(false);
            uiManager.ShowRetry();
        }

        private void ResetAvailableTypes()
        {
            availableTypes = new List<BrokenStuffTypes>() {
                BrokenStuffTypes.Chair,
                BrokenStuffTypes.Bed,
                BrokenStuffTypes.Plumbing,
                BrokenStuffTypes.Window
            };
        }

    }
}
