using UnityEngine;
using Z3.ObjectPooling;

namespace Z3.Effects
{
    public class AnimationVFX : MonoBehaviour
    {
        [Header("Animation VFX")]
        [SerializeField] protected Animator animator;

        private float lifeTime = float.PositiveInfinity;

        private void Reset() => TryGetComponent(out animator);

        protected virtual void OnEnable()
        {
            lifeTime = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                this.ReturnToPool();
            }
        }
    }
}