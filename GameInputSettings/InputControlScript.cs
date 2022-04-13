using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InputControlScript : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private Vector3 _position;
    private bool _druging;

    private ContactFilter2D _contactFilter2D = new ContactFilter2D();
    private RaycastHit2D[] results = new RaycastHit2D[8];

    void Start()
    {
        UnitController.SelectedUnits.CollectionChanged += UnitController.ShowUnitInfo;
    }

    void Update()
    {
        //Debug.Log(InputSettings.MouseLayer);
        if (InputSettings.MouseLayer == 0 || _druging)
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                _camera.orthographicSize = math.max(_camera.orthographicSize +
                    -Input.mouseScrollDelta.y * InputSettings.ZoomScrollSensivity, 0.1f);
            }

            if (Input.GetMouseButtonDown(1))//LCM
            {
                _druging = true;
                _position = _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(1))//RCM
            {
                _camera.transform.position += _position - _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(1))//RCM
            {
                _druging = false;
            }
        }

        if (Input.GetMouseButtonDown(0))//LCM
        {
            Vector2 _cursorPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            int rayCollision = Physics2D.Raycast(_cursorPosition, new Vector2(0, 0), _contactFilter2D, results, _camera.farClipPlane);
            if (rayCollision > 0)
            {
                UnitController.SelectedUnits.Add(results[0].transform.gameObject);
            }
        }
    }
}
