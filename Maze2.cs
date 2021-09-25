using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2 : MonoBehaviour
{
    int mazeWidth = 22;
    int mazeDepth = 19;

    int[,] maze = new int[19, 22] {
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
    { 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
    { 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
    { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 }
    };

    public List<GameObject> tileset = new List<GameObject>();

    float tileWidth = 3.0f;
    float tileDepth = 3.0f;

    float xi = -25.0f;
    float zi = 25.0f;

    Vector2 finalPos, beginPos, atual;
    List<Vector2> visitados = new List<Vector2>();
    List<Vector2> becos = new List<Vector2>();

    public List<GameObject> path = new List<GameObject>();

    int[,] mazeCopy = new int[19, 22] {
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
    { 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
    { 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1 },
    { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
    { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1 },
    { 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
    { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1 },
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 }
    };

    void Start()
    {
        beginPos = new Vector2(0, 1);
        finalPos = new Vector2(18, 20);
        atual = beginPos;
        //Debug.Log(atual);
        Create();

        makeCost();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetPath();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (atual != finalPos)
            {
                PathCost();
            }
        }
    }

    void Create()
    {
        for (int i = 0; i < mazeDepth; i++) //z
        {
            for (int j = 0; j < mazeWidth; j++) //x
            {
                GameObject tilePrefab = tileset[maze[i, j]];
                Vector3 p = tilePrefab.transform.position;
                p.x = xi + j * tileWidth;
                p.z = zi - i * tileDepth;

                GameObject newTile = Instantiate(tilePrefab, p, Quaternion.identity) as GameObject;

            }
        }
    }

    bool verifyWall(Vector2 n)
    {
        bool isWall = false;
        if (maze[(int)n.x, (int)n.y] == 1)
        {
            isWall = true;
        }
        return isWall;
    }

    bool verifyBeco(Vector2 n)
    {
        bool exists = false;
        if (becos.Count != 0)
        {
            for (int i = 0; i < becos.Count; i++)
            {
                if (n == becos[i])
                {
                    exists = true;
                    break;
                }
            }
        }

        return exists;
    }

    void Refresh(Vector2 n)
    {
        GameObject tilePrefab = tileset[maze[(int)n.x, (int)n.y]];
        Vector3 p = tilePrefab.transform.position;
        p.x = xi + (int)n.y * tileWidth;
        p.z = zi - (int)n.x * tileDepth;
        //p.y = 2.0f;

        //Destroy()

        GameObject newTile = Instantiate(tilePrefab, p, Quaternion.identity) as GameObject;

        path.Add(newTile);
    }

    void ResetPath()
    {
        if (path.Count > 0)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Destroy(path[i]);
                //path.RemoveAt(path.Count - 1);
            }

            for (int i = 0; i < visitados.Count; i++)
            {
                maze[(int)visitados[i].x, (int)visitados[i].y] = 0;
            }
            path.Clear();
            visitados.Clear();
            beginPos = new Vector2(0, 1);
            atual = beginPos;
        }
    }


    //aqui embaixo começa o A*

    void makeCost()
    {
        for (int i = 0; i < mazeDepth; i++) //z
        {
            for (int j = 0; j < mazeWidth; j++) //x
            {
                if (mazeCopy[i, j] == 1)
                {
                    mazeCopy[i, j] = 5000;
                }
                else
                {
                    mazeCopy[i, j] = Random.Range(0, 10);
                }

            }
        }
    }

    float DistanceCost(Vector2 a, Vector2 b, int cost)
    {
        //float t = Mathf.Abs(a.x-b.x)+Mathf.Abs(a.y-b.y);
        //Debug.Log(t);
        //return t;
        return cost + Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }

    void PathCost()
    {
        while (atual != finalPos)
        {
            Vector2[] lados = new Vector2[]
            {
                new Vector2( 0, 0 ),
                new Vector2( 0, 0 ),
                new Vector2( 0, 0 ),
                new Vector2( 0, 0 ),
            };

            float d = 5000;
            int index1 = 0, index2 = 0;

            lados[0] = new Vector2(atual.x - 1, atual.y);
            lados[1] = new Vector2(atual.x + 1, atual.y);
            lados[2] = new Vector2(atual.x, atual.y - 1);
            lados[3] = new Vector2(atual.x, atual.y + 1);

            //bool buraco;
            //buraco = false;
            int count = 0;


            for (int i = 0; i < 4; i++)
            {
                //Debug.Log(mazeCopy[(int)lados[i].x, (int)lados[i].y]);

                if (lados[i].x > -1 && lados[i].y > -1 && lados[i].x <= 18 && lados[i].y <= 21)
                {
                    float p = DistanceCost(finalPos, lados[i], mazeCopy[(int)lados[i].x, (int)lados[i].y]);
                    //Debug.Log(lados[i]);
                    //Debug.Log(p);

                    if (maze[(int)lados[i].x, (int)lados[i].y] == 1 || verifyBeco(lados[i]) == true || verifyPath(lados[i]) == true)
                    {
                        count++;
                    }

                    if (p < d && verifyWall(lados[i]) == false && verifyBeco(lados[i]) == false && verifyPath(lados[i]) == false)//visitados[visitados.Count-1] != lados[i]
                    {
                        if (visitados.Count == 0)
                        {
                            d = p;
                            index1 = (int)lados[i].x;
                            index2 = (int)lados[i].y;
                            // Debug.Log(lados[i]);
                            // Debug.Log(d);
                        }
                        else if (visitados[visitados.Count - 1] != lados[i])
                        {
                            d = p;
                            index1 = (int)lados[i].x;
                            index2 = (int)lados[i].y;
                            // Debug.Log(lados[i]);
                            // Debug.Log(d);

                        }
                    }
                }
            }

            if (count <= 2)
            {
                visitados.Add(atual);
                maze[(int)atual.x, (int)atual.y] = 2;
                //Refresh(atual);
                atual = new Vector2(index1, index2);
            }
            else
            {
                becos.Add(atual);
                maze[(int)atual.x, (int)atual.y] = 0;
                //Refresh(atual);
                atual = visitados[visitados.Count - 1];
                visitados.RemoveAt(visitados.Count - 1);
            }
            d = 5000;
            Debug.Log(atual);

            if (atual == finalPos)
            {
                maze[(int)atual.x, (int)atual.y] = 2;
                visitados.Add(atual);

                for (int i = 0; i < visitados.Count; i++)
                {
                    Refresh(visitados[i]);
                }
            }
        }
    }

    bool verifyPath(Vector2 n)
    {
        bool exist = false;

        if (visitados.Count > 0)
        {
            for (int i = 0; i < visitados.Count; i++)
            {
                if (visitados[i] == n && n != visitados[visitados.Count - 1])
                {
                    exist = true;
                    break;
                }
            }
        }

        return exist;
    }
}
