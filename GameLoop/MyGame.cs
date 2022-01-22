using System;

public class MyGame
{
    string input { get; set; }
    int cursorx = 0;
    int cursory = 0;

	public MyGame()
	{
	}

    public void initialize()
    {
        Console.WriteLine("GameLoop Demo Initializing...");
    }

    public void run()
    {
        while (true)
        {
            Console.SetCursorPosition(0, 10);
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
                //input.Remove(input.Length - 1,1);
                input += "+";
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
        //Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
        ClearCurrentConsoleLine();
        Console.Write("[cmd:]"+input);

    }

    /// <summary>
    /// Thanks dknaak
    /// https://stackoverflow.com/questions/8946808/can-console-clear-be-used-to-only-clear-a-line-instead-of-whole-console/8946847
    /// </summary>
    public static void ClearCurrentConsoleLine()
    {
        int cursorx = Console.CursorLeft;
        int cursory = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.WriteLine(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(cursorx, Console.CursorTop - 1);
    }

}
