using System;
using System.Collections.Generic;

namespace Version_2_C
{
    class clsDateComparer : IComparer<clsWork>
    {
        public int Compare(clsWork x, clsWork y)
        {
            //DateTime lcDateX = x.Date;
            //DateTime lcDateY = y.Date;

            return x.Date.CompareTo(y.Date);

            //return lcDateX.CompareTo(lcDateY);
        }
    }

    class clsDDateComparer : IComparer<clsWork>
    {
        public int Compare(clsWork x, clsWork y)
        {
            //DateTime lcDateX = x.Date;
            //DateTime lcDateY = y.Date;

            return y.Date.CompareTo(x.Date);

            //return lcDateX.CompareTo(lcDateY);
        }
    }
}
