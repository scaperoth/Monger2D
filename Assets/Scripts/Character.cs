using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    GameSettings _gameSettings;

    Vector3 _target;
    Transform _transform;
    Vector3 _flipper = new Vector3(-1, 1, 1);
    Vector3 _originalScale;
    [SerializeField]
    bool _targetSet = false;
    Animator _animator;

    private void Start()
    {
        SetDefaults();
    }

    private void SetDefaults()
    {
        if (_transform == null)
        {
            _transform = transform;
            _originalScale = _transform.localScale;
            _animator = GetComponent<Animator>();
        }
    }

    public void Init(Transform target, int layer)
    {
        SetDefaults();
        gameObject.layer = layer;
        SetTargetTransform(target);
    }

    public void SetTargetTransform(Transform target)
    {
        _target = new Vector3(target.position.x, target.position.y, _transform.position.z);

        if (_target.x > _transform.position.x)
        {
            _transform.localScale = Vector3.Scale(_originalScale, _flipper);
        }
        else
        {
            _transform.localScale = _originalScale;
        }

        _targetSet = true;
    }

    private void Update()
    {
        if (!_targetSet)
        {
            return;
        }

        _animator.SetBool("Walking", true);
        _transform.position = Vector3.MoveTowards(_transform.position, _target, _gameSettings.characterSpeed * Time.deltaTime);
    }

    public void Die()
    {
        _targetSet = false;
        _animator.SetBool("Walking", false);
        _animator.SetTrigger("Dead");
    }
}
