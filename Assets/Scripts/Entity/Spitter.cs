using UnityEngine;
using UnityEngine.SceneManagement;

public class Spitter : MonoBehaviour, IEntity {
    // Basic status
    [SerializeField] Estate _state;
    public int      HealthPoint          ;
    public int      AttackPoint          ;
    public float    MoveSpeed          ;
    public float    RotateSpeed        ;
    public float    ViewDistance       ;
    public float    FireRate;

    [SerializeField] protected int _CurrentHP;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _FirePoint;

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
        HealthPoint = GameSettings.options.Spitter_HealthPoint;
        AttackPoint = GameSettings.options.Spitter_AttackPoint;
        MoveSpeed = GameSettings.options.Spitter_MoveSpeed;
        RotateSpeed = GameSettings.options.Spitter_RotateSpeed;
        ViewDistance = GameSettings.options.Spitter_ViewDistance;
        FireRate = GameSettings.options.Spitter_FireRate;

        _distanceToStop = ViewDistance * 0.8f;
        _fireInterval = 1 / FireRate;
    }

    [System.Obsolete]
    private void Start() {

        LocateTarget(0);
        _state = Estate.Wander;
    }

    [System.Obsolete]
    private void Update() {
        _fireTimer -= Time.deltaTime;
        AwareAgent();

        Quaternion targetRotation = new();
        if (_target != null)
        {
            // 修改點 1: 檢查 _target 是否為 null
            if (_state == Estate.TargetFound)
            {
                Shoot();
                // Rotate toward target
                Vector2 targetDirection = _target.position - transform.position;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90;
                targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            if (_state == Estate.Wander)
            {
                _wanderDirectionChangeTimer -= Time.deltaTime;
                if (_wanderDirectionChangeTimer <= 0)
                {
                    _wanderDirectionChangeTimer = _wanderDirectionChangeInterval;
                    ChangeWanderDirection();
                }
                targetRotation = Quaternion.LookRotation(Vector3.forward, _wanderDirection);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
        }
        else
        {
            // 修改點 2: 重新定位目標
            LocateTarget(0);
        }
    }

    private void Shoot() {
        // 修改點 3: 檢查 _target 是否為 null
        if (_target == null) return;
        float angle = Vector2.Angle(transform.up, _target.position - transform.position);

        if (_fireTimer < 0f && angle < 30) {
            _fireTimer = _fireInterval;
            Bullet bullet = Instantiate(_bulletPrefab, _FirePoint.position, _FirePoint.rotation);
            bullet.SetStatus(AttackPoint, 10, BulletType.Enemy);
        }
    }
    private void FixedUpdate() {
        // 進入射程不會再向Agent靠近
        // 修改點 4: 檢查 _target 是否為 null 並更新邏輯
        if ((_state == Estate.TargetFound && _target != null && Vector2.Distance(transform.position, _target.position) > _distanceToStop)
            || _state == Estate.Wander) {
            _rigidbody.AddForce(transform.up * MoveSpeed);
        }
    }

    /*
    protected void LocateTarget(int group) {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    */
    [System.Obsolete]
    protected void LocateTarget(int group)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "HumanGame")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                //Debug.Log("A");
                _target = player.transform;
            }
            else
            {
                //Debug.Log("B");
                _target = null;
            }
        }
        else
        {
            GameObject agent = GameObject.FindGameObjectWithTag("Agent");
            if (agent != null)
            {
                _target = agent.transform;
            }
            else
            {
                _target = null;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) {
            TurnBack();
        }
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
    private void TurnBack() {
        // Reverse direction by rotating 180 degrees
        transform.Rotate(0f, 0f, 180f);

        // Optionally change wander direction to avoid getting stuck
        ChangeWanderDirection();

        // Reset any forces applied during charge
        _rigidbody.velocity = Vector2.zero;
    }
}
