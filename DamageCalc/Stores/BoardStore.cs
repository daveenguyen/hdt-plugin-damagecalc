using DamageCalc.Actions;
using DamageCalc.Components;
using DamageCalc.Dispatcher;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System.Collections.Immutable;
using System.Linq;

namespace DamageCalc.Stores
{
    class BoardStore : BaseStore
    {
        public int MinionCount
        {
            get { return minionsDamageList.Count; }
        }

        public int TotalDamage
        {
            get { return calculateDamage(); }
        }

        public int SavageDamage
        {
            get { return 2 * (MinionCount + 1); }
        }

        public BoardStore(PluginDispatcher d) : base(d) { }

        override protected void OnDispatch(object sender, Payload payload)
        {
            switch (payload.ActionType)
            {
                case ACTION_TYPE.INIT:
                case ACTION_TYPE.PLAYER_DRAW:
                case ACTION_TYPE.PLAYER_PLAY:
                case ACTION_TYPE.PLAYER_GET:
                case ACTION_TYPE.OPPONENT_PLAY:
                    updateList(payload);
                    EmitChanges(payload);
                    break;
                case ACTION_TYPE.GAME_END:
                case ACTION_TYPE.UNLOAD:
                    dispatcher = null;
                    minionsDamageList = null;
                    EmitChanges(payload);
                    break;
                default:
                    break;
            }
        }

        private ImmutableList<int> minionsDamageList = ImmutableList<int>.Empty;

        private void updateList(Payload payload)
        {
            var builder = ImmutableList.CreateBuilder<int>();

            foreach (Entity entity in payload.PlayerBoardMinions)
            {
                builder.Add(entity.Attack);
            }

            minionsDamageList = builder.ToImmutable();

        }

        private int calculateDamage()
        {
            return minionsDamageList.Sum();
        }
    }
}
