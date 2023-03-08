using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0; //Объявляем закрытую переменную для угла поворота по вертикали

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) // Проверяем, существует ли этот компонент.
            body.freezeRotation = true;
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert; // Увеличиваем угол поворота по вертикали в соответствии с перемещениями указателя мыши.

            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); //Фиксируем угол поворота по вертикали в диапазоне, заданном минимальным и максимальным значениями.

            float rotationY = transform.localEulerAngles.y; //  Сохраняем одинаковый угол поворота вокруг оси Y (т. е. вращение в горизонтальной плоскости отсутствует).

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // Создаем новый вектор из сохраненных значений поворота.
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor; // Приращение угла поворота через значение delta.

            float rotationY = transform.localEulerAngles.y + delta; // Значение delta — это величина изменения угла поворота.

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}

