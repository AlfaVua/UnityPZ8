using UnityEngine;

namespace Game.Colors
{
    public class SpriteColorController : MonoBehaviour
    {
        private Color _targetColor;
        private Color _targetShadowColor;
        [SerializeField] private SpriteRenderer mainRenderer;
        [SerializeField] private SpriteRenderer shadowRenderer;

        private void Awake()
        {
            _targetColor = mainRenderer.color;
            _targetShadowColor = shadowRenderer.color;
        }

        public void ChangeColor(Color color)
        {
            _targetColor = color;
            var darkenColor = color * .8f;
            _targetShadowColor= darkenColor;
        }

        private void FixedUpdate()
        {
            // Так, я дуже люблю Lerp, поки до анімацій не дійшли лекції :D
            if (mainRenderer.color != _targetColor)
                mainRenderer.color = Color.Lerp(mainRenderer.color, _targetColor, .15f);
            
            if (shadowRenderer.color != _targetShadowColor)
                shadowRenderer.color = Color.Lerp(shadowRenderer.color, _targetShadowColor, .15f);
        }
    }
}