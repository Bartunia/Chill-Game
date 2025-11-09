using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace N_Flappy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BirdMovement : MonoBehaviour
    {
        [Header("Wejście")]
        [SerializeField] private KeyCode flapKey = KeyCode.Space;
    
        private SpriteRenderer _spriteRenderer;
        public Sprite[] sprites;
        private int _spriteIndex;
        private Vector3 _direction;

        public float gravity = -9.8f;
        public float strength = 5f;

        private bool _isAlive = true;
        [SerializeField] public GameManager gameManager;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
       
        }

        private void Start()
        {
            InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        }

        private void OnEnable()
        {
            var transform1 = transform;
            Vector3 position = transform1.position;
            position.y = 0f;
            transform1.position = position;
            _direction = Vector3.zero;
        }

        private void Update()
        {
            if (FlapPressed())
            {
                _direction = Vector3.up * strength;
            }

            _direction.y += gravity * Time.deltaTime;
            transform.position += _direction * Time.deltaTime;

        }

        private void AnimateSprite()
        {
            _spriteIndex++;
            if (_spriteIndex >= sprites.Length)
            {
                _spriteIndex = 0;
            }

            _spriteRenderer.sprite = sprites[_spriteIndex];
        }


        [Obsolete("Obsolete")]
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                gameManager.GameOver();
            }
            else if (other.gameObject.CompareTag("Scoring"))
            {
                gameManager.IncreaseScore();
            }
        }

        private bool FlapPressed()
        {
            if (Input.GetKeyDown(flapKey) || Input.GetMouseButtonDown(0)) return true;
            return false;
        }
    }
}
