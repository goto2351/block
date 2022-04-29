using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockController : MonoBehaviour
{
    private int lifetime;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= 1;
        if (lifetime == 0)
        {
            Destroy(gameObject);
            GameObject.Find("Canvas").GetComponent<ItemController>().DestroyItemBlock();
        }
    }

}
