using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

public class DrivingGame
{
    private Vector3 carPosition;
    private float carRotation;
    private Camera3D camera;
    Model carModel = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(2, 2, 2));
    private float currentSpeed = 0f;
    private const float acceleration = 0.000001f;
    private const float maxSpeed = 0.001f;
    private const float friction = 0.9999f;
    private List<Obstacle> obstacles = new List<Obstacle>();

    public void Init()
    {
        camera.Position = new Vector3(0, 10, -10);
        carPosition = new Vector3(0, 0, 0);
        carRotation = 0;
        camera.Target = carPosition;
        camera.Up = new Vector3(0, 1, 0);
        camera.FovY = 45;
        camera.Projection = CameraProjection.Perspective;

        obstacles.Add(new Obstacle { Position = new Vector3(5, 0, 5), Width = 2, Depth = 2, Height = 2, Color = Color.Blue });

        Random rng = new Random();

        for (int i = 0; i < 200; i++)
        {
            obstacles.Add(new Obstacle
            {
                Position = new Vector3(
                    rng.NextSingle() * 40 - 20,
                    0,
                    rng.NextSingle() * 500
                ),
                Width = 2,
                Height = 2,
                Depth = 2,
                Color = Color.Blue
            });
        }
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

        Vector3 previousPosition = carPosition;

        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            currentSpeed += acceleration;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.S))
        {
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

        if (CheckCollisions())
        {
            carPosition = previousPosition;
            currentSpeed = 0;
        }

        camera.Target = carPosition;
        camera.Position = carPosition + new Vector3(0, 5, -10);
    }

    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RayWhite);

        Raylib.DrawText("Speed: " + currentSpeed.ToString("F2"), 10, 10, 20, Color.Black);
        Raylib.DrawText($"X: {carPosition.X:F2}", 10, 30, 20, Color.Black);
        Raylib.DrawText($"Z: {carPosition.Z:F2}", 10, 50, 20, Color.Black);

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

        foreach (Obstacle obstacle in obstacles)
        {
            float distance = Vector3.Distance(carPosition, obstacle.Position);
            if (distance > 50f) continue;
            Raylib.DrawCube(obstacle.Position, obstacle.Width, obstacle.Height, obstacle.Depth, obstacle.Color);
            Raylib.DrawCubeWires(obstacle.Position, obstacle.Width, obstacle.Height, obstacle.Depth, Color.Black);
        }

        Raylib.EndMode3D();

        Raylib.EndDrawing();
    }

    public Boolean CheckCollisions()
    {
        bool collision = false;
        foreach (Obstacle obstacle in obstacles)
        {
            float distance = Vector3.Distance(carPosition, obstacle.Position);
            if (distance > 10f) continue;
            if (MathF.Abs(carPosition.X - obstacle.Position.X) < (1 + obstacle.Width / 2) &&
                MathF.Abs(carPosition.Y - obstacle.Position.Y) < (1 + obstacle.Height / 2) &&
                MathF.Abs(carPosition.Z - obstacle.Position.Z) < (1 + obstacle.Depth / 2))
            {
                collision = true;
            }
        }
        return collision;
    }
}

public class Obstacle
{
    public Vector3 Position;
    public float Width;
    public float Height;
    public float Depth;
    public Color Color;
}