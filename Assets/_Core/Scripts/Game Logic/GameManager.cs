
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

namespace GorillaZilla
{
    [RequireComponent(typeof(Level))]
    public class GameManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] Player player;

        [Header("References")]
        [SerializeField] WaveDisplay waveDisplay;
        [SerializeField] AudioSource sfx_BackgroundMusic;
        [SerializeField] AudioSource sfx_WaveEnd;

        [Header("Wave Settings")]
        [SerializeField] float delayBetweenWaves = 1f;
        [SerializeField] Wave waveTemplate;
        private Wave curWave;
        private Level level;
        private int waveNum = 0;

        private bool isGameOver = false;

        private void Awake()
        {
            level = GetComponent<Level>();
            level.onWaveSpawned.AddListener(OnWaveSpawned);
            level.onLastEnemyDestroyed.AddListener(OnLastEnemyDestroyed);

            if (player == null) player = GameObject.FindObjectOfType<Player>();
            player.onPlayerHit.AddListener(OnPlayerHit);
        }

        private void OnWaveSpawned()
        {
            // throw new NotImplementedException();
        }
        private void OnLastEnemyDestroyed()
        {
            EndWave();
        }
        public void OnPlayerHit()
        {
            waveNum = 0;
            GameOver();
        }
        Wave MakeWave(int waveNum)
        {
            Wave wave = Wave.Copy(waveTemplate);
            int numBuildings = Mathf.Min(waveTemplate.numBuildings + waveNum, 20);
            int numEnemies = Mathf.Min(waveTemplate.numEnemies + waveNum, 20);
            wave.numBuildings = numBuildings;
            wave.numEnemies = numEnemies;
            return wave;
        }
        [ContextMenu("Start Game")]
        public void StartGame()
        {
            waveNum = 0;
            StartNextWave();
            player.Revive();

            player.menu.ToggleMenu(false);
        }
        void StartNextWave()
        {
            curWave = MakeWave(waveNum);
            StartCoroutine(WaveSequence(curWave));
        }
        void EndWave()
        {
            sfx_BackgroundMusic.Stop();
            if (!isGameOver)
            {
                waveNum++;
                StartNextWave();
                sfx_WaveEnd.Play();
            }
        }
        IEnumerator WaveSequence(Wave wave)
        {
            player.timeManipulator.enabled = false;

            waveDisplay.ShowMessage("WAVE " + (waveNum + 1));
            isGameOver = false;
            if (waveNum >= 1)
                yield return StartCoroutine(level.ClearLevelAnimated(delayBetweenWaves));
            yield return new WaitForSeconds(delayBetweenWaves);
            if (!PlayerSettings.MuteMusic)
                sfx_BackgroundMusic.Play();

            waveDisplay.Hide();
            yield return StartCoroutine(level.SpawnWave(wave));
            player.timeManipulator.enabled = true;

        }
        void GameOver()
        {
            level.ClearLevel();
            player.timeManipulator.enabled = false;
            isGameOver = true;
            waveDisplay.ShowMessage("GAME OVER");
            StartCoroutine(GameOverSequence());
        }

        IEnumerator GameOverSequence()
        {
            yield return new WaitForSeconds(delayBetweenWaves);
            waveDisplay.Hide();
            player.menu.ToggleMenu(true);
            player.menu.OpenStartPage();
            player.Revive();
        }
    }
}
