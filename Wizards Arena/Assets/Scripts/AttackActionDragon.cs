using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.AgentSystem
{

    [TaskCategory("AgentSystem")]
    public class AttackActionDragon : Action
    {

        [Tooltip("Target GameObject")]
        public SharedGameObject target;
        [Tooltip("Damage to the turret")]
        public SharedInt damage;

        private TurretHealth targetTower;

        public override void OnStart()
        {
            base.OnStart();

            if (target.Value == null)
            {
                return;
            }
            MinionHealth targetTower = target.Value.GetComponent<MinionHealth>();
            string damageTaken = damage.ToString();
            targetTower.TakeDamage(int.Parse(damageTaken));

        }

        // Follow the target. The task will never return success as the agent should continue to follow the target even after arriving at the destination.
        public override TaskStatus OnUpdate()
        {
            if (target.Value == null)
            {
                return TaskStatus.Success;
            }


            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            base.OnReset();
            target = null;
        }
    }
}