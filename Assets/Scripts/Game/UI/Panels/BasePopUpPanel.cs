using Game.UI.Common;
using UnityEngine;

namespace Game.UI.Panels
{
    public abstract class BasePopUpPanel: BasePanel
    {
        [SerializeField] private PopUp popUp;

        public override void Initialize(PanelAnimationConfig config)
        {
            base.Initialize(config);
            popUp.Initialize(config);
        }

        public override void Activate()
        {
            base.Activate();
            popUp.Show();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            popUp.Hide();
        }
    }
}