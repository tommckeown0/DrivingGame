using Raylib_cs;
using System.Numerics;

public static class CameraDemo
{
    public static void Run()
    {
        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(0, 10, 0);
        camera.Target = new Vector3(0, 0, 0);
        camera.Up = new Vector3(0, 1, 0);
        camera.FovY = 45;
        camera.Projection = CameraProjection.Perspective;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                camera.Position += new Vector3(-0.001f, 0, 0);
                //camera.Target += new Vector3(-0.001f, 0, 0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                camera.Position += new Vector3(0.001f, 0, 0);
                //camera.Target += new Vector3(0.001f, 0, 0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                camera.Position += new Vector3(0, 0, -0.001f);
                //camera.Target += new Vector3(0, 0, -0.001f);
            }
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                camera.Position += new Vector3(0, 0, 0.001f);
                //camera.Target += new Vector3(0, 0, 0.001f);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Q))
            {
                camera.Position += new Vector3(0, -0.001f, 0);
                //camera.Target += new Vector3(0, -0.001f, 0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.E))
            {
                camera.Position += new Vector3(0, 0.001f, 0);
                //camera.Target += new Vector3(0, 0.001f, 0);
            }

            Raylib.DrawText("Position X value: " + camera.Position.X.ToString("F3"), 10, 10, 20, Color.Black);
            Raylib.DrawText("Position Y value: " + camera.Position.Y.ToString("F2"), 10, 30, 20, Color.Black);
            Raylib.DrawText("Position Z value: " + camera.Position.Z.ToString("F2"), 10, 50, 20, Color.Black);
            Raylib.DrawText("Target X value: " + camera.Target.X.ToString("F3"), 10, 70, 20, Color.Black);
            Raylib.DrawText("Target Y value: " + camera.Target.Y.ToString("F2"), 10, 90, 20, Color.Black);
            Raylib.DrawText("Target Z value: " + camera.Target.Z.ToString("F2"), 10, 110, 20, Color.Black);

            Raylib.BeginMode3D(camera);
            Raylib.DrawGrid(10, 1);
            Raylib.DrawCube(new Vector3(0, 0, 0), 2, 2, 2, Color.Red);
            Raylib.DrawCubeWires(new Vector3(0, 0, 0), 2, 2, 2, Color.Black);
            Raylib.DrawLine3D(new Vector3(0, 0, 0), new Vector3(0, 5, 0), Color.Green);
            Raylib.DrawLine3D(new Vector3(0, 0, 0), new Vector3(5, 0, 0), Color.Red);
            Raylib.DrawLine3D(new Vector3(0, 0, 0), new Vector3(0, 0, 5), Color.Blue);
            Raylib.EndMode3D();

            Raylib.EndDrawing();
        }
    }
}
