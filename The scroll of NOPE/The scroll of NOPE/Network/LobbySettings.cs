using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_scroll_of_NOPE.Network
{
    public class LobbySettings
    {
        public enum PlayerTypes { Anka, Student };
        public PlayerTypes SelectedPlayerType;

        public enum ClassTypes { TE14, TE15, TE16, DE16, EL14, EL15, EL16 };
        public ClassTypes SelectedClass;
    }

    public class LobbyUpdateEventArgs : EventArgs
    {
        public LobbySettings Data { get; set; }
    }
}
