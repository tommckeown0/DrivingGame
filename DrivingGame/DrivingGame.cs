using Raylib_cs;
using System.Numerics;

public class DrivingGame
{
    private Vector3 carPosition;
    private float carRotation;
    private Camera3D camera;
    private float speed = 0.0005f;
    Model carModel = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(2, 2, 2));
    private float currentSpeed = 0f;
    private const float acceleration = 0.000001f;
    private const float maxSpeed = 0.001f;
    private const float friction = 0.9999f;

    public void Init()
    {
        camera.Position = new Vector3(0, 10, 10);
        carPosition = new Vector3(0, 0, 0);
        carRotation = 0;
        camera.Target = carPosition;
        camera.Up = new Vector3(0, 1, 0);
        camera.FovY = 45;
        camera.Projection = CameraProjection.Perspective;
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }
    }

    public void Update()
    {
        Vector3 forward = new Vector3(
            MathF.Sin(carRotation),
            0,
            MathF.Cos(carRotation)
        );
        Vector3 backward = new Vector3(
            -MathF.Sin(carRotation),
            0,
            -MathF.Cos(carRotation)
        );
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            //carPosition += forward * currentSpeed;
            currentSpeed += acceleration;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            //carPosition += backward * currentSpeed;
            currentSpeed -= acceleration;
        }        
        else { 
            currentSpeed *= friction;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            carRotation += 0.0002f;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            carRotation -= 0.0002f;
        }
        carPosition += forward * currentSpeed;
        camera.Target = carPosition;
        camera.Position = carPosition + new Vector3(0, 5, 10);
        //currentSpeed *= friction;
    }

    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RayWhite);
        Raylib.DrawText("Speed: " + currentSpeed.ToString("F4"), 10, 10, 20, Color.Black);

        Raylib.BeginMode3D(camera);
        Vector3 forward = new Vector3(MathF.Sin(carRotation), 0, MathF.Cos(carRotation));        
        Raylib.DrawLine3D(carPosition, carPosition + forward * 10, Color.Blue);
        Raylib.DrawGrid(1000, 1);
        Raylib.DrawModelEx(
            carModel,
            carPosition,
            new Vector3(0, 1, 0),  // rotation axis (Y axis)
            carRotation * (180f / MathF.PI),  // rotation angle in DEGREES
            new Vector3(1, 1, 1),  // scale
            Color.Red
        );
        Raylib.DrawModelWiresEx(
            carModel,
            carPosition,
            new Vector3(0, 1, 0),  // rotation axis (Y axis)
            carRotation * (180f / MathF.PI),  // rotation angle in DEGREES
            new Vector3(1, 1, 1),  // scale
            Color.Black
        );
        Raylib.DrawLine3D(new Vector3(0, 0, 0), new Vector3(0, 5, 0), Color.Green);
        Raylib.DrawLine3D(new Vector3(0, 0, 0), new Vector3(5, 0, 0), Color.Red);
        Raylib.DrawLine3D(new Vector3(0, 0, 0), new Vector3(0, 0, 5), Color.Blue);        
        Raylib.EndMode3D();

        Raylib.EndDrawing();
    }
}