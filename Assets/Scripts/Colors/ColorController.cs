using UnityEngine;

namespace Game.Colors
{
    public abstract class ColorController : MonoBehaviour
    {
        private Color _targetColor;
        protected virtual Color MainColor { get; set; }

        protected virtual void Awake()
        {
            _targetColor = MainColor;
        }

        public virtual void ChangeColor(Color color)
        {
            _targetColor = color;
        }

        protected virtual void FixedUpdate()
        {
            // Так, я дуже люблю Lerp, поки до анімацій не дійшли лекції :D
            if (MainColor != _targetColor)
                MainColor = Color.Lerp(MainColor, _targetColor, .15f);
        }
    }
}