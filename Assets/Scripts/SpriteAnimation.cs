using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private int _currentSpriteIndex;
        private float _NextFrameTime;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondsPerFrame = 1f / _frameRate;
            _NextFrameTime = Time.time + _secondsPerFrame;
            _currentSpriteIndex = 0;
        }

        private void Update()
        {
            if (_NextFrameTime > Time.time) return;

            if(_currentSpriteIndex >= _sprites.Length)
            {
                if (_loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    enabled = false;
                    _onComplete?.Invoke();
                    return; 
                }
            }

            _renderer.sprite = _sprites[_currentSpriteIndex];
            _NextFrameTime += _secondsPerFrame;
            _currentSpriteIndex++;
        }
    }
}