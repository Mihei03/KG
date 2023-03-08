using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;
    void Start()
    {
        _charController = GetComponent<CharacterController>(); // Доступ к другим компонентам, присоединенным к этому же объекту.
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed); // Ограничим движение по диагонали той же скоростью, что и движение параллельно осям.
        movement.y = gravity; // Используем значение переменной gravity вместо нуля
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement); // Преобразуем вектор движения от локальных к глобальным координатам.

        _charController.Move(movement); //Заставим этот вектор перемещать компонент CharacterController.
    }
}