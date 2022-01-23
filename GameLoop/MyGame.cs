﻿using System;

public class MyGame
{
    string input { get; set; }
    bool enterFlg { get; set; }
    bool newRender { get; set; }
    int CursorStartingPositionConstant = 6;
	public MyGame()
	{
        input = "";
        enterFlg = false;
        newRender = false;
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
        if (Console.KeyAvailable == true)
        {
            newRender = true;
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Backspace)
            {
                this.input.Remove(input.Length - 1,1);
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
        if (this.input.EndsWith("\r"))
        {
            this.input = "";
            this.enterFlg = true;

        }
    }

    /// <summary>
    /// Thanks dknaak
    /// https://stackoverflow.com/questions/8946808/can-console-clear-be-used-to-only-clear-a-line-instead-of-whole-console/8946847
    /// </summary>
    public void render() 
    {
        if (newRender)
        {

            // draw the new line over top of old line
            if (enterFlg)
                Console.CursorTop++;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine(("[cmd:]" + input).PadRight(Console.WindowWidth));

            Console.SetCursorPosition(CursorStartingPositionConstant + input.Length, Console.CursorTop - 1);



            // Reset flags
            this.enterFlg = false;
        }
        newRender = false;
    }

}
