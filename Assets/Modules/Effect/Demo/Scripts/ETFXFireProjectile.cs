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
            if (Input.GetKeyDown(KeyCode.H))
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

        // 玩家生成子弹方法1
        public void CreatePlayerProjectileWay1()
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.parent.right);
            if (hit) //Finds the point where you click with the mouse
            {
                projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
                projectile.name = "PlayerCreate";// 玩家生成标识
                projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
                projectile.transform.LookAt(hit.point);
                // projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
            }
        }

        // 玩家生成子弹方法2
        public void CreatePlayerProjectileWay2()
        {
            projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
            projectile.name = "PlayerCreate";// 玩家生成标识
            projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
        }

        // 玩家生成子弹方法3
        public void CreatePlayerProjectileWay3()
        {
            projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
            projectile.name = "PlayerCreate";// 玩家生成标识
            projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
            projectile.transform.forward = PlayerController.Instance.transform.right;
            // projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
        }

        // 敌人生成子弹方法
        public void CreateEnemyProjectile(Transform trans)
        {
            projectile = Instantiate(projectiles[enemyEffectIndex], trans.position, Quaternion.identity) as GameObject;
            projectile.name = "EnemyCreate";// 敌人生成标识
            projectile.GetComponent<Rigidbody>().useGravity = false; // 去除重力
            projectile.transform.LookAt(PlayerController.Instance.transform.position);
            // projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 500);
        }

        // 敌人子弹追踪
        public void TrackPlayer()
        {
            var direction = (PlayerController.Instance.transform.position - projectile.transform.position).normalized;
            if (direction != projectile.transform.forward)
            {
                projectile.transform.forward = direction;
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 500);
            }
        }

        // 玩家子弹追踪最近的敌人
        public void TrackEnemy()
        {
            var enemy = Physics2D.BoxCast(PlayerController.Instance.transform.position, PlayerController.Instance.size, 0.0f,
                 transform.right, 0.0f, LayerMask.GetMask("CanBeAttack"));
            if (enemy)
            {
                var enemyScript = enemy.collider.transform.parent.GetComponent<Enemy>();
                if (enemyScript && enemyScript.stateMachine.currentState != enemyScript.dead)
                {
                    var enemyOffset = new Vector3(enemy.collider.transform.position.x, enemy.collider.transform.position.y + 0.5f, 0);
                    var direction = (enemyOffset - projectile.transform.position).normalized;
                    if (direction != projectile.transform.forward)
                    {
                        projectile.transform.forward = direction;
                        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 300);
                    }
                }
                else
                {
                    projectile.transform.forward = PlayerController.Instance.transform.right;
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 300);
                }
            }
            else
            {
                projectile.transform.forward = PlayerController.Instance.transform.right;
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 300);
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