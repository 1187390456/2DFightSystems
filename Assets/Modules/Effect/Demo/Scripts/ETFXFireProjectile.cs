using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EpicToonFX
{
    public class ETFXFireProjectile : MonoBehaviour
    {
        public static ETFXFireProjectile Instance { get; private set; }
        public GameObject[] projectiles;
        public Transform spawnPosition;
        public int currentProjectile = 0;
        public float speed = 1000;

        // private RaycastHit hit;

        private GameObject projectile;
        public int enemyEffectIndex = 0; // 特效索引
        private bool isMouseCreate = false;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                previousEffect();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                previousEffect();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()) //On left mouse down-click
            {
                CreatePlayerProjectileWay1();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                CreatePlayerProjectileWay2();
            }
            Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);
            if (projectile && projectile.name == "EnemyCreate")
            {
                TrackPlayer();
            }
            if (projectile && projectile.name == "PlayerCreate")
            {
                TrackEnemy();
            }
        }

        // 玩家生成子弹方法1 鼠标点击
        public void CreatePlayerProjectileWay1()
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.parent.right);
            if (hit)
            {
                projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
                projectile.name = "PlayerCreate";// 玩家生成标识
                isMouseCreate = true;
                projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
                projectile.transform.LookAt(hit.point);
            }
        }

        // 玩家生成子弹方法2
        public void CreatePlayerProjectileWay2()
        {
            projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
            projectile.name = "PlayerCreate";// 玩家生成标识
            isMouseCreate = false;
            projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
        }

        // 敌人生成子弹方法
        public void CreateEnemyProjectile(Transform trans)
        {
            projectile = Instantiate(projectiles[enemyEffectIndex], trans.position, Quaternion.identity) as GameObject;
            projectile.name = "EnemyCreate";// 敌人生成标识
            projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
        }

        // 敌人子弹追踪
        public void TrackPlayer()
        {
            projectile.transform.LookAt(PlayerController.Instance.transform.position);
            JudgePlatformForRb();
        }

        // 玩家子弹追踪最近的敌人 移动端500 pc端50
        public void TrackEnemy()
        {
            var enemy = Physics2D.BoxCast(PlayerController.Instance.transform.position, PlayerController.Instance.size, 0.0f,
                 transform.right, 0.0f, LayerMask.GetMask("CanBeAttack"));
            if (enemy && enemy.collider.transform.parent.GetComponent<Enemy>().stateMachine.currentState != enemy.collider.transform.parent.GetComponent<Enemy>().dead)
            {
                var enemyPosOffset = new Vector2(enemy.collider.transform.position.x, enemy.collider.transform.position.y + enemy.collider.bounds.size.y / 2);
                projectile.transform.LookAt(enemyPosOffset);
                JudgePlatformForRb();
            }
            else if (!isMouseCreate)
            {
                projectile.transform.forward = Vector2.up;
                JudgePlatformForRb();
            }
            else
            {
                JudgePlatformForRb();
            }
        }

        // 根据平台施加
        private void JudgePlatformForRb()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 500);
            }
            else
            {
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 50);
            }
        }

        public void nextEffect() //Changes the selected projectile to the next. Used by UI
        {
            if (currentProjectile < projectiles.Length - 1)
                currentProjectile++;
            else
                currentProjectile = 0;
        }

        public void previousEffect() //Changes selected projectile to the previous. Used by UI
        {
            if (currentProjectile > 0)
                currentProjectile--;
            else
                currentProjectile = projectiles.Length - 1;
        }

        public void AdjustSpeed(float newSpeed) //Used by UI to set projectile speed
        {
            speed = newSpeed;
        }

        // 敌人切换特效
        public void SwitchEnemyAttackEffect()
        {
            enemyEffectIndex = Random.Range(0, projectiles.Length);
        }
    }
}