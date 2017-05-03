using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    public class PlayerModel
    {
        private static PlayerModel _instance = null;
        private PlayerModel()
        {

        }
        public static PlayerModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PlayerModel();
            }
            return _instance;
        }
        public List<PlayerInfo> Playerinfo = new List<PlayerInfo>();
    }
}
