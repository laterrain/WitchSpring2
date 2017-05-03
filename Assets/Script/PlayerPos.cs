using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class PlayerPos
    {
        private static PlayerPos _instance = null;
        private PlayerPos()
        {

        }
        public static PlayerPos GetInstance()
        {
            if (_instance==null)
            {
                _instance=new PlayerPos();
            }
            return _instance;
        }
        public int index;
        public int storyIndex = 1;
    }
}
