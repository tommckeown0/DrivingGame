using Raylib_cs;

Raylib.InitWindow(800, 600, "Driving Game");

//CameraDemo.Run();
//DrivingGame.Run();
DrivingGame game = new DrivingGame();
game.Init();
game.Run();

Raylib.CloseWindow();