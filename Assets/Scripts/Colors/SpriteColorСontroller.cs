using UnityEngine;

namespace Game.Colors
{
    public class SpriteColorController : ColorController
    {
        [SerializeField] private SpriteRenderer mainRenderer;
        [SerializeField] private SpriteRenderer shadowRenderer;
        private Color _targetShadowColor;

        protected override Color MainColor
        {
            get => mainRenderer.color;
            set => mainRenderer.color = value;
        }

        protected override void Awake()
        {
            base.Awake();
            _targetShadowColor = ShadowColor;
        }

        private Color ShadowColor
        {
            get => shadowRenderer.color;
            set => shadowRenderer.color = value;
        }

        public override void ChangeColor(Color color)
        {
            base.ChangeColor(color);
            var darkenColor = color * .8f;
            _targetShadowColor= darkenColor;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (ShadowColor != _targetShadowColor)
                ShadowColor = Color.Lerp(ShadowColor, _targetShadowColor, .15f);
        }
    }
}