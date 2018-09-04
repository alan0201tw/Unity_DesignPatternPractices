using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawAxis : MonoBehaviour
{
    /// <summary>
    /// You can use Unity's Default-line material. The Default-material, which uses Unity's standard shader.
    /// Doesn't support vertex color(the color data in each vertices), so you will see black lines only.
    /// </summary>
    public Material mat;

    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        //GL.PushMatrix();
        mat.SetPass(0);
        //GL.LoadOrtho();

        GL.Begin(GL.LINES);
        // X axis
        GL.Color(Color.red);
        GL.Vertex(new Vector3(-100, 0, 0));
        GL.Vertex(new Vector3(100, 0, 0));
        GL.End();

        GL.Begin(GL.LINES);
        // Y axis
        GL.Color(Color.green);
        GL.Vertex(new Vector3(0, -100, 0));
        GL.Vertex(new Vector3(0, 100, 0));
        GL.End();

        GL.Begin(GL.LINES);
        // Z axis
        GL.Color(Color.blue);
        GL.Vertex(new Vector3(0, 0, -100));
        GL.Vertex(new Vector3(0, 0, 100));
        GL.End();

        //GL.PopMatrix();
    }
}