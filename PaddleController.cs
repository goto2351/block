using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // マウスのx座標に合わせてパドルを移動させる
        Vector3 pointerScreenPosition = Input.mousePosition;
        //画面外に出ないように制限する
        pointerScreenPosition.x = Mathf.Clamp(pointerScreenPosition.x, 0.0f, Screen.width);
       // pointerScreenPosition.y = -4.0f;
        //pointerScreenPosition.z = 0.0f;

        Camera gameCamera = Camera.main;
        Vector3 pointerWorldPosition = gameCamera.ScreenToWorldPoint(pointerScreenPosition);
        pointerWorldPosition.y = -4.0f;
        pointerWorldPosition.z = 0.0f;
        gameObject.transform.position = pointerWorldPosition;

    }
}
