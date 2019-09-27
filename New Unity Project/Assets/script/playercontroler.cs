using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playercontroler : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public int scale;
    int test = 3;
    Vector3[] Point = new Vector3[50];
    int ipoint = 0;
    int nbpoint;
    // Start is called before the first frame update
    void Start()

    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && transform.position.x % scale == 0 && transform.position.z % scale == 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                float z = arrondi( hit.point.z);
                float x = arrondi(hit.point.x);


                Debug.Log("x :" + x + ", z :" + z);
                /*agent.SetDestination(new Vector3(x,hit.point.y,z));
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    agent.destination = new Vector3(0, 0, 0);*/

                addpoint(x,z);
            }
        }
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            Go();
            
        }
    }

    float arrondi(float a )
    {
        if ((((int)a + 1) - a) > 0.5f)
            a = (int)(a);
        else
            a = (int)(a + 1);

        if ((a - 1) % scale == 0f)
            return a - 1;
        else if ((a + 1) % scale == 0f)
            return a + 1;
        else return a;
       
    }

    void addpoint(float x , float z )
    {
        float px = transform.position.x;
        float pz = transform.position.z;
        if (x > px)
        {
            for (int i = (int)0; px < x; i++)
            {
                px += scale;
                Point[i] = new Vector3(px, 0, pz);               
                nbpoint++;
            }
        }
        else if(x< px)
        {
            for (int i = (int)0; px > x; i++)
            {
                px -= scale;
                Point[i] = new Vector3(px, 0, pz);
                nbpoint++;
            }

        }
        if (z > pz)
        {
            for (int i = (int)nbpoint; pz < z; i++)
            {
                pz += scale;
                Point[i] = new Vector3(px, 0, pz);
               
                nbpoint++;
            }
        }
        if (z < pz)
        {
            for (int i = (int)nbpoint; pz > z; i++)
            {
                pz -= scale;
                Point[i] = new Vector3(px, 0, pz);
                
                nbpoint++;
            }
        }


    }

    void Go()
    {
        if(ipoint == nbpoint)
        {
            nbpoint = 0;
            ipoint = 0;
        }
        if (nbpoint == 0)
            return;
        agent.destination = Point[ipoint];
        ipoint++;
            
        
    }
}
