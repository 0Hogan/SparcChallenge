using System;

namespace SparcChallenge{


    class Program
    {
        static void Main()
        {
            Console.WriteLine("I'm a program!");
            Console.WriteLine("I am finished.");
            LockoutCounter variable = new LockoutCounter();
            
            Aircraft ac = new Aircraft("ACID.txt");
            
            Console.WriteLine(ac.ID);
            
            variable.Increment();
            if (true == variable.IsLocked) {
                Console.WriteLine("locked out.");
            }
            
            variable.Increment();
            variable.Increment();
            if (true == variable.IsLocked) {
                Console.WriteLine("locked out 2.");
            }    
             ProfileLoader loader = new ProfileLoader("John","ISR","F-35",variable);
             
             loader.Load(ac);
             
        }
    }
}