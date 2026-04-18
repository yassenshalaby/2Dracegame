using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private float[] _laneX = { -2.5f, 0f, 2.5f };
    private int _currentLane = 1;
    public float laneSpeed = 10f;

    private bool _isImmune;
    private float _immuneTimer;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        Vector3 target = new Vector3(_laneX[_currentLane], transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * laneSpeed);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2f)
                    MoveLeft();
                else
                    MoveRight();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();

        if (_isImmune)
        {
            _immuneTimer -= Time.deltaTime;
            if (_immuneTimer <= 0f)
                _isImmune = false;
        }
    }

    void MoveLeft()
    {
        if (_currentLane > 0) _currentLane--;
    }

    void MoveRight()
    {
        if (_currentLane < _laneX.Length - 1) _currentLane++;
    }

    public void ActivateImmunity()
    {
        _isImmune = true;
        _immuneTimer = 5f;
    }

    public bool IsImmune() { return _isImmune; }
}