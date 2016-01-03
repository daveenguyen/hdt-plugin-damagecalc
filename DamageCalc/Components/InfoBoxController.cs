using DamageCalc.Actions;
using DamageCalc.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamageCalc.Components
{
    class InfoBoxController
    {
        private DamageInfoBox boardDmg,
            savageDmg,
            doubleSavageDmg;

        private BoardStore boardStore;

        public InfoBoxController(BoardStore b)
        {
            boardStore = b;
            boardStore.EmitChange += OnEmitChange;

            boardDmg = new DamageInfoBox("Board Damage");
            savageDmg = new DamageInfoBox("Savage Roar");
            doubleSavageDmg = new DamageInfoBox("Double Savage");

            boardDmg.fromTop(0);
            savageDmg.fromTop(2);
            doubleSavageDmg.fromTop(3);
        }

        private void OnEmitChange(object sender, Payload e)
        {
            switch (e.ActionType)
            {
                case ACTION_TYPE.INIT:
                case ACTION_TYPE.GAME_START:
                case ACTION_TYPE.GAME_END:
                case ACTION_TYPE.IN_MENU:
                    boardDmg.hideInfo();
                    savageDmg.hideInfo();
                    doubleSavageDmg.hideInfo();
                    break;

                case ACTION_TYPE.UNLOAD:
                    break;

                default:
                    int damage = boardStore.TotalDamage;
                    boardDmg.showInfo();
                    boardDmg.Health = e.OpponentEHP;
                    boardDmg.Damage = damage;

                    damage = boardStore.TotalDamage + boardStore.SavageDamage;
                    savageDmg.showInfo();
                    savageDmg.Health = e.OpponentEHP;
                    savageDmg.Damage = damage;

                    damage = boardStore.TotalDamage + 2*boardStore.SavageDamage;
                    doubleSavageDmg.showInfo();
                    doubleSavageDmg.Health = e.OpponentEHP;
                    doubleSavageDmg.Damage = damage;
                    break;
            }
        }
    }
}
