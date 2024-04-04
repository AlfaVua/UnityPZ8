using UnityEngine;

namespace Game.Colors
{
    public class CameraColorController : ColorController
    {
        [SerializeField] private Camera mainCamera;


        protected override Color MainColor
        {
            get => mainCamera.backgroundColor;
            set => mainCamera.backgroundColor = value;
        }
    }
}
