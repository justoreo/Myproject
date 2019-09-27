using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tilegeneration : MonoBehaviour {
	public GameObject bloc1;
	public GameObject bloc0;
    public GameObject bloc2;
    public NavMeshSurface surface;
    public Camera cam;
    public int scale = 3;
    private const int len = 50, hei = 50;
    private GameObject[,] bloc = new GameObject[len, hei];
    private int[,] mat = new int[len, hei];

    // Use this for initialization
    void Start() {        
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < hei; j++)
            {
                mat[i, j] = (i % 2 + j % 2) % 2;
            }
        }
        for (int i = 0; i < len; i++) {
            for (int j = 0; j < hei; j++) {
                if (mat[i, j] == 1)
                    bloc[i, j] = Instantiate(bloc1, new Vector3(i * scale, 0, j * scale), Quaternion.identity);
                else
                    bloc[i, j] = Instantiate(bloc0, new Vector3(i * scale, 0, j * scale), Quaternion.identity);
            }
        }
        // update nav
        surface.BuildNavMesh();
	}

    // Update is called once per frame
    void Update()
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) || true)
        {
            resetcolor();
            if (Physics.Raycast(r, out hit))
            {
                float z = arrondi(hit.point.z);
                float x = arrondi(hit.point.x);

                vert(x, z);
             

            }
        }

    }
    float arrondi(float a)
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

    void resetcolor()
    {
        for(int i = 0; i < len; i++)
        {
            for (int j = 0; j < hei; j++)
            {
                if(bloc[(int)i , (int)j].name == "Cube2(Clone)")
                {
                    Destroy(bloc[(int)i, (int)j]);
                    if (mat[i, j] == 0)
                    {
                        bloc[(int)i , (int)j] = Instantiate(bloc0, new Vector3(i*scale, 0, j*scale), Quaternion.identity);
                    }
                    else
                        bloc[(int)i, (int)j] = Instantiate(bloc1, new Vector3(i*scale , 0, j*scale ), Quaternion.identity);

                }
                
            }
        }
    }

    void vert(float x , float z)
    {
        GameObject perso = GameObject.Find("boby");
        if (!perso.GetComponent<NavMeshAgent>().pathPending && perso.GetComponent<NavMeshAgent>().remainingDistance ==0)
        {
            
            float px = perso.transform.position.x;
            float pz = perso.transform.position.z;
            if (x > px)
            {
                for (int i = (int)px; i <= x; i += scale)
                {
                    Destroy(bloc[(int)i / scale, (int)pz / scale]);
                    bloc[(int)i / scale, (int)pz / scale] = Instantiate(bloc2, new Vector3(i, 0, pz), Quaternion.identity);
                }
            }
            else if (px >= x)
            {
                for (int i = (int)x; i <= px; i += scale)
                {
                    Destroy(bloc[(int)i / scale, (int)pz / scale]);
                    bloc[(int)i / scale, (int)pz / scale] = Instantiate(bloc2, new Vector3(i, 0, pz), Quaternion.identity);
                }
            }
            if (z > pz)
            {
                for (int i = (int)pz; i <= z; i += scale)
                {
                    Destroy(bloc[(int)x / scale, (int)i / scale]);
                    bloc[(int)x / scale, (int)i / scale] = Instantiate(bloc2, new Vector3(x, 0, i), Quaternion.identity);
                }
            }
            else if (pz > z)
            {
                for (int i = (int)z; i <= pz; i += scale)
                {
                    Destroy(bloc[(int)x / scale, (int)i / scale]);
                    bloc[(int)x / scale, (int)i / scale] = Instantiate(bloc2, new Vector3(x , 0, i ), Quaternion.identity);
                }
            }
        }
    }
}
