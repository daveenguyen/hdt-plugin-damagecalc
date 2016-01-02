using DamageCalc.Actions;
using DamageCalc.Stores;
using Hearthstone_Deck_Tracker;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DamageCalc.Components
{
    class InfoBox
    {
        private HearthstoneTextBlock _info;
        private BoardStore boardStore;
        private Canvas _canvas = Hearthstone_Deck_Tracker.API.Core.OverlayCanvas;

        public string Text
        {
            get { return _info.Text; }
            set { _info.Text = value; }
        }

        public InfoBox(BoardStore b)
        {
            boardStore = b;
            boardStore.EmitChange += OnEmitChange;

            _info = new HearthstoneTextBlock();
            _info.Text = "";
            _info.FontSize = 18;
            _info.Opacity = 0;

            DelayedSetPosition(1000);

            _canvas.Children.Add(_info);
        }

        private async void DelayedSetPosition(int delay)
        {
            await Task.Delay(delay);

            _info.Opacity = 1;

            double fromTop = _canvas.Height * .75;
            double fromLeft = _canvas.Width * .3;

            Canvas.SetTop(_info, fromTop);
            Canvas.SetLeft(_info, fromLeft);
        }

        void OnEmitChange(object sender, Payload payload)
        {
            switch (payload.ActionType)
            {
                case ACTION_TYPE.INIT:
                case ACTION_TYPE.GAME_START:
                case ACTION_TYPE.GAME_END:
                case ACTION_TYPE.IN_MENU:
                    _info.Text = "";
                    break;
                case ACTION_TYPE.UNLOAD:
                    _canvas.Children.Remove(_info);
                    break;
                default:
                    _info.Text = "Damage Calculator\n";

                    int damage = boardStore.TotalDamage;
                    _info.Text += string.Format("{0}Board Damage: {1}\n", CheckLethal(payload, damage), damage);

                    damage = boardStore.TotalDamage + boardStore.SavageDamage;
                    _info.Text += string.Format("{0}Savage Roar: {1}\n", CheckLethal(payload, damage), damage);

                    damage = boardStore.TotalDamage + 2*boardStore.SavageDamage;
                    _info.Text += string.Format("{0}2xSavage Roar: {1}\n", CheckLethal(payload, damage), damage);

                    break;
            }
        }

        string CheckLethal(Payload payload, int damage)
        {
            int hp = payload.OpponentEHP;

            if (hp - damage <= 0)
            {
                return string.Format("*LETHAL({0})* ", hp - damage);
            } else
            {
                return string.Empty;
            }
        }
    }
}
