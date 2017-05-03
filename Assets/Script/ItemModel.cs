using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    public class ItemModel
    {//道具信息的单例，保证全局唯一性
        private static ItemModel _instance = null;
        private ItemModel()
        {
            
        }
        public static ItemModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ItemModel();
            }
            return _instance;
        }
        public List<Item> itemList = new List<Item>();//总道具表
        public List<Item> bagList = new List<Item>();//背包中所拥有的道具列表
    }
}
