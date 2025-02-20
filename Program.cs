using System;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;

InitializeGame();
while (!shouldExit)
{
    // Check for terminal resize before each move, enable optional termination by default
    if (TerminalResized())
    {
        Console.Clear();
        Console.WriteLine("Console was resized. Program exiting.");
        shouldExit = true;
        continue;
    }
    Move(enableTermination: true, speedDelta: IsPlayerSpeedBoosted() ? 3 : 0);

    // Check if food was consumed after each move
    if (HasConsumedFood())
    {
        int consumedFoodIndex = food;
        ChangePlayer(consumedFoodIndex);
        ShowFood();
        if (consumedFoodIndex == 2)
        {
            FreezePlayer();
        }
    }
}

// Returns true if the player position overlaps with food position
bool HasConsumedFood()
{
    // Check if player and food are on the same row
    if (playerY != foodY) return false;

    // Check if player overlaps with any part of the food
    int playerRight = playerX + player.Length;
    int foodRight = foodX + 5;

    // Check for overlap
    return playerX < foodRight && playerRight > foodX;
}

// Returns true if the Terminal was resized 
bool TerminalResized()
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

// Displays random food at a random location
void ShowFood()
{
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer(int consumedFoodIndex)
{
    player = states[consumedFoodIndex];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops the player from moving
void FreezePlayer()
{
    System.Threading.Thread.Sleep(1000);
    player = states[0];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Returns true if the player's appearance is (X_X)
bool IsPlayerFrozen()
{
    return player == states[2];
}

// Returns true if the player's appearance is (^-^)
bool IsPlayerSpeedBoosted()
{
    return player == states[1];
}

// Reads directional input from the Console and moves the player
void Move(bool enableTermination = false, int speedDelta = 0)
{
    if (IsPlayerFrozen())
    {
        return;
    }

    int lastX = playerX;
    int lastY = playerY;

    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                playerY--;
                break;
            case ConsoleKey.DownArrow:
                playerY++;
                break;
            case ConsoleKey.LeftArrow:
                playerX -= (1 + speedDelta);
                break;
            case ConsoleKey.RightArrow:
                playerX += (1 + speedDelta);
                break;
            case ConsoleKey.Escape:
                shouldExit = true;
                break;
            default:
                if (enableTermination)
                {
                    shouldExit = true;
                    return;
                }
                break;
        }

        // Clear the characters at the previous position
        Console.SetCursorPosition(lastX, lastY);
        for (int i = 0; i < player.Length; i++)
        {
            Console.Write(" ");
        }

        // Keep player position within the bounds of the Terminal window
        playerX = Math.Clamp(playerX, 0, width);
        playerY = Math.Clamp(playerY, 0, height);

        // Draw the player at the new location
        Console.SetCursorPosition(playerX, playerY);
        Console.Write(player);
    }
}

// Clears the console, displays the food and player
void InitializeGame()
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}
