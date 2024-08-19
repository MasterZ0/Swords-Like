using System.Collections.Generic;
using UnityEngine;
using Z3.GMTK2024.BattleSystem;
using Z3.NodeGraph.Core;
using Z3.ObjectPooling;

namespace Z3.GMTK2024.NgTasks
{
    public class MeteorRainAttack : BossActionState
    {
        [SerializeField] private Parameter<Meteor> meteorPrefab;
        [SerializeField] private Parameter<Transform> arenaCenter;

        private Meteor prefab;
        private float time;

        private List<(float duration, Vector3 blockedPosition)> blockedRegions;

        protected override void StartAction()
        {
            base.StartAction();

            prefab = meteorPrefab.Value;
            time = 0f;
            blockedRegions = new List<(float, Vector3)>();
        }

        protected override void UpdateAction()
        {
            SpawnMeteor();

            time += DeltaTime;
            if (time >= ShieldBossData.MeteorAttackDuration)
            {
                EndAction();
            }
        }

        private void SpawnMeteor()
        {
            // CleanupBlockedRegions
            blockedRegions.RemoveAll(region => Time.time - region.duration > ShieldBossData.MeteorAttackDelayForBlockRegion);

            bool success = FindValidMeteorPosition(out Vector3 position);
            if (!success)
                return;

            // Spawn meteor
            Meteor meteorInstance = ObjectPool.SpawnPooledObject(prefab, position, Quaternion.identity);
            meteorInstance.SetDamage(ShieldBossData.MeteorAttackDamage);

            blockedRegions.Add((Time.time, position));
        }

        private bool FindValidMeteorPosition(out Vector3 spawnPosition)
        {
            const int MaxAttempts = 100;

            for (int attempts = 0; attempts < MaxAttempts; attempts++)
            {
                // Get random position in arena
                Vector2 randomCircle = Random.insideUnitCircle * ShieldBossData.MeteorAttackCenterRadius;
                spawnPosition = new Vector3(randomCircle.x, 0, randomCircle.y) + arenaCenter.Value.position;

                // Check if is free
                bool isValidPosition = true;
                foreach ((float _ , Vector3 blockedPosition) in blockedRegions)
                {
                    if (Vector3.Distance(spawnPosition, blockedPosition) < ShieldBossData.MeteorAttackRadiusForBlockRegion)
                    {
                        isValidPosition = false;
                        break;
                    }
                }

                // Found free region
                if (isValidPosition)
                    return true;
            }

            spawnPosition = Vector3.zero;
            return false;
        }
    }
}
