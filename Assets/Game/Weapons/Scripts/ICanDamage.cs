namespace MyGame
{
    public interface ICanDamage
    {
        float Damage { get; set; }
        void ApplyDamage(INetworkTarget networkTarget, float damage);
    }
}