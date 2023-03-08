using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>(); //������ � ������ �����������, �������������� � ����� �� �������.

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // �������� ��������� ���� � ������ ������.
    }

    void OnGUI()
    {
        GUI.color = Color.red;
        int size = 10; // 100 ��� Label, ����� ������ �����, �� ������, �� ������, � ��� Button � 12 ����.
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Button(new Rect(posX, posY, size, size), "���-��"); // ������� GUI.Label() ���������� �� ������ ������.
        // ������� �� Button, �.�. ��������...
        //GUI.Label(new Rect(posX, posY, size, size), "+");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ������� �� ������� ������ ����.
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0); // �������� ������ � ��� �������� ��� ������ � ������.

            Ray ray = _camera.ScreenPointToRay(point);// �������� � ���� ����� ���� ������� ScreenPointToRay(). 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // ���������� ��� ��������� ����������� ����������, �� ������� ������� ������.
            {
                Debug.Log("Hit " + hit.point); // ��������� ���������� �����, � ������� ����� ���. 
                //������� ��� �������� �����������������...

                GameObject hitObject = hit.transform.gameObject; // �������� ������, � ������� ����� ���.
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>(); // ��������, ���� �� ������ ReactiveTarget. 
                if (target != null) // ��������� ������� � ����� ������� ���������� ReactiveTarget.
                    target.ReactToHit(); //����� ������ ��� ������.
                else
                    StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1); // �������� ����� yield ��������� �����������, ����� ������� ������������.

        Destroy(sphere); // ������� ���� GameObject � ������� ������.
    }
}

