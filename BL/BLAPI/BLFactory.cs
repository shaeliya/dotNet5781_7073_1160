﻿using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//להפוך לXML כמו ה-DAL FACTORY

// מכין לי את האובייקט כך שנוכל להעביר אותו לשכבה הבאה
namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL GetBL(string type)
        {
            switch (type)
            {
                case "1":
                    return new BLImp();
                case "2":
                //return new BLImp2();
                default:
                    return new BLImp();
            }
        }
    }
}
