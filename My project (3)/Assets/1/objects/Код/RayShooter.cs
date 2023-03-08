using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>(); //Доступ к другим компонентам, присоединенным к этому же объекту.

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Скрываем указатель мыши в центре экрана.
    }

    void OnGUI()
    {
        GUI.color = Color.red;
        int size = 10; // 100 для Label, иначе просто точка, ни больше, ни меньше, а для Button и 12 норм.
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Button(new Rect(posX, posY, size, size), "что-то"); // Команда GUI.Label() отображает на экране символ.
        // Заменил на Button, т.к. красивее...
        //GUI.Label(new Rect(posX, posY, size, size), "+");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Реакция на нажатие кнопки мыши.
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0); // Середина экрана — это половина его ширины и высоты.

            Ray ray = _camera.ScreenPointToRay(point);// Создание в этой точке луча методом ScreenPointToRay(). 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Испущенный луч заполняет информацией переменную, на которую имеется ссылка.
            {
                Debug.Log("Hit " + hit.point); // Загружаем координаты точки, в которую попал луч. 
                //Оставлю для проверки работоспособности...

                GameObject hitObject = hit.transform.gameObject; // Получаем объект, в который попал луч.
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>(); // Проверка, есть ли скрипт ReactiveTarget. 
                if (target != null) // Проверяем наличие у этого объекта компонента ReactiveTarget.
                    target.ReactToHit(); //Вызов метода для мишени.
                else
                    StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1); // Ключевое слово yield указывает сопрограмме, когда следует остановиться.

        Destroy(sphere); // Удаляем этот GameObject и очищаем память.
    }
}

