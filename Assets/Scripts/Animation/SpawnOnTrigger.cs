using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aperture.StateMachine;

namespace Boss
{
    public class SpawnOnTrigger : MonoBehaviour
    {
        //public Transform _spawnEnemyPoint;
        //public Transform _spawnBossPoint;
        public GameObject Enemy_01;
        public GameObject Enemy_02;
        // GameObject Enemy_Boss;
        //public BossBase bossBase;


        private void Awake()
        {
            Enemy_01.gameObject.SetActive(false);
            Enemy_02.gameObject.SetActive(false);
            //Enemy_Boss.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            SetActiveEnemy();
            SetActiveEnemy2();
            SetActiveBoss();
           
            //Instantiate(Enemy, _spawnEnemyPoint.position, _spawnEnemyPoint.rotation);
            //Instantiate(EnemyBoss, _spawnBossPoint.position, _spawnBossPoint.rotation);
            //EnemyBoss.transform.localPosition = EnemyBoss.transform.localEulerAngles = Vector3.zero;


            Destroy(gameObject);
        }


       private void SetActiveEnemy()
        {
            Enemy_01.gameObject.SetActive(true);
        }

        private void SetActiveEnemy2()
        {
            Enemy_02.gameObject.SetActive(true);
        }

        private void SetActiveBoss()
        {
            /*Enemy_Boss.gameObject.SetActive(true);
            bossBase.SwitchState(BossAction.ATTACK);*/
        }
    }
}