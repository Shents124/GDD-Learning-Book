using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleConsumer : MonoBehaviour
{

        public LineController lineController;

        void OnGUI ()
        {
                // Just query for the points from the lineController
                string text = "";
                List<Vector3> points = lineController.getPoints ();
                foreach (Vector3 point in points)
                        text += point.ToString () + ", ";
                GUI.Label (new Rect (40, 40, 500, 20), "Total points: "+points.Count);
                GUI.Label (new Rect (40, 60, 500, 500), text);
        }
}
