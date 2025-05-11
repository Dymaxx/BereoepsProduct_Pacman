namespace Assets.Scripts
{
    /// <summary>
    /// Interface voor GameObjecten die opgegeten kunnen worden door Pacman.
    /// </summary>
    public interface IEatable
    {
        /// <summary>
        /// Aktie die wordt uitgevoerd als Pacman de IEatable aanraakt.
        /// </summary>
        public void Eaten();
    }
}
