using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

Console.Clear();
Console.WriteLine("What would you like the starting password to be for your door?");
string? pass = Console.ReadLine();
TheLockedDoor Door = new TheLockedDoor(pass);

bool leave = false;

while(leave == false)
{
    Console.WriteLine($"You can open, close, lock or unlock the door. You can also change the code by entering 'change code'. The door is currently {Door.CurrentState()}. If you are done with the door just type leave.");
    string input = Console.ReadLine();
    switch(input)
    {
        case "open":
        Door.Open();
        break;
        case "close":
        Door.Close();
        break;
        case "lock":
        Door.Lock();
        break;
        case "unlock":
        Door.Unlock();
        break;
        case "change code":
        Door.changePass();
        break;
        case "leave":
        leave = true;
        break;
    }
}



public class TheLockedDoor
{

    private State _state;
    private int passWord; 

    public TheLockedDoor()
    {
        _state = State.Locked;
        passWord = 0;
    }

    public TheLockedDoor(string passcode)
    {
        _state = State.Locked;
        try
        {
            int code = Convert.ToInt32(passcode);
            passWord = code;
        }catch(FormatException)
        {
            Console.WriteLine("Not a numeric value given the password will be set to default");
            passWord = 0;
        }
    }

    public void changePass()
    {
        Console.WriteLine("You must type in the current passcode first");
        string? input = Console.ReadLine();
        int convertInput = 0;
        try
        {
            convertInput = Convert.ToInt32(input);
        }catch(FormatException)
        {
            Console.WriteLine("Not a numeric value given the current passcode will be kept");
        }
        if(convertInput == passWord)
        {
            Console.WriteLine("What whould you like the new passcode to be?");
            string? newPass = Console.ReadLine();
            int changePass = 0;
            try
            {
                changePass = Convert.ToInt32(newPass);
                passWord = changePass;
            }
            catch(FormatException)
            {
                Console.WriteLine("Not a numeric value given the current passcode will be kept");
            }
        }else
        {
            Console.WriteLine("Incorrect password the current passcode will be kept");
        }
        
    }

    public void Open()
    {
        
        if(CurrentState() != State.Locked)
        {
            if(CurrentState() == State.Closed)
            {
                ToggleOpenClosed();
            }else
            {
                Console.WriteLine("The Door is already open");
            }
        }else
        {
            Console.WriteLine("The Door is locked you cannot open it");
        }
    }

    public void Close()
    {
        if(CurrentState() == State.Open)
        {
            ToggleOpenClosed();
        }else
        {
            Console.WriteLine("The Door is already Closed");
        }
    }
    public void ToggleOpenClosed()
    {
        if(CurrentState() != State.Locked)
        {
            if(_state == State.Closed)
            {
                _state = State.Open;
            }else
            {
                _state = State.Closed;
            }
        }
    }

    public void Lock()
    {
        if(CurrentState() == State.Closed)
        {
            _state = State.Locked;
        }else
        {
            Console.WriteLine("The Door is open it needs to be closed before you can lock it");
        }
    
    }

    public void Unlock()
    {
        if(CurrentState() == State.Locked)
        {
            Console.WriteLine("To unlock the door you must enter the passcode first");
            string? input = Console.ReadLine();
            int convertInput = 0;
            try
            {
                convertInput = Convert.ToInt32(input);
            }catch(FormatException)
            {
                Console.WriteLine("Not a numeric vallue given the door will remain locked");
            }
            if(convertInput == passWord)
            {
                _state = State.Closed;
            }else
            {
                Console.WriteLine("Incorrect Password the door will remain locked");
            }
        }else
        {
            Console.WriteLine("The Door is already unlocked");
        }
    }

    public State CurrentState()
    {
       return _state;
    }

    public enum State{Locked,Open,Closed}
}