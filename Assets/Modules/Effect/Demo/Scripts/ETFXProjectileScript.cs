using UnityEngine;
using System.Collections;

public class ETFXProjectileScript : MonoBehaviour
{
    public GameObject impactParticle; // Effect spawned when projectile hits a collider
    public GameObject projectileParticle; // Effect attached to the gameobject as child
    public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned

    [Header("Adjust if not using Sphere Collider")]
    public float colliderRadius = 1f;

    [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
    public float collideOffset = 0.15f;

    private RaycastHit2D hit;

    private float startTime;
    private float maxAliveTime = 10.0f;

    private void Start()
    {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
        }
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= startTime + maxAliveTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            if (GetComponent<Rigidbody>().velocity == Vector3.zero) return;
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity); // Sets rotation to look at direction of movement
        }

        // RaycastHit hit;

        float radius; // Sets the radius of the collision detection
        if (transform.GetComponent<SphereCollider>())
            radius = transform.GetComponent<SphereCollider>().radius;
        else
            radius = colliderRadius;

        Vector3 direction = transform.GetComponent<Rigidbody>().velocity; // Gets the direction of the projectile, used for collision detection
        if (transform.GetComponent<Rigidbody>().useGravity)
            direction += Physics.gravity * Time.deltaTime; // Accounts for gravity if enabled
        direction = direction.normalized;

        float detectionDistance = transform.GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime; // Distance of collision detection for this frame

        // Physics.SphereCast(transform.position, radius, direction, out hit, detectionDistance)

        if (gameObject.name == "PlayerCreate")
        {
            hit = Physics2D.CircleCast(transform.position, radius, transform.right, detectionDistance, LayerMask.GetMask("Ground", "CanBeAttack"));
            if (hit && HitConditionByPlayer()) // Checks if collision will happen
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    Player.Instance.TransportAttackInfoToEnemy(hit, transform);
                }
                CreateEffect();
            }
        }
        else if (gameObject.name == "EnemyCreate")
        {
            hit = Physics2D.CircleCast(transform.position, radius, transform.right, detectionDistance, LayerMask.GetMask("Ground", "Player"));
            if (hit && HitConditionByEnemy())
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    AttackInfo attackInfo = new AttackInfo
                    {
                        damage = 10.0f,
                        damageSourcePosX = transform.position.x,
                    };
                    hit.transform.SendMessage("AcceptAttackDamage", attackInfo);
                }
                CreateEffect();
            }
        }
    }

    // 玩家发射 检测条件
    private bool HitConditionByPlayer()
    {
        // 墙壁
        if (hit.collider.transform.parent.gameObject.GetComponent<Enemy>() == null)
        {
            return true;
        }
        // 敌人
        else
        {
            var enemy = hit.collider.transform.parent.gameObject.GetComponent<Enemy>();
            return enemy.stateMachine.currentState != enemy.dead;
        }
    }

    // 敌人发射 检测条件
    private bool HitConditionByEnemy()
    {
        return Player.Instance.state.stateMachine.currentState != Player.Instance.state.dash;
    }

    // 生成特效
    private void CreateEffect()
    {
        transform.position = hit.point + (hit.normal * collideOffset); // Move projectile to point of collision

        GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject; // Spawns impact effect

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                                                                             //Component at [0] is that of the parent i.e. this object (if there is any)
        for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
        {
            ParticleSystem trail = trails[i];

            if (trail.gameObject.name.Contains("Trail"))
            {
                trail.transform.SetParent(null); // Detaches the trail from the projectile
                Destroy(trail.gameObject, 2f); // Removes the trail after seconds
            }
        }

        Destroy(projectileParticle, 3f); // Removes particle effect after delay
        Destroy(impactP, 3.5f); // Removes impact effect after delay
        Destroy(gameObject); // Removes the projectile
    }
}