namespace MyGame
{
    public interface INetworkTarget
    {
        float Health { get; set; }
        void TakeDamage(float damage);
        void NetworkDie();
        void NetworkInitialize();
    }
}