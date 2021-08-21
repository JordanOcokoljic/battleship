namespace State
{
    /// <summary>
    /// Describes the possible outcomes of an attack on the <see cref="Board"/>.
    /// </summary>
    public enum AttackOutcome
    {
        DestroyedSegment,
        AlreadyDestroyed,
        Miss
    }
}