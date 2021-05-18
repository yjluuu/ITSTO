using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSTOAPI.Log4net
{
    public class InterfaceLoggerLayoutPattern: PatternLayout
    {
        public InterfaceLoggerLayoutPattern()
        {
            this.AddConverter("interfaceLoggerInfo", typeof(InterfaceLoggerConverter));
        }
    }
}
