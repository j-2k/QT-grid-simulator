using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IQuadTreeObject
{
    Vector2 GetPosition();
}
public class QuadTreeN2<T> where T : IQuadTreeObject
{
    private int m_maxObjectCount;
    private List<T> m_storedObjects;
    public Rect m_bounds;
    private QuadTreeN2<T>[] cells;

    public QuadTreeN2(int maxSize, Rect bounds)
    {
        m_maxObjectCount = maxSize;
        m_storedObjects = new List<T>(maxSize);
        //
        m_bounds = bounds;
        cells = new QuadTreeN2<T>[4];
    }

    public void Insert(T insertedObject)                                        //Insert function
    {
        if (cells[0] != null)                                                   //If the first cell has been created exe
        {
            int iCell = GetCellToInsertObject(insertedObject.GetPosition());    //
            if (iCell > -1)                                                     //
            {
                cells[iCell].Insert(insertedObject);                            //
            }
            return;                                                             //Return = get out of this scope & continue
        }

        m_storedObjects.Add(insertedObject);
        if (m_storedObjects.Count > m_maxObjectCount)                           //Check if the amount of stored objects exceed the maximum object count to then start dividing
        {
            //Start Dividing Into 4 Squares
            if (cells[0] == null)
            {
                float subWidth = (m_bounds.width / 2f);     // div width by 2
                float subHeight = (m_bounds.height / 2f);   // div height by 2
                float x = m_bounds.x;                       // get x pos
                float y = m_bounds.y;                       // get y pos
                cells[0] = new QuadTreeN2<T>(m_maxObjectCount, new Rect(x + subWidth, y, subWidth, subHeight));                 //Rect = (x,y,width,height)
                cells[1] = new QuadTreeN2<T>(m_maxObjectCount, new Rect(x, y, subWidth, subHeight));                            // x = x pos / y = y pos / width & height = scale of rect
                cells[2] = new QuadTreeN2<T>(m_maxObjectCount, new Rect(x, y + subHeight, subWidth, subHeight));
                cells[3] = new QuadTreeN2<T>(m_maxObjectCount, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));
            }
        }
    }

    //Clear the Quad Tree
    public void Clear()
    {
        m_storedObjects.Clear();

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != null)
            {
                cells[i].Clear();
                cells[i] = null;
            }
        }
    }

    public void DrawLines()
    {
        DebugDrawBounds();

        if (cells[0] != null)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] != null)
                {
                    cells[i].DrawLines();
                }
            }
        }
    }

    public void DebugDrawBounds()
    {
        Debug.DrawLine(new Vector2(m_bounds.x, m_bounds.y), new Vector2(m_bounds.x, m_bounds.y + m_bounds.height), Color.red);
        Debug.DrawLine(new Vector2(m_bounds.x, m_bounds.y), new Vector2(m_bounds.x + m_bounds.width, m_bounds.y), Color.red);
        Debug.DrawLine(new Vector2(m_bounds.x + m_bounds.width, m_bounds.y), new Vector2(m_bounds.x + m_bounds.width, m_bounds.y + m_bounds.height), Color.red);
        Debug.DrawLine(new Vector2(m_bounds.x + m_bounds.width, m_bounds.y + m_bounds.height), new Vector2(m_bounds.x, m_bounds.y + m_bounds.height), Color.red);
    }

    public bool ContainsLocation(Vector2 position)                              //ContainsLocation Function to check for a T/F if the rect contains the pos
    {                                                                           
        return m_bounds.Contains(position);                                     //Return a true or false if the bounds of the rect contains the position
    }

    int GetCellToInsertObject(Vector2 position)
    {
        for (int i = 0; i < 4; i++)
        {
            if(cells[i].ContainsLocation(position))
            {
                return i;
            }
        }
        return -1;
    }
} 
