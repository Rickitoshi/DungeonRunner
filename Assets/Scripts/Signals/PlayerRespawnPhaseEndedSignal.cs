using Game.Systems;

namespace Signals
{
    public class PlayerRespawnPhaseEndedSignal
    {
        public readonly RespawnPhaseEnded PhaseEnded;

        public PlayerRespawnPhaseEndedSignal(RespawnPhaseEnded phaseEnded)
        {
            PhaseEnded = phaseEnded;
        }
    }
}