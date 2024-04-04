using System;
using Game.Platform;
using UnityEngine;

namespace Game.Player
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private LayerMask platformMask;
        [SerializeField] private GameController gameController;
        [SerializeField] private Transform playerViewTransform;
        [SerializeField] private ParticleSystem landingAnimation;
        [SerializeField] private float rayCastingOffsetY = 0;
        [SerializeField] private float jumpPower = 7;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float raycastDistance = 1;
        
        private Vector2 _previousVelocity;
        public Vector2 PreviousVelocity => _previousVelocity;
        private Vector3 LeftRayStartPoint =>
            rigidBody.transform.position + Vector3.left * transform.localScale.x / 2 + Vector3.up * rayCastingOffsetY;
        private Vector3 RightRayStartPoint =>
            rigidBody.transform.position + Vector3.right * transform.localScale.x / 2 + Vector3.up * rayCastingOffsetY;
        public bool IsGrounded =>
            Physics2D.Raycast(LeftRayStartPoint, Vector2.down, raycastDistance, platformMask) ||
            Physics2D.Raycast(RightRayStartPoint, Vector2.down, raycastDistance, platformMask);

        private void Update()
        {
            UpdateMovement();
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
                JumpAction();
            }
        }

        private void FixedUpdate()
        {
            _previousVelocity = rigidBody.velocity;
        }

        private void JumpAction()
        {
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        private void UpdateMovement()
        {
            // Змінив з GetAxis для керування без інерції
            float x = 0;
            if (Input.GetKey(KeyCode.A)) x = -1;
            else if (Input.GetKey(KeyCode.D)) x = 1;
            ChangeViewDirection(x);
            rigidBody.velocity = new Vector2(x * moveSpeed, rigidBody.velocity.y);
        }

        private void ChangeViewDirection(float directionX)
        {
            if (directionX == 0) return;
            var localScale = playerViewTransform.localScale;
            playerViewTransform.localScale = new Vector2(Math.Abs(localScale.x) * directionX, localScale.y);
        }

        private void OnDrawGizmos()
        {
            var positionLeft = LeftRayStartPoint;
            var positionRight = RightRayStartPoint;
            Gizmos.DrawLine(positionLeft, positionLeft + Vector3.down * raycastDistance);
            Gizmos.DrawLine(positionRight, positionRight + Vector3.down * raycastDistance);
        }

        public void AfterLandedOnPlatform(PlatformBase platform)
        {
            gameController.ChangeColors();
            
            var main = landingAnimation.main;
            var lowerVelocityY = _previousVelocity.y / 5;
            main.startSpeedMultiplier = lowerVelocityY * lowerVelocityY / 4;
            main.startColor = platform.TargetColor;
            
            landingAnimation.Emit(Mathf.RoundToInt(-_previousVelocity.y * 2));
        }
    }
}
