using Assets.Code.Player;
using UnityEngine;

namespace Assets.Code.Background
{
    public class BackgroundComponent : MonoBehaviour
    {
        public Vector3 Offset;
        public float ParallaxMultiplier;

        private PlayerComponent _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerComponent>();
        }

        private void Update()
        {
            transform.position = Offset + _player.transform.position * -ParallaxMultiplier;
        }
    }
}
