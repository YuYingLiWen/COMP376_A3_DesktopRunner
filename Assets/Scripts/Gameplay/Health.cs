
public class Health 
{
    private readonly int maxHealth;
    private int points;
    private Health() { }
    public Health(int health)
    {
        this.maxHealth = health;
        this.points = health;
    }
    public int Points => points;
    public int MaxPoints => maxHealth;
    public void TakeDamage(int damage) => points -= damage;
    public float GetHealthPercent() => (float)points / (float)maxHealth;
    public bool IsAlive() => points > 0;
    public void Reset() => points = maxHealth;
    public void AddHealth(int points)
    {
        if (this.points + points > maxHealth) return;

        this.points += points;
    }
}
