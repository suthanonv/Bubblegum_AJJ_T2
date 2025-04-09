using UnityEngine;

public class Dropper : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int BouncePow;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void Drop()
    {
        rb.gravityScale = 1;
        rb.AddForce(Vector2.up * BouncePow);
    }
}
