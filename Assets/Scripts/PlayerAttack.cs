using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject fireballPrefab; 
    public float fireballSpeed = 10f; 
    public float maxDistance = 10f; // Set your desired maximum distance
    [SerializeField] private Transform firePoint;
    

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("attack");

            float directionMultiplier = spriteRenderer.flipX ? -1f : 1f;
            firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x) * directionMultiplier, firePoint.localPosition.y, firePoint.localPosition.z);

            GameObject fireballInstance = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

            if (spriteRenderer.flipX)
            {
                fireballInstance.transform.localScale = new Vector3(-1 * Mathf.Abs(fireballInstance.transform.localScale.x),
                                                                    fireballInstance.transform.localScale.y,
                                                                    fireballInstance.transform.localScale.z);
            }

            Vector2 fireDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

            Rigidbody2D rb = fireballInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = fireDirection * fireballSpeed;

                // Destroy the fireball after it travels a certain distance
                Destroy(fireballInstance, maxDistance / fireballSpeed);
            }
        }
    }
}
