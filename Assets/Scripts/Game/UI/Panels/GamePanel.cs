using UnityEngine;
using Game.UI.Common;

public class GamePanel : BasePanel
{
    [SerializeField] private MagnetUI magnetUI;

    public MagnetUI Magnet => magnetUI;
}
