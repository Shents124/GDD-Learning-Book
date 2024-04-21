﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineController : MonoBehaviour 
{
	public Material lineMaterial;
	public Color selectColor;
	public Color endColor;
	public bool ignoreIntercept=false;

	private LineRenderer line;
	private bool isDrawing = false;
	private List<Vector3> pointsList = new List<Vector3>();
	private Vector3 inputPos;

	struct DashLine
	{
		public Vector3 start;
		public Vector3 end;
	};

	void Awake()
	{
		line = GetComponent<LineRenderer> ();
		line.useWorldSpace = true;
		isDrawing = false;
		Init();
	}

	void SetColor(Color color)
    {
		line.startColor = color;
		line.endColor = color;
    }

	public void Init()
    {
		line.positionCount = 0;
		pointsList.RemoveRange(0, pointsList.Count);
		SetColor(selectColor);
	}

    void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			isDrawing = true;
			Init();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isDrawing = false;
		}
		// Track points on mouse pressed
		if(isDrawing)
		{
			inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			inputPos.z=0;
			if (!pointsList.Contains (inputPos)) 
			{
				int previousIndex = pointsList.Count;
				pointsList.Add (inputPos);
				line.positionCount = pointsList.Count;
				line.SetPosition (previousIndex, (Vector3)pointsList [previousIndex]);
				if(!ignoreIntercept && doesLineIntersectsItself ())
				{
					isDrawing = false;
					SetColor (endColor);
				}
			}
		}
	}

	private bool doesLineIntersectsItself()
	{
		if (pointsList.Count < 2)
			return false;
		int TotalLines = pointsList.Count - 1;
		DashLine [] lines = new DashLine [TotalLines];
		if (TotalLines > 1) 
		{
			for (int i=0; i<TotalLines; i++) 
			{
				lines [i].start = (Vector3)pointsList [i];
				lines [i].end = (Vector3)pointsList [i + 1];
			}
		}
		for (int i=0; i<TotalLines-1; i++) 
		{
			DashLine currentLine;
			currentLine.start = (Vector3)pointsList [pointsList.Count - 2];
			currentLine.end = (Vector3)pointsList [pointsList.Count - 1];
			if (doSegmentIntersect (lines [i], currentLine)) 
				return true;
		}
		return false;
	}

	private bool isSamePoint (Vector3 pointA, Vector3 pointB)
	{
		return (pointA.x == pointB.x && pointA.y == pointB.y);
	}

	private bool doSegmentIntersect (DashLine L1, DashLine L2)
	{
		if (isSamePoint (L1.start, L2.start) || isSamePoint (L1.start, L2.end) ||
			isSamePoint (L1.end, L2.start) || isSamePoint (L1.end, L2.end))
			return false;
		
		return((Mathf.Max (L1.start.x, L1.end.x) >= Mathf.Min (L2.start.x, L2.end.x)) &&
		       (Mathf.Max (L2.start.x, L2.end.x) >= Mathf.Min (L1.start.x, L1.end.x)) &&
		       (Mathf.Max (L1.start.y, L1.end.y) >= Mathf.Min (L2.start.y, L2.end.y)) &&
		       (Mathf.Max (L2.start.y, L2.end.y) >= Mathf.Min (L1.start.y, L1.end.y)) 
		       );
	}

	public List<Vector3> getPoints()
    {
		return pointsList;
    }
}