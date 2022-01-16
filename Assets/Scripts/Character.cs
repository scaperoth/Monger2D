using UnityEngine;

public class Character : MonoBehaviour
{
    float _speed = 1;
    Vector3 _target;
    Transform _transform;
    Vector3 _flipper = new Vector3(-1, 1, 1);
    Vector3 _originalScale;
    [SerializeField]
    bool _targetSet = false;
    Animator _animator;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (_transform == null)
        {
            _transform = transform;
            _originalScale = _transform.localScale;
            _animator = GetComponent<Animator>();
        }
    }

    public void SetTargetTransform(Transform target)
    {
        Init();

        _target = new Vector3(target.position.x, _transform.position.y, _transform.position.z);

        if (_target.x > _transform.position.x)
        {
            _transform.localScale = Vector3.Scale(_originalScale, _flipper);
        }
        else
        {
            _transform.localScale = _originalScale;
        }

        Debug.Log($"Setting target transform to {_target}, {_targetSet}");

        _targetSet = true;
        Debug.Log($"new target state {_targetSet}");
    }

    private void Update()
    {
        if (!_targetSet)
        {
            Debug.Log("Target not set yet");
            return;
        }

        Debug.Log($"_transform.position {_transform.position}, _target {_target}");

        _animator.SetBool("Walking", true);
        _transform.position = Vector3.MoveTowards(_transform.position, _target, _speed * Time.deltaTime);
    }

}
