using System;
using UnityEngine;

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

    private bool isAlive = true;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
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


    /*void Update()
    {
        if (!isAlive) return;

        if (FlapPressed())
        {
            // Wyzeruj pionową składową, żeby każdy flap był „równy”
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
        }

        // Ogranicz prędkość spadania
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }

        // Obrót zależny od prędkości Y
        float t = Mathf.InverseLerp(maxFallSpeed, 6f, rb.linearVelocity.y);
        float targetZ = Mathf.Lerp(tiltDownAngle, tiltUpAngle, t);
        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, tiltSmooth * Time.deltaTime);
    }*/

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isAlive) return;
        isAlive = false;
        // obsługa śmierci / pauza / event do GameManagera
    }

    private bool FlapPressed()
    {
        if (Input.GetKeyDown(flapKey) || Input.GetMouseButtonDown(0)) return true;
        return false;
    }
}
