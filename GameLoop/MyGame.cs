using System;

public class MyGame
{
    string input { get; set; }

	public MyGame()
	{
	}

    public void initialize()
    {
        Console.WriteLine("GameLoop Demo Initializing...");
        Console.Write("[cmd:]");
    }

    public void run()
    {
        while (true)
        {
            processInput();
            update();
            render();
        }
    }

    public void processInput()
    {
        if (Console.KeyAvailable == false)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Backspace)
            {
                input.Remove(input.Length - 1,1);
            }
            else if(key.Key == ConsoleKey.Enter)
            {
                // Most efficent to put this logic here.
                Console.SetCursorPosition(0, Console.CursorTop+1);
                input = "";
            }
            else
            {
                input += key.KeyChar.ToString();
            }
        }

    }

    public void update(/*TimeSpan elapsedTime*/)
    {
        //check if last character on input is the Enter char
        if (input.EndsWith("\r\n"))
        {
            input = "";
            // and then set cursorx to be after the "[cmd]"
        }
    }
    public void render() 
    {
        // remember where the cursor was to know where it will be
        //cursorx = Console.CursorLeft;
        //cursory = Console.CursorTop;

        // wipe the old image off (NOTE: There is a better way to do this with string formatting, but I am lazy)
        // See https://docs.microsoft.com/en-us/dotnet/api/system.string.format?view=net-6.0 for more on that later.
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.WriteLine(new string(' ', Console.WindowWidth));

        // draw the new image
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write("[cmd:]"+input);

        // 
        //Console.SetCursorPosition(cursorx, Console.CursorTop - 1);
    }

    /// <summary>
    /// Thanks dknaak
    /// https://stackoverflow.com/questions/8946808/can-console-clear-be-used-to-only-clear-a-line-instead-of-whole-console/8946847
    /// </summary>
}
