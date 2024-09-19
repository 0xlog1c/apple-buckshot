using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GunRecoilJump : MonoBehaviour {
    Rigidbody2D _rb;  

    [Header("Gun Transfrom Configuration")]
    [SerializeField] Transform gunTransform;
    [SerializeField] Transform gunBarrel;

    [Header("Fire Settings")]
    [SerializeField] float recoilForce = 10f;
    [SerializeField] float fireCooldown = 1f;
    [SerializeField] AudioSource shootEffect;
    [SerializeField] AudioSource emptyChamberEffect;
    float timeSinceLastShot = 0f;

    [Header("Bullet Apple Settings")]
    [SerializeField] GameObject bulletApplePrefab;        
    [SerializeField] int initialBulletApples;  
    [SerializeField] float bulletAppleSpeed = 20f;      
    [SerializeField] TextMeshProUGUI amountOfApplesText;
    [SerializeField] AudioSource biteEffect;
    int numberOfApples;

    int count = 0;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        numberOfApples = initialBulletApples;
    }

    void Update() {
        amountOfApplesText.text = numberOfApples.ToString();

        AimGun();

        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timeSinceLastShot >= fireCooldown) {
            FireGun();
            timeSinceLastShot = 0f;
        }
        
        if (numberOfApples == 0) {
            GameManager.Instance.ResetGame();
        }
    }

    void AimGun() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)gunTransform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FireGun() {
        if (numberOfApples > 0) {
            GameManager.Instance.SetTimeElapsed(0);

            shootEffect.Play();
            
            Vector2 fireDirection = gunTransform.right;

            _rb.AddForce(-fireDirection * recoilForce, ForceMode2D.Impulse);

            GameObject bullet = Instantiate(bulletApplePrefab, gunBarrel.position, gunTransform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = fireDirection * bulletAppleSpeed;

            numberOfApples--;

            Destroy(bullet, 2f);
        } else {
            emptyChamberEffect.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        biteEffect.Play();
        numberOfApples++;
        count++;
        Destroy(other.gameObject);
    }

    public int GetNumberOfApplesCollected() {
        return count;
    }
}
