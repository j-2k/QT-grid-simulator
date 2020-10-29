using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeN2Starter : MonoBehaviour
{
	public class SphereObj : IQuadTreeObject
	{
		private Vector2 m_Position;
		public SphereObj(Vector3 position)
		{
			m_Position = position;
		}
		public Vector2 GetPosition()
		{
			return new Vector2(m_Position.x, m_Position.y);
		}
	}
	Rect quadtreeSize;
	GameObject sphereGO;
	Vector3 mousePos;
	QuadTreeN2<SphereObj> sphereQuadTree;
	void Start()
    {
		quadtreeSize = new Rect(-100, -100, 200, 200);
		sphereGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphereGO.transform.localScale = new Vector3(4,4,1);
		sphereQuadTree = new QuadTreeN2<SphereObj>(0, quadtreeSize);
	}

    void Update()
    {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (sphereQuadTree.m_bounds.Contains(mousePos))
		{
			Debug.Log("mouse in bounds");
			if (Input.GetMouseButtonDown(0))
			{
				Instantiate(sphereGO, mousePos + transform.forward * 5, Quaternion.identity);
				SphereObj sphereObj = new SphereObj(mousePos);
				sphereQuadTree.Insert(sphereObj);
			}
		}
		else
		{
			Debug.Log("mouse is not in the bounds of the QT");
		}

		if (Input.GetMouseButtonDown(1))
		{
			sphereQuadTree.Clear();
		}
	}

	void OnDrawGizmos()
	{
		if (sphereQuadTree != null)           // dont remove or get annoying error message
		{
			sphereQuadTree.DrawLines();
		}
	}
}
