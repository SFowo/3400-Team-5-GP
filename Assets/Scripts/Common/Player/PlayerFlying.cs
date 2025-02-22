using UnityEngine;

namespace Common.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerFlying : MonoBehaviour
    {
        [SerializeField] private float flyingSpeed = 5f;

        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        public void StartFlying()
        {
            player.CurrentState = Player.State.Flying;
        }

        public void StopFlying()
        {
            player.CurrentState = Player.State.Walking;
        }

        private void UpdateMovementFlying()
        {
            var input = player.GetMovementInput(flyingSpeed, false);

            var factor = player.Acceleration * Time.deltaTime;
            player.Velocity = Vector3.Lerp(player.Velocity, input, factor);

            player.Move(player.Velocity * Time.deltaTime);
        }

        private void Update()
        {
            if (player.CurrentState == Player.State.Flying)
            {
                UpdateMovementFlying();
            }
        }

        private void OnToggleFlying()
        {
            if (player.CurrentState == Player.State.Flying)
                StopFlying();
            else
                StartFlying();
        }
    }
}