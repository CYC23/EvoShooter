using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IEntity {

    public int gHealthPoint = 100;
    public int gAttackPoint = 10;
    public float gMoveSpeed = 15;
    public float gRotateSpeed = 60f;
    public float gFireRate = 2;
    public float gBulletSpeed = 10;
    public int gMagazineSize = 10;
    public float gViewDistance = 10;

    public int _CurrentHP;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform gunBarrel;

    private Vector2 _moveInput;
    private Vector3 _mousePosition;

    private float _fireTimer;
    private float _fireInterval;
    private Rigidbody2D _rigidbody;

    private string currentSceneName;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _fireInterval = 1 / gFireRate;
        _CurrentHP = gHealthPoint;
    }
    private void Update() {

        _fireTimer -= Time.deltaTime;
        Move();
        Steer();
        Shoot();
    }
    private void FixedUpdate() {
        _rigidbody.AddForce(gMoveSpeed * _moveInput);
    }
    private void Move() {

        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    private void Steer() {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = 0;
        float angle = Mathf.Atan2(_mousePosition.y - transform.position.y, _mousePosition.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, gRotateSpeed * Time.deltaTime);
    }
    private void Shoot() {

        if (Input.GetMouseButton(0) && _fireTimer < 0f) {
            _fireTimer = _fireInterval;
            Bullet bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
            bullet.SetStatus(gAttackPoint, gBulletSpeed);
        }
    }

    public void TakeDamage(int amount) {
        _CurrentHP -= amount;
        if (_CurrentHP < 0) {
            Destroy(gameObject);
            _CurrentHP = 0;
            HumanPlaySceneManager.manager.GameOver();
        }
    }
    public void TakeHeal(int amount) {
        _CurrentHP += amount;
        if (_CurrentHP > gHealthPoint) {
            _CurrentHP = gHealthPoint;
        }
    }


    public void KnockBack(Vector2 direction, float strength) {
        _rigidbody.AddForce(direction * strength, ForceMode2D.Impulse);
    }
}
