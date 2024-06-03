using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Agent : MonoBehaviour , IEntity
{
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

    private Vector2 _wanderDirection;
    [SerializeField] private float _wanderDirectionChangeInterval = 6f;
    private float _wanderDirectionChangeTimer;

    private float _distanceToStop;
    private Transform _target;
    private float _fireTimer;
    private float _fireInterval;
    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        gHealthPoint = GameSettings.options.Agent_R_HealthPoint;
        gAttackPoint = GameSettings.options.Agent_R_AttackPoint;
        gMoveSpeed = GameSettings.options.Agent_R_MoveSpeed;
        gRotateSpeed = GameSettings.options.Agent_R_RotateSpeed;
        gFireRate = GameSettings.options.Agent_R_FireRate;
        gBulletSpeed = GameSettings.options.Agent_R_BulletSpeed;
        gMagazineSize = GameSettings.options.Agent_R_MagazineSize;
        gViewDistance = GameSettings.options.Agent_R_ViewDistance;

        _rigidbody = GetComponent<Rigidbody2D>();
        _distanceToStop = gViewDistance * 0.8f;
        _fireInterval = 1 / gFireRate;
        _CurrentHP = gHealthPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        gHealthPoint = GameSettings.options.Agent_R_HealthPoint;
        gAttackPoint = GameSettings.options.Agent_R_AttackPoint;
        gMoveSpeed = GameSettings.options.Agent_R_MoveSpeed;
        gRotateSpeed = GameSettings.options.Agent_R_RotateSpeed;
        gFireRate = GameSettings.options.Agent_R_FireRate;
        gBulletSpeed = GameSettings.options.Agent_R_BulletSpeed;
        gMagazineSize = GameSettings.options.Agent_R_MagazineSize;
        gViewDistance = GameSettings.options.Agent_R_ViewDistance;

        _rigidbody = GetComponent<Rigidbody2D>();
        _distanceToStop = gViewDistance * 0.8f;
        _fireInterval = 1 / gFireRate;
        _CurrentHP = gHealthPoint;

        LocateTarget(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        _fireTimer -= Time.deltaTime;


        if (_target != null)
        {
            Shoot();
            // Rotate toward target
            Vector2 targetDirection = _target.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, gRotateSpeed * Time.deltaTime);


        }
        else
        {
            LocateTarget(0);
            _wanderDirectionChangeTimer -= Time.deltaTime;
            if (_wanderDirectionChangeTimer <= 0)
            {
                _wanderDirectionChangeTimer = _wanderDirectionChangeInterval;
                ChangeWanderDirection();
            }

        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.up * gMoveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
         if (other.gameObject.CompareTag("Wall"))
        {
            TurnBack();
        }
    }




    private void Shoot()
    {

        if (_fireTimer < 0f)
        {
            _fireTimer = _fireInterval;
            Bullet bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
            bullet.SetStatus(gAttackPoint, gBulletSpeed);
        }
    }

    public void TakeDamage(int amount)
    {
        _CurrentHP -= amount;
        if (_CurrentHP < 0)
        {
            Destroy(gameObject);
            AgentPlaySceneManager.manager.GameOver();
        }
    }
    public void TakeHeal(int amount)
    {
        _CurrentHP += amount;
        if (_CurrentHP > gHealthPoint)
        {
            _CurrentHP = gHealthPoint;
        }
    }


    public void KnockBack(Vector2 direction, float strength)
    {
        _rigidbody.AddForce(direction * strength, ForceMode2D.Impulse);
    }

    protected void LocateTarget(int group)
    {
        // 获取当前Agent的位置
        Vector2 currentPosition = transform.position;

        // 在gViewDistance范围内找到所有具有"Spitter", "Tank", "Charger", "Zombie"标签的对象
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentPosition, gViewDistance);
        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            // 获取当前碰撞体的游戏对象
            GameObject obj = collider.gameObject;

            // 排除自身的碰撞体
            if (obj == this.gameObject)
            {
                continue;
            }

            string tag = obj.tag;
            Debug.Log(tag);
            // 检查标签是否是我们感兴趣的标签
            if (tag == "Enemy" )
            {
                Debug.Log("get it");
                // 计算当前对象和Agent之间的平方距离
                Vector2 directionToTarget = (Vector2)obj.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                // 如果当前对象距离更近，则更新最近的目标
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestTarget = obj;
                }
            }
        }

        // 设置_target为找到的最近目标的transform
        if (closestTarget != null)
        {
            _target = closestTarget.transform;
        }
        else
        {
            _target = null;
        }
    }


    protected void ChangeWanderDirection()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        _wanderDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void TurnBack()
    {
        // Reverse direction by rotating 180 degrees
        transform.Rotate(0f, 0f, 180f);

        // Optionally change wander direction to avoid getting stuck
        ChangeWanderDirection();

        // Reset any forces applied during charge
        _rigidbody.velocity = Vector2.zero;
    }
}
