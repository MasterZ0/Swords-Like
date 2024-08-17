using Z3.DemoSkull.BattleSystem;
using Z3.ObjectPooling;
using System;
using UnityEngine;

namespace Z3.DemoSkull.AI
{
    /// <summary>
    /// Used for enemies that are spawned and sent to the pool. This component will send the enemy to the Pool after call the Kill Enemy node.
    /// </summary>
    public class TemporaryEnemy : MonoBehaviour
    {
        public event Action<Enemy> OnEnemyDie;
        public event Action<TemporaryEnemy> OnRemoveEnemy;

        public Enemy Enemy { get; private set; }

        private void Awake()
        {
            Enemy = GetComponentInChildren<Enemy>(true);

            if (!Enemy)
                throw new MissingComponentException($"Missing enemy component in Game Object: {gameObject}");

            name = $"[Temporary Enemy] {name}";
        }

        private void OnEnable()
        {
            Enemy.OnFinishEnemyDeath += OnFinishEnemyDeath;
            Enemy.Status.OnTakeDamage += OnTakeDamage;

            Enemy.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            Enemy.OnFinishEnemyDeath -= OnFinishEnemyDeath;
            Enemy.Status.OnTakeDamage -= OnTakeDamage;
        }

        private void OnTakeDamage(DamageInfo obj)
        {
            if (obj.IsDead)
            {
                OnEnemyDie.Invoke(Enemy);
            }
        }

        private void OnFinishEnemyDeath()
        {
            OnRemoveEnemy(this);
            transform.ReturnToPool();
        }
    }
}