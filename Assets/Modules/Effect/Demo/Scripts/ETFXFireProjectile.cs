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

        private GameObject projectile;
        public int enemyEffectIndex = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (projectile && projectile.name == "EnemyCreate")
            {
                TrackPlayer();
            }
            if (projectile && projectile.name == "PlayerCreate")
            {
                TrackEnemy();
            }
        }

        // 玩家生成子弹方法
        public void CreatePlayerProjectileWay()
        {
            projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity);
            projectile.name = "PlayerCreate";
            projectile.GetComponent<Rigidbody>().useGravity = false;
        }

        // 敌人生成子弹方法
        public void CreateEnemyProjectile(Transform trans)
        {
            projectile = Instantiate(projectiles[enemyEffectIndex], trans.position, Quaternion.identity);
            projectile.name = "EnemyCreate";
            projectile.GetComponent<Rigidbody>().useGravity = false;
        }

        // 敌人子弹追踪
        public void TrackPlayer()
        {
            projectile.transform.LookAt(Player.Instance.transform.position);
            JudgePlatformForRb();
        }

        // 玩家子弹追踪最近的敌人 移动端500 pc端50
        public void TrackEnemy()
        {
            var enemy = Physics2D.BoxCast(Player.Instance.transform.position, Player.Instance.state.playerData.fireSize, 0.0f,
                 transform.right, 0.0f, LayerMask.GetMask("CanBeAttack"));
            if (enemy && enemy.collider.transform.parent.GetComponent<Enemy>().stateMachine.currentState != enemy.collider.transform.parent.GetComponent<Enemy>().dead)
            {
                var enemyPosOffset = new Vector2(enemy.collider.transform.position.x, enemy.collider.transform.position.y + enemy.collider.bounds.size.y / 2);
                projectile.transform.LookAt(enemyPosOffset);
            }
            else
            {
                projectile.transform.LookAt(Player.Instance.transform.right);
            }
            JudgePlatformForRb();
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