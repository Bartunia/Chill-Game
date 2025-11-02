using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMovement : MonoBehaviour
{
    [Header("Lot / Skok")]
    [SerializeField] private float flapForce = 5.5f;
    [SerializeField] private float maxFallSpeed = -7.5f;

    [Header("Pochylenie")]
    [SerializeField] private float tiltUpAngle = 35f;
    [SerializeField] private float tiltDownAngle = -80f;
    [SerializeField] private float tiltSmooth = 8f;

    [Header("Wejście")]
    [SerializeField] private KeyCode flapKey = KeyCode.Space;

    private Rigidbody2D rb;
    private bool isAlive = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Jeśli dostajesz ostrzeżenie na freezeRotation,
        // możesz użyć Constraints:
        // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.freezeRotation = true;
    }

    void OnEnable()
    {
        if (rb != null) rb.linearVelocity = Vector2.zero; // <-- zamiast velocity
        isAlive = true;
    }

    void Update()
    {
        if (!isAlive) return;

        if (FlapPressed())
        {
            // Wyzeruj pionową składową, żeby każdy flap był „równy”
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);    // <-- zamiast velocity
            rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
        }

        // Ogranicz prędkość spadania
        if (rb.linearVelocity.y < maxFallSpeed)                           // <-- zamiast velocity
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }

        // Obrót zależny od prędkości Y
        float t = Mathf.InverseLerp(maxFallSpeed, 6f, rb.linearVelocity.y);
        float targetZ = Mathf.Lerp(tiltDownAngle, tiltUpAngle, t);
        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, tiltSmooth * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isAlive) return;
        isAlive = false;
        // obsługa śmierci / pauza / event do GameManagera
    }

    private bool FlapPressed()
    {
        if (Input.GetKeyDown(flapKey) || Input.GetMouseButtonDown(0)) return true;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) return true;
        return false;
    }
}
