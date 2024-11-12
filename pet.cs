using System;

//husdjuret
public class Pet
{
    public string Name { get; set; }
    public int Hunger { get; set; }
    public int Happiness { get; set; }
    public int Energy { get; set; }
    public DateTime LastFed { get; set; }
    public DateTime LastPlayed { get; set; }
    public DateTime LastSlept { get; set; }

    public Pet(string name)
    {
        Name = name;
        Hunger = 50;
        Happiness = 50;
        Energy = 50;
        LastFed = DateTime.Now;
        LastPlayed = DateTime.Now;
        LastSlept = DateTime.Now;
    }

    public void Feed()
    {
        Hunger = Math.Min(100, Hunger + 20);
        LastFed = DateTime.Now;
    }

    public void Play()
    {
        Happiness = Math.Min(100, Happiness + 20);
        Energy = Math.Max(0, Energy - 10);
        LastPlayed = DateTime.Now;
    }

    public void Sleep()
    {
        Energy = Math.Min(100, Energy + 30);
        Hunger = Math.Max(0, Hunger - 10);
        LastSlept = DateTime.Now;
    }

public void UpdateStatus()
{
    //minska hunger och energi allt eftersom tiden går
    Hunger = Math.Max(0, Hunger - 5);   //se till att värdet inte går under 0
    Energy = Math.Max(0, Energy - 3); 
    Happiness = Math.Max(0, Happiness - 2); 
}

}