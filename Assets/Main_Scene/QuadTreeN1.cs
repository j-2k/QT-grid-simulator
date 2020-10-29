using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeN1 : MonoBehaviour
{
    public Rect r_bounds;
    Vector3 mousePos;
    GameObject sphereT;
    public Rect[] cells;
    int cellSize = 0;
    // Start is called before the first frame update
    void Start()
    {
        r_bounds.width = 12;
        r_bounds.height = 12;
        //r_bounds.position = new Vector2(-r_bounds.width / 2, -r_bounds.height / 2);
        r_bounds.center = Camera.main.transform.position;
        sphereT = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereT.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        sphereT.name = "Point";
        sphereT.transform.position = new Vector3(0,8,0);
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebug();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetMouseButton(0))
        //{
        //    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
        if (r_bounds.Contains(mousePos))
        {
            Debug.Log("mouse is inside the rect");
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(sphereT, mousePos + transform.forward * 5, Quaternion.identity);
                Debug.Log(mousePos);
                cellSize += 4;
                cells = new Rect[cellSize];
                float subWidth = (r_bounds.width / 2f);
                float subHeight = (r_bounds.height / 2f);
                float x = r_bounds.x;
                float y = r_bounds.y;
                for (int i = 0; i < cellSize; i++)
                {
                    if (i == 0 || i == 4 || i == 8)
                    {
                        cells[i] = new Rect(x + subWidth, y, subWidth, subHeight);
                    }
                    if (i == 1 || i == 5 || i == 9)
                    {
                        cells[i] = new Rect(x, y, subWidth, subHeight);
                    }
                    if (i == 2 || i == 6 || i == 10)
                    {
                        cells[i] = new Rect(x, y + subHeight, subWidth, subHeight);
                    }
                    if (i == 3 || i == 7 || i == 11)
                    {
                        cells[i] = new Rect(x + subWidth, y + subHeight, subWidth, subHeight);
                        //i = 0;
                        //break;
                    }
                }
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].Contains(mousePos))
                    {
                        subWidth = (r_bounds.width / 4f);
                        subHeight = (r_bounds.height / 4f);
                        x = r_bounds.x/2;
                        y = r_bounds.y/2;
                        for (int j = 0; j < cellSize; j++)
                        {
                            if (j == 0 || j == 4 || j == 8)
                            {
                                cells[i] = new Rect(x + subWidth, y, subWidth, subHeight);
                            }
                            if (j == 1 || j == 5 || j == 9)
                            {
                                cells[i] = new Rect(x, y, subWidth, subHeight);
                            }
                            if (j == 2 || j == 6 || j == 10)
                            {
                                cells[i] = new Rect(x, y + subHeight, subWidth, subHeight);
                            }
                            if (j == 3 || j == 7 || j == 11)
                            {
                                cells[i] = new Rect(x + subWidth, y + subHeight, subWidth, subHeight);
                                //i = 0;
                                //break;
                            }
                        }
                    }
                }

            }
        }
        else
        {
            Debug.Log("no mouse detected");
        }

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != null)
            {
                Debug.DrawLine(new Vector2(cells[i].x, cells[i].y), new Vector2(cells[i].x, cells[i].y + cells[i].height), Color.red);
                Debug.DrawLine(new Vector2(cells[i].x, cells[i].y), new Vector2(cells[i].x + cells[i].width, cells[i].y), Color.red);
                Debug.DrawLine(new Vector2(cells[i].x + cells[i].width, cells[i].y), new Vector2(cells[i].x + cells[i].width, cells[i].y + cells[i].height), Color.red);
                Debug.DrawLine(new Vector2(cells[i].x + cells[i].width, cells[i].y + cells[i].height), new Vector2(cells[i].x, cells[i].y + cells[i].height), Color.red);
            }
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 90), "QT Panel");

        if (GUI.Button(new Rect(20, 40, 80, 20), "Start QT"))
        {

        }

        if (GUI.Button(new Rect(20, 70, 80, 20), "Insert Point"))
        {

        }
    }

    void DrawDebug()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(r_bounds.x, r_bounds.y), new Vector2(r_bounds.x, r_bounds.y + r_bounds.height));
        Gizmos.DrawLine(new Vector2(r_bounds.x, r_bounds.y), new Vector2(r_bounds.x + r_bounds.width, r_bounds.y));
        Gizmos.DrawLine(new Vector2(r_bounds.x + r_bounds.width, r_bounds.y), new Vector2(r_bounds.x + r_bounds.width, r_bounds.y + r_bounds.height));
        Gizmos.DrawLine(new Vector2(r_bounds.x + r_bounds.width, r_bounds.y + r_bounds.height), new Vector2(r_bounds.x, r_bounds.y + r_bounds.height));
        Gizmos.DrawWireSphere(r_bounds.center, 0.1f);
    }
}
