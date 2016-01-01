using DamageCalc.Actions;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DamageCalc.Components
{
    class Payload : EventArgs
    {
        public ACTION_TYPE ActionType { get; set; }

        public Card Card { get; set; }

        public int PlayerEHP
        {
            get
            {
                return PlayerHP + PlayerArmor;
            }
        }

        public int OpponentEHP
        {
            get
            {
                return OpponentHP + OpponentArmor;
            }
        }

        public ImmutableList<Entity> PlayerBoardMinions
        {
            get
            {
                if (playerBoardMinions == null) GetBoardMinions();
                return playerBoardMinions;
            }
        }

        public Payload()
        {
            _player = null;
            if (PlayerEntity != null)
                _player = PlayerEntity.GetTag(GAME_TAG.CONTROLLER);
        }

        private int? _player;

        private ImmutableList<Entity> playerBoardMinions = null;

        private Entity[] Entities
        {
            get
            {
                return Helper.DeepClone<Dictionary<int, Entity>>(
                    Hearthstone_Deck_Tracker.API.Core.Game.Entities).Values.ToArray<Entity>();
            }
        }

        private Entity PlayerEntity
        {
            get { return Entities == null ? null : Entities.FirstOrDefault(x => x.IsPlayer); }
        }

        private void GetBoardMinions()
        {
            var playerBoardMinionsBuilder = ImmutableList.CreateBuilder<Entity>();

            foreach (var e in Entities)
            {
                if (e.GetTag(GAME_TAG.CONTROLLER) == _player)
                {
                    if (e.IsInPlay)
                    {
                        if (e.IsMinion)
                        {
                            playerBoardMinionsBuilder.Add(e);
                        }
                    }
                }
            }

            playerBoardMinions = playerBoardMinionsBuilder.ToImmutable();
        }

        private int PlayerHP
        {
            get
            {
                return Hearthstone_Deck_Tracker.Utility.BoardDamage.EntityHelper.GetHeroEntity(true).Health;
            }
        }

        private int PlayerArmor
        {
            get
            {
                return Hearthstone_Deck_Tracker.Utility.BoardDamage.EntityHelper.GetHeroEntity(true).GetTag(GAME_TAG.ARMOR);
            }
        }

        private int OpponentHP
        {
            get
            {
                return Hearthstone_Deck_Tracker.Utility.BoardDamage.EntityHelper.GetHeroEntity(false).Health;
            }
        }

        private int OpponentArmor
        {
            get
            {
                return Hearthstone_Deck_Tracker.Utility.BoardDamage.EntityHelper.GetHeroEntity(false).GetTag(GAME_TAG.ARMOR);
            }
        }
    }
}
