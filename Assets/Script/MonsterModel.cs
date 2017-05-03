using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    class MonsterModel
    {
        private static MonsterModel _instance = null;
        private MonsterModel()
        {

        }
        public static MonsterModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MonsterModel();
            }
            return _instance;
        }

        public List<MonsterVo> monsterList = new List<MonsterVo>();
    }
}
