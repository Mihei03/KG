using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public const float baseSpeed = 6.0f;
    private CharacterController _charController;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>(); // ������ � ������ �����������, �������������� � ����� �� �������.
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed); // ��������� �������� �� ��������� ��� �� ���������, ��� � �������� ����������� ����.
        movement.y = gravity; // ���������� �������� ���������� gravity ������ ����
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement); // ����������� ������ �������� �� ��������� � ���������� �����������.

        _charController.Move(movement); //�������� ���� ������ ���������� ��������� CharacterController.
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }
}