using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class MyGame
{
    string input { get; set; }
    bool enterFlg { get; set; }
    bool newRender { get; set; }
    int CursorStartingPositionConstant = 6;
    DateTime lastUpdateTimestamp = DateTime.Now;
    List<MyEvent> events = new List<MyEvent>();
    List<MyEvent> printables = new List<MyEvent>();
    
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
            update(this.lastUpdateTimestamp - DateTime.Now);
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
                if (input.Length > 0)
                    input = input.Remove(input.Length - 1,1);
            }
            else
            {
                input += key.KeyChar.ToString();
            }
        }

    }

    public void update(TimeSpan elapsedTime)
    {
        //check if last character on input is the Enter char
        if (this.input.EndsWith("\r"))
        {
            input = input.Remove(input.Length-1,1);
            if (input == "quit")
                Environment.Exit(0);
            
            // now here you check for a correctly minced line
            string[] param = input.Split(" ");
            if (param.Length == 5    &&
                param[0] == "create" &&
                param[1] == "event"  &&
                Regex.IsMatch(param[2], @"^[a-zA-Z]+$") &&
                Regex.IsMatch(param[3], @"^[0-9]+$")    &&
                Regex.IsMatch(param[4], @"^[0-9]+$"))
            {
                TimeSpan interval = TimeSpan.FromMilliseconds(int.Parse(param[3])).Duration(); // NOTE: I call 'Duration' to force value to always be positive. 
                events.Add(new MyEvent(param[2], int.Parse(param[4]), DateTime.Now, interval) );
            }
            this.input = "";
            this.enterFlg = true;

        }

        // wipe old cache
        printables.Clear();

        // init items marked for deletion
        List<MyEvent> deletethese = new List<MyEvent>();

        var dummy = true; // deletelater

        foreach (MyEvent theevent in events)
        {
            // mark events for deletion that have reached their end (removed subsequently)
            if (theevent.eventRemainingCount < 1)
            {
                deletethese.Add(theevent);
            }
            //if (theevent.timestamp.Add(elapsedTime) >= DateTime.Now)
            if (dummy)
            {
                printables.Add(theevent);
                theevent.timestamp = DateTime.Now;
                theevent.eventRemainingCount--;

            }
            //if(dummy)
            //{
            //    printables.Add(new MyEvent("bob", 10, DateTime.Now, DateTime.Now - DateTime.Now) );
            //    theevent.timestamp = DateTime.Now;
            //    theevent.eventRemainingCount--;
            //    dummy = false;
            //}
        }

        events.RemoveAll(x => deletethese.Contains(x));

        // if we found events that will fire this cycle
        if (printables.Count > 0)
            newRender = true;

        this.lastUpdateTimestamp = DateTime.Now;
    }

    /// <summary>
    /// Thanks, @dknaak
    /// https://stackoverflow.com/questions/8946808/can-console-clear-be-used-to-only-clear-a-line-instead-of-whole-console/8946847
    /// </summary>
    public void render() 
    {
        //if (newRender)
        //{
            // if the enter button was pressed, we draw over the top of the next line
            // instead of the line our cursor is currently on. 
            if (enterFlg)
            {
                Console.CursorTop++;
                this.enterFlg = false;
            }
            
            // cursor to left
            Console.SetCursorPosition(0, Console.CursorTop);

            // Print all events that have fired.
            foreach (MyEvent theevent in printables)
            {
                Console.WriteLine($"Event: {theevent.eventName} ({theevent.eventRemainingCount} remaining)");
            }

            // Re-draw the command line, line. 
            Console.WriteLine(("[cmd:]" + input).PadRight(Console.WindowWidth));

            // Put the cursor back where it should go
            Console.SetCursorPosition(CursorStartingPositionConstant + input.Length, Console.CursorTop - 1);


        //}
        newRender = false; /// DELETELATER you n
    }


    /// <summary>
    /// Utility class for tracking types
    /// </summary>
    class MyEvent
    {
        public string eventName { get; set; }
        public int eventRemainingCount { get; set; }
        public DateTime timestamp { get; set; }
        public TimeSpan interval { get; set; }


        public MyEvent(string name, int count, DateTime timestampt, TimeSpan passed)
        {
            eventName = (name);
            eventRemainingCount = count;
            timestamp = timestampt;
            interval = passed;
        }

    }
}
