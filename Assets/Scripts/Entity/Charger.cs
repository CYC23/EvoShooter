
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Charger : MonoBehaviour, IEntity {
    // Basic status
    [SerializeField] Estate _state;
    public int HealthPoint;
    public int AttackPoint;
    public float MoveSpeed;
    public float RotateSpeed;
    public float ChargeSpeed;
    public float ViewDistance;
    public float FireRate;

    [SerializeField] protected int _CurrentHP;

    private Transform _target;
    private Rigidbody2D _rigidbody;

    private float _distanceToStop;
    private float _fireInterval;
    private float _fireTimer;
    // wander
    private Vector2 _wanderDirection;
    [SerializeField] private float _wanderDirectionChangeInterval = 6f;
    private float _wanderDirectionChangeTimer;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _CurrentHP = HealthPoint;
        HealthPoint = GameSettings.options.Charger_HealthPoint;
        AttackPoint = GameSettings.options.Charger_AttackPoint;
        MoveSpeed = GameSettings.options.Charger_MoveSpeed;
        RotateSpeed = GameSettings.options.Charger_RotateSpeed;
        ChargeSpeed = GameSettings.options.Charger_ChargeSpeed;
        ViewDistance = GameSettings.options.Charger_ViewDistance;
        FireRate = GameSettings.options.Charger_FireRate;

        _distanceToStop = ViewDistance * 0.8f;
        _fireInterval = 1 / FireRate;
    }
    private void Start() {

        LocateTarget(0);
        _state = Estate.Wander;
    }
    private void Update() {
        _fireTimer -= Time.deltaTime;
        AwareAgent();

        Quaternion targetRotation = new();
        if (_target != null && _state == Estate.TargetFound) {
            Charge();
            // Rotate toward target
            Vector2 targetDirection = _target.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90;
            targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        if (_state == Estate.Wander) {
            _wanderDirectionChangeTimer -= Time.deltaTime;
            if (_wanderDirectionChangeTimer <= 0) {
                _wanderDirectionChangeTimer = _wanderDirectionChangeInterval;
                ChangeWanderDirection();
            }
            targetRotation = Quaternion.LookRotation(Vector3.forward, _wanderDirection);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
    }

    private void Charge() {

        float angle = Vector2.Angle(transform.up, _target.position - transform.position);

        if (_fireTimer < 0f && angle < 10) {
            _fireTimer = _fireInterval;
            _rigidbody.AddForce(transform.up * ChargeSpeed, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            IEntity player = other.gameObject.GetComponent<IEntity>();
            player.TakeDamage(AttackPoint);
            player.KnockBack(transform.up, 16);
        }
        else if (other.gameObject.CompareTag("Wall")) {
            TurnBack();
        }
    }
    private void FixedUpdate() {
        // 進入射程不會再向Agent靠近
        if ((_state == Estate.TargetFound && Vector2.Distance(transform.position, _target.position) > _distanceToStop)
            || _state == Estate.Wander) {
            _rigidbody.AddForce(transform.up * MoveSpeed);
        }
    }
    protected void LocateTarget(int group) {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int amount) {
        _CurrentHP -= amount;
        if (_CurrentHP < 0) {
            Destroy(gameObject);
            int score = GameSettings.options.Score_Spitter;
            HumanPlaySceneManager.manager.IncreaseScore(score);
        }
    }

    public void TakeHeal(int amount) {
        _CurrentHP += amount;
        if (_CurrentHP > HealthPoint) {
            _CurrentHP = HealthPoint;
        }
    }

    public void KnockBack(Vector2 direction, float strength) {
        _rigidbody.AddForce(direction * strength, ForceMode2D.Impulse);
    }
    private void TurnBack() {
        // Reverse direction by rotating 180 degrees
        transform.Rotate(0f, 0f, 180f);

        // Optionally change wander direction to avoid getting stuck
        ChangeWanderDirection();

        // Reset any forces applied during charge
        _rigidbody.velocity = Vector2.zero;
    }
    protected void AwareAgent() {
        if (_target != null && Vector2.Distance(transform.position, _target.position) < ViewDistance) {
            _state = Estate.TargetFound;
        }
        else {
            _state = Estate.Wander;
        }
    }
    protected void ChangeWanderDirection() {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        _wanderDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
