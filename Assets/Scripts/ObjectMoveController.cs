using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveController : MonoBehaviour
{
    [SerializeField] private float speed;

    public static ObjectMoveController instance;

    Vector2 pos;
    bool isRevial;
    float distance;
    float timeStart;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        speed = 2.5f;
        isRevial = false;
        timeStart = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRevial)
        {
            timeStart += Time.deltaTime;
            if(timeStart > 0.5f)
            {
                transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0, 0));
            }
            
        }
        else
        {
            pos = transform.position;
           
            transform.position = new Vector3(pos.x + distance, pos.y, 0);
            
            isRevial = false;
        }
    }

    public void Revival(float dis)
    {
        distance = dis;
        isRevial = true;
       
    }
    
}
