using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class BattleInputManager : MonoBehaviour
    {
        private Camera mainCamera;

        private static Vector3 mouseDrugPosition;
        private bool druging;

        private ContactFilter2D contactFilter2D = new ContactFilter2D();
        private RaycastHit2D[] results = new RaycastHit2D[8];

        void Start()
        {
            contactFilter2D.useTriggers = true;
            mainCamera = GetComponent<Camera>();
        }

        void Update()
        {
            //Debug.Log(InputSettings.MouseLayer);
            if (InputMouseInformator.MouseLayer == 0 || druging)
            {
                if (Input.mouseScrollDelta.y != 0)
                {
                    mainCamera.orthographicSize = math.max(mainCamera.orthographicSize +
                        -Input.mouseScrollDelta.y * InputMouseInformator.ZoomScrollSensivity, 0.1f);
                }

                if (Input.GetMouseButtonDown(1))//RCM
                {
                    druging = true;
                    mouseDrugPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                }

                if (Input.GetMouseButton(1))//RCM
                {
                    if (mouseDrugPosition == Vector3.zero)
                    {
                        druging = true;
                        mouseDrugPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    }
                    mainCamera.transform.position += mouseDrugPosition - mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -500);
                }

                if (Input.GetMouseButtonUp(1))//RCM
                {
                    druging = false;
                }

                //Unit pic
                if (Input.GetMouseButtonDown(0))//LCM
                {
                    Vector2 _cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                    //UnitController.SelectedUnits.Clear();
                    int rayCollision = Physics2D.Raycast(_cursorPosition, new Vector2(0, 0),
                        contactFilter2D, results, mainCamera.farClipPlane);
                    if (rayCollision > 0)
                    {
                        if (SelectedUnitsInformator.SelectedUnits.Count > 0)
                            SelectedUnitsInformator.SelectedUnits[0] = results[0].transform.gameObject.GetComponent<UnitOperator>();
                        else
                            SelectedUnitsInformator.SelectedUnits.Add(results[0].transform.gameObject.GetComponent<UnitOperator>());
                    }
                }
            }
        }

        public static void ResetMouseDrugPosition()
        {
            if (InputMouseInformator.MouseLayer == 0)
                mouseDrugPosition = Vector3.zero;
        }
    }
}
