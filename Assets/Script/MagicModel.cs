using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    public class MagicModel
    {//魔法信息的单例
        private static MagicModel _instance = null;
        private MagicModel()
        {

        }
        public static MagicModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MagicModel();
            }
            return _instance;
        }
        public List<Magic> magicList = new List<Magic>();//存储所有魔法的列表
    }
}
