using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMagic : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private ContactFilter2D _contactFilter2D = new ContactFilter2D();
    private RaycastHit2D[] results = new RaycastHit2D [8];

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {   
        Vector2 _cursorPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        Ray2D ray = new Ray2D(_cursorPosition, new Vector3(0,0,1));
        int rayCollision = Physics2D.Raycast(_cursorPosition, new Vector2(0, 0), _contactFilter2D, results, _camera.farClipPlane);
        if(rayCollision > 0)
        {
            foreach (RaycastHit2D result in results)
            {
                if(result is RaycastHit2D raycastHit2D)
                {
                    //raycastHit2D.Damage(float damage, Points points);
                }
            }
        }
    }

}
