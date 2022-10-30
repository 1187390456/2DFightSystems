using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] [Header("物体堆栈信息")] private ItemStack _itemStack;
        [SerializeField] [Header("当前精灵渲染")] private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _colliderEnableAfter = 1.0f;
        [SerializeField] private float _throwGravity = 2.0f;
        [SerializeField] private float _maxForce = 5.0f;
        [SerializeField] private float _minForce = 3.0f;
        [SerializeField] private float _throwYForce = 5.0f;

        public ItemStack ItemStack
        {
            get => _itemStack;
            set => _itemStack = value;
        }

        private Collider2D _collider;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _rb = GetComponent<Rigidbody2D>();
            _collider.enabled = false;
        }

        private void Start()
        {
            SetupGameObject(); // 同步设置
            StartCoroutine(EnableCollider(_colliderEnableAfter));
        }

        private void OnValidate()
        {
            SetupGameObject();
        }

        public IEnumerator EnableCollider(float afterTime)
        {
            yield return new WaitForSeconds(afterTime);
            _collider.enabled = true;
        }

        private void SetupGameObject()
        {
            if (_itemStack == null) return;

            _spriteRenderer.sprite = _itemStack.ItemDefinition.GameSprite;

            _itemStack.NumberOfItems = _itemStack.NumberOfItems;

            var name = _itemStack.ItemDefinition.Name;
            var number = _itemStack.CanStack ? _itemStack.NumberOfItems.ToString() : "not allow stack";
            gameObject.name = $"{name}({number})";
        }

        public ItemStack Pick()
        {
            Destroy(gameObject);
            return _itemStack;
        }

        public void Throw(float xDir)
        {
            _rb.gravityScale = _throwGravity;
            var throwXForce = Random.Range(_minForce, _maxForce);
            _rb.velocity = new Vector2(Mathf.Sign(xDir) * throwXForce, _throwYForce);
            StartCoroutine(DisableGravity(_throwYForce));
        }

        private IEnumerator DisableGravity(float atYVelocity)
        {
            yield return new WaitUntil(() => _rb.velocity.y < -atYVelocity);
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0;
        }
    }
}