using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuGuGuMusic
{
    public class MLButton:Button
    {
        public MLButton()
        {

        }
        public MLButton(MGroup mGroup)
        {
            _mGroup = mGroup;
        }

        private MGroup _mGroup = new MGroup();
        public MGroup M_mGroup
        {
            get { return _mGroup; }
            set
            {
                _mGroup = value;
            }
        }
    }
}
