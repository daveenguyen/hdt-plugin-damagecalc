using Hearthstone_Deck_Tracker.Plugins;
using System;

namespace DamageCalc
{
    public class DamageCalcPlugin : IPlugin
    {
        private String
            _pluginName = "Damage Calculator",
            _author = "Davee",
            _desc = "A calculator that adds the damage you have from minions on your board.",
            _buttonText = "Settings";

        private int
            _majVer = 0,
            _minVer = 1,
            _buildVer = 0;

        public System.Windows.Controls.MenuItem MenuItem
        {
            get { return null; }
        }

        public void OnButtonPress()
        {
        }

        public void OnLoad()
        {
            MainEntry.Load();
        }

        public void OnUnload()
        {
            MainEntry.Unload();
        }

        public void OnUpdate()
        {
        }

        #region Plugin Info
        public string Author
        {
            get { return _author; }
        }

        public string ButtonText
        {
            get { return _buttonText; }
        }

        public string Description
        {
            get { return _desc; }
        }

        public string Name
        {
            get { return _pluginName; }
        }

        public Version Version
        {
            get { return new Version(_majVer, _minVer, _buildVer); }
        }
        #endregion
    }
}
