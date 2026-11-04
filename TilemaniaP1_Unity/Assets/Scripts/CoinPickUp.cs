using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;

    bool isCollected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            AudioSource.PlayClipAtPoint(coinPickUpSFX, transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
