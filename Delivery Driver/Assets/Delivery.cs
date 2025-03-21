using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay;
    bool hasPackage;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ouch");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Collecting Package

        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package Picked Up!");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }

        // Giving Package to Customer

        if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Customer Touched!");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }
    }
}
