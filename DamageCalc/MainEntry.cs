using DamageCalc.Actions;
using DamageCalc.Components;
using DamageCalc.Dispatcher;
using DamageCalc.Stores;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace DamageCalc
{
    static class MainEntry
    {
        static InfoBox info;
        static PluginDispatcher dispatcher;
        static BoardStore boardStore;

        public static void Load()
        {
            dispatcher = new PluginDispatcher();
            boardStore = new BoardStore(dispatcher);
            info = new InfoBox(boardStore);

            // Game
            GameEvents.OnInMenu.Add(OnInMenu);
            GameEvents.OnGameStart.Add(OnGameStart);
            GameEvents.OnGameEnd.Add(OnGameEnd);

            // Player
            GameEvents.OnPlayerDraw.Add(OnPlayerDraw);
            GameEvents.OnPlayerGet.Add(OnPlayerGet);
            GameEvents.OnPlayerPlay.Add(OnPlayerPlay);
            GameEvents.OnPlayerCreateInPlay.Add(OnPlayerPlay);
            GameEvents.OnPlayerDeckToPlay.Add(OnPlayerPlay);

            // Opponent
            GameEvents.OnOpponentPlay.Add(OnOpponentPlay);
            GameEvents.OnOpponentCreateInPlay.Add(OnOpponentPlay);
            GameEvents.OnOpponentDeckToPlay.Add(OnOpponentPlay);

            dispatcher.OnActionReceived(ACTION_TYPE.INIT);
        }

        public static void Unload()
        {
            dispatcher.OnActionReceived(ACTION_TYPE.UNLOAD);
        }

        #region DISPATCH ACTIONS
        static void OnInMenu()
        {
            dispatcher.OnActionReceived(ACTION_TYPE.IN_MENU);
        }

        static void OnGameStart()
        {
            dispatcher.OnActionReceived(ACTION_TYPE.GAME_START);
        }

        static void OnGameEnd()
        {
            dispatcher.OnActionReceived(ACTION_TYPE.GAME_END);
        }

        static void OnPlayerDraw(Card c)
        {
            dispatcher.OnActionReceived(ACTION_TYPE.PLAYER_DRAW, c);
        }

        static void OnPlayerGet(Card c)
        {
            dispatcher.OnActionReceived(ACTION_TYPE.PLAYER_GET, c);
        }

        static void OnPlayerPlay(Card c)
        {
            dispatcher.OnActionReceived(ACTION_TYPE.PLAYER_PLAY, c);
        }

        static void OnOpponentPlay(Card c)
        {
            dispatcher.OnActionReceived(ACTION_TYPE.OPPONENT_PLAY, c);
        }
        #endregion

    }
}
