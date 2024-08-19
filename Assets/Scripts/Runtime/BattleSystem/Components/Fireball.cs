using UnityEngine;

namespace Z3.GMTK2024.BattleSystem
{
    public class Fireball : Projectile
    {
        private float initialRotationSpeed;
        private float timeElapsed;
        private float moveSpeed;

        public void Shoot(Damage damage, float moveSpeed, float rotationSpeed)
        {
            this.moveSpeed = moveSpeed;
            initialRotationSpeed = rotationSpeed;
            timeElapsed = 0f;

            Shoot(damage, moveSpeed);
        }

        private void FixedUpdate()
        {
            timeElapsed += Time.fixedDeltaTime;

            // A fórmula para diminuir a rotação ao longo do tempo
            float currentRotationSpeed = initialRotationSpeed / (1 + timeElapsed);

            // Rotaciona em espiral em torno do eixo Y
            transform.Rotate(0, currentRotationSpeed * Time.fixedDeltaTime, 0);

            // Mantém a velocidade de movimento para frente
            rigidbody.velocity = transform.forward * moveSpeed;
        }
    }
}