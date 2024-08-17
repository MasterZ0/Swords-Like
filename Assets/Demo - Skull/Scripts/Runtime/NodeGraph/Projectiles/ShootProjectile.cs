using Z3.DemoSkull.BattleSystem;
using Z3.DemoSkull.Projectiles;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.Projectiles
{
    [NodeCategory(MenuPath.Projectiles)]
    [NodeDescription("Shoot the projectile")]
    public class ShootProjectile : ActionTask//<IStatusOwner> 
    {
        [Header("In")]
        [SerializeField] private Parameter<Projectile> projectile;

        [Header("Config")]
        [SerializeField] private Parameter<DamageData> damage;
        [SerializeField] private Parameter<float> velocity;

        public override string Info => projectile.IsBinding ?
            $"Shoot {projectile}" : base.Info;

        protected override void StartAction()
        {
            //projectile.Value.Shoot(new Damage(damage.Value, Agent), velocity.Value);
            EndAction();
        }
    }
}