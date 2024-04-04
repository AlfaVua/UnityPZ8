using Game.Colors;
using Game.Player;
using UnityEngine;

namespace Game.Platform
{
    public abstract class PlatformBase : MonoBehaviour
    {
        private ColorController _spriteColorController;
        public Color TargetColor => _spriteColorController.GetTargetColor();

        private void Awake()
        {
            _spriteColorController = GetComponent<ColorController>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.tag.Equals("Player"))
            {
                return;
            }

            var playerController = other.gameObject.GetComponent<PlayerScript>();
            if (playerController.PreviousVelocity.y < 0 && playerController.IsGrounded)
            {
                playerController.AfterLandedOnPlatform(this);
            }
        }
    }
}