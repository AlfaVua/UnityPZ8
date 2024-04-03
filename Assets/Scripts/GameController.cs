using Game.Colors;
using Game.Player;
using UnityEngine;

namespace Game.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform platformContainer;
        [SerializeField] private PlayerScript playerScript;
        [SerializeField] private Transform movementBounds;
        [SerializeField] private Transform mainCameraTransform;
        [SerializeField] private SpriteColorController[] colorControllers;

        void FixedUpdate()
        {
            var playerPosition = playerScript.transform.position;
            var cameraPosition = mainCameraTransform.position;
            var cameraTargetY = Mathf.Lerp(cameraPosition.y, playerPosition.y + 2.5f, .3f);

            movementBounds.position = Vector3.up * playerPosition.y;
            mainCameraTransform.position = new Vector3(cameraPosition.x, cameraTargetY, cameraPosition.z);
        }

        public void OnPlayerJumped()
        {
            foreach (var colorController in colorControllers)
            {
                Color color = Random.ColorHSV(0, 1, .2f, 1, .4f, 1);
                colorController.ChangeColor(color);
            }
        }
    }
}
